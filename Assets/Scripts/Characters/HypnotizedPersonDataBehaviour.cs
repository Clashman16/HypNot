using HypNot.Map;
using UnityEngine;

namespace HypNot.Behaviours.Characters
{
   public class HypnotizedPersonDataBehaviour : MonoBehaviour
   {
      private CharacterType m_type;

      private HypnotizedPersonAnimatorBehaviour m_animator;

      public CharacterType Type
      {
         get => m_type;
         set
         {
            m_type = value;

            if (m_animator == null)
            {
               m_animator = GetComponent<HypnotizedPersonAnimatorBehaviour>();
            }

            m_animator.Type = m_type;
         }
      }

      private int m_firstManaCount;

      public int FirstManaCount
      {
         get => m_firstManaCount;
         set => m_firstManaCount = value;
      }

      private int m_manaCount;

      public int ManaCount
      {
         get => m_manaCount;
         set
         {
            m_manaCount = value;

            if(m_manaCount < 0)
            {
               m_manaCount = 0;
            }

            if (m_animator == null)
            {
               m_animator = GetComponent<HypnotizedPersonAnimatorBehaviour>();
            }

            m_animator.ManaCount = m_manaCount;

            if (m_manaCount == 0)
            {
               GetComponent<HypnotizedPersonTargetBehaviour>().OnPersonSaved();
            }
         }
      }

      private bool m_hasNeighbor;

      public bool HasNeighbor
      {
         get => m_hasNeighbor;
         set => m_hasNeighbor = value;
      }

      private HypnotizedPersonSpawnPointBehaviour m_spawnPoint;

      public HypnotizedPersonSpawnPointBehaviour SpawnPoint
      {
         get => m_spawnPoint;
         set => m_spawnPoint = value;
      }
   }
}
