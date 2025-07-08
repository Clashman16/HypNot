using TMPro;
using UnityEngine;

namespace HypNot.Behaviours.Characters
{
   public class HypnotizedPersonAnimatorBehaviour : MonoBehaviour
   {
      private Animator m_animator;

      private TextMeshProUGUI m_manaDisplay;

      public CharacterType Type
      {
         set
         {
            if(m_animator == null)
            {
               m_animator = GetComponent<Animator>();
            }

            m_animator.SetTrigger(value.ToString().ToLower());
         }
      }

      public int ManaCount
      {
         set
         {
            if (m_manaDisplay == null)
            {
               m_manaDisplay = GetComponentInChildren<TextMeshProUGUI>();
            }

            m_manaDisplay.text = value.ToString();
         }
      }
   }
}
