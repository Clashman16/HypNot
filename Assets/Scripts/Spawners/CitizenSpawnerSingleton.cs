using HypNot.Behaviours.UI;
using HypNot.Player;
using ScriptableObjects;
using UnityEngine;

namespace HypNot.Spawners
{
   public sealed class CitizenSpawnerSingleton : ObjectRecycler
   {
      private static CitizenSpawnerSingleton m_instance = null;

      private Transform m_spawnTransform;

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

      }

      public void Spawn(Transform p_citizenTarget)
      {
         GameObject l_instantiatedCitizen = null;

         if (!IsRecycleBinEmpty)
         {
            l_instantiatedCitizen = RemoveFromRecycleBin();
         }

         if (l_instantiatedCitizen == null)
         {
            string l_citizenName = PlayerStateSingleton.Instance.FirstCitizenType.ToString().ToLower();

            ScriptableInstantiatedObjectDatabase l_database = SpawnableDatabaseSingleton.Instance.Database;

            GameObject l_citizenResource = l_database.Citizen;

            if(m_spawnTransform == null)
            {
               m_spawnTransform = Object.FindObjectOfType<CitizenIndicatorBehaviour>().transform;
            }

            l_instantiatedCitizen = Object.Instantiate(l_citizenResource, m_spawnTransform.position, Quaternion.identity);

            l_instantiatedCitizen.GetComponent<Animator>().runtimeAnimatorController = l_database.AnimatorsByName[l_citizenName];
         }
      }
   }
}
