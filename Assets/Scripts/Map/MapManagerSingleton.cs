using HypNot.Behaviours.Characters;
using System.Collections.Generic;
using UnityEngine;

namespace HypNot.Map
{
   public sealed class MapManagerSingleton
   {
      private static MapManagerSingleton m_instance = null;

      public static MapManagerSingleton Instance
      {
         get
         {
            if (m_instance == null)
            {
               m_instance = new MapManagerSingleton();
            }
            return m_instance;
         }
      }

      private const int m_characterTypeEnumSize = 15;

      public int CharacterTypeEnumSize
      {
         get => m_characterTypeEnumSize;
      }

      private Dictionary<CharacterType, int> m_hypnotizedPersonTypes;

      public Dictionary<CharacterType, int> HypnotizedPersonTypes
      {
         get => m_hypnotizedPersonTypes;
      }

      private Dictionary<int, int> m_hypnotizedPersonMana;

      public Dictionary<int, int> HypnotizedPersonMana
      {
         get => m_hypnotizedPersonMana;
      }

      private const int m_hypnotizedPersonManaMax = 4;

      private GameObject m_lastSpawnedCitizen;

      public GameObject LastSpawnedCitizen
      {
         get => m_lastSpawnedCitizen;
         set => m_lastSpawnedCitizen = value;
      }

      private MapManagerSingleton()
      {
         m_hypnotizedPersonTypes = new Dictionary<CharacterType, int>();

         m_hypnotizedPersonMana = new Dictionary<int, int>();

         for (int l_i = 0; l_i < m_characterTypeEnumSize; l_i++)
         {
            m_hypnotizedPersonTypes.Add((CharacterType) l_i, 0);

            if(l_i <= m_hypnotizedPersonManaMax)
            {
               m_hypnotizedPersonMana.Add(l_i, 0);
            }
         }
      }

      public void Reset()
      {
         for (int l_i = 0; l_i < m_characterTypeEnumSize; l_i++)
         {
            m_hypnotizedPersonTypes[(CharacterType)l_i] = 0;

            if (l_i <= m_hypnotizedPersonManaMax)
            {
               m_hypnotizedPersonMana[l_i] = 0;
            }
         }

         CitizenAIBehaviour[] l_citizens = Object.FindObjectsByType<CitizenAIBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None);

         foreach (CitizenAIBehaviour l_citizen in l_citizens)
         {
            Object.Destroy(l_citizen.gameObject);
         }

         HypnotizedPersonDataBehaviour[] l_hypnotizedPersons = Object.FindObjectsByType<HypnotizedPersonDataBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None);

         foreach (HypnotizedPersonDataBehaviour l_person in l_hypnotizedPersons)
         {
            Object.Destroy(l_person.gameObject);
         }
      }
   }
}
