using HypNot.Behaviours.Characters;
using HypNot.Behaviours.UI;
using HypNot.Map;
using HypNot.Player;
using HypNot.Spawners.Utils;
using Pathfinding;
using ScriptableObjects;
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

            CitizenAIBehaviour l_citizen = l_instantiatedCitizen.GetComponent<CitizenAIBehaviour>();

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

            l_instantiatedCitizen.GetComponent<Animator>().runtimeAnimatorController = l_database.AnimatorsByName[l_citizenName];

            CitizenAIBehaviour l_citizen = l_instantiatedCitizen.GetComponent<CitizenAIBehaviour>();

            l_citizen.Type = l_type;

            ActiveCitizenAI(l_citizen, p_citizenTarget);
         }

         MapManagerSingleton.Instance.LastSpawnedCitizen = l_instantiatedCitizen;
      }

      private void ActiveCitizenAI(CitizenAIBehaviour p_citizen, Transform p_citizenTarget)
      {
         AIPath l_path = p_citizen.GetComponent<AIPath>();

         l_path.destination = p_citizenTarget.position;

         l_path.canSearch = true;
         l_path.canMove = true;

         p_citizen.Target = p_citizenTarget.GetComponent<HypnotizedPersonTargetBehaviour>();
         p_citizen.AIPath = l_path;
      }

      public override void AddToRecycleBin(GameObject p_object)
      {
         CharacterType l_type = p_object.GetComponent<CitizenAIBehaviour>().Type;

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
