using HypNot.Behaviours.Characters;
using HypNot.Behaviours.UI;
using HypNot.Map;
using HypNot.Player;
using HypNot.Spawners.Utils;
using Pathfinding;
using HypNot.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace HypNot.Spawners
{
   public sealed class CitizenSpawnerSingleton : ObjectRecycler
   {
      private static CitizenSpawnerSingleton m_instance = null;

      private Transform m_spawnTransform;

      private Queue<CharacterType> m_typeRecycleBin;

      public static CitizenSpawnerSingleton Instance
      {
         get
         {
            if (m_instance == null)
            {
               m_instance = new CitizenSpawnerSingleton();
            }
            return m_instance;
         }
      }

      public CitizenSpawnerSingleton() : base()
      {
         m_typeRecycleBin = new Queue<CharacterType>();
      }

      public void Spawn(Transform p_citizenTarget)
      {
         GameObject l_instantiatedCitizen = null;

         if (!IsRecycleBinEmpty)
         {
            l_instantiatedCitizen = RemoveFromRecycleBin();

            l_instantiatedCitizen.transform.position = m_spawnTransform.position;

            l_instantiatedCitizen.GetComponent<CitizenAnimatorBehaviour>().IsDestinationReached = false;

            CitizenDataBehaviour l_citizen = l_instantiatedCitizen.GetComponent<CitizenDataBehaviour>();

            ActiveCitizenAI(l_citizen, p_citizenTarget);
         }

         if (l_instantiatedCitizen == null)
         {
            CharacterType l_type;

            if (m_typeRecycleBin.Count == 0)
            {
               l_type = PlayerStateSingleton.Instance.FirstCitizenType;
            }
            else
            {
               l_type = m_typeRecycleBin.Dequeue();
            }

            string l_citizenName = l_type.ToString().ToLower();

            ScriptableInstantiatedObjectDatabase l_database = SpawnableDatabaseSingleton.Instance.Database;

            GameObject l_citizenResource = l_database.Citizen;

            if(m_spawnTransform == null)
            {
               m_spawnTransform = Object.FindObjectOfType<CitizenIndicatorBehaviour>().transform;
            }

            l_instantiatedCitizen = Object.Instantiate(l_citizenResource, m_spawnTransform.position, Quaternion.identity);

            Animator l_animator = l_instantiatedCitizen.GetComponent<Animator>();
            l_animator.runtimeAnimatorController = l_database.AnimatorsByName[l_citizenName];
            l_animator.Rebind();

            CitizenDataBehaviour l_citizen = l_instantiatedCitizen.GetComponent<CitizenDataBehaviour>();

            l_citizen.Type = l_type;

            ActiveCitizenAI(l_citizen, p_citizenTarget);
         }

         MapManagerSingleton.Instance.LastSpawnedCitizen = l_instantiatedCitizen;
      }

      private void ActiveCitizenAI(CitizenDataBehaviour p_citizen, Transform p_citizenTarget)
      {
         p_citizen.OrderInLayer = MapManagerSingleton.Instance.OrderInLayer;

         MapManagerSingleton.Instance.OrderInLayer += 1;

         HypnotizedPersonTargetBehaviour l_target = p_citizenTarget.GetComponent<HypnotizedPersonTargetBehaviour>();

         Dictionary<Vector2, bool> l_spots = l_target.Data.OccupiedCitizenSpots;

         List<Vector2> l_freeSpots = new List<Vector2>();

         foreach (KeyValuePair<Vector2, bool> l_spot in l_spots)
         {
            if (!l_spot.Value)
            {
               l_freeSpots.Add(l_spot.Key);
            }
         }

         Vector2 l_citizenPosition = p_citizen.transform.position;

         l_freeSpots.Sort((l_a, l_b) => Vector3.Distance(l_citizenPosition, l_a).CompareTo(Vector3.Distance(l_citizenPosition, l_b)));

         AIPath l_path = p_citizen.GetComponent<AIPath>();

         l_path.destination = l_freeSpots[0];

         l_path.canSearch = true;
         l_path.canMove = true;

         CitizenAIBehaviour l_ai = p_citizen.GetComponent<CitizenAIBehaviour>();

         l_ai.Target = l_target;
         l_ai.AIPath = l_path;

         MapManagerSingleton.Instance.AddCollidable(p_citizen);
      }

      public override void AddToRecycleBin(GameObject p_object)
      {
         CharacterType l_type = p_object.GetComponent<CitizenDataBehaviour>().Type;

         CitizenIndicatorBehaviour l_scoreIndicator = Object.FindObjectOfType<CitizenIndicatorBehaviour>();
         l_scoreIndicator.Type = l_type;

         if (IsRecycleBinFullFilled)
         {
            m_typeRecycleBin.Enqueue(l_type);
         }
         
         base.AddToRecycleBin(p_object);
      }

      public void AddToTypeRecycleBin(CharacterType p_type)
      {
         m_typeRecycleBin.Enqueue(p_type);

         CitizenIndicatorBehaviour l_scoreIndicator = Object.FindObjectOfType<CitizenIndicatorBehaviour>();
         l_scoreIndicator.Type = p_type;
      }

      public override void Reset()
      {
         base.Reset();

         m_typeRecycleBin.Clear();
      }
   }
}
