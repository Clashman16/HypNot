using UnityEngine;

namespace HypNot.Behaviours.Characters
{
   public class HypnotizedPersonDataBehaviour : MonoBehaviour
   {
      [SerializeField]
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

      [SerializeField]
      private int m_manaCount;

      public int ManaCount
      {
         get => m_manaCount;
         set => m_manaCount = value;
      }
   }
}
