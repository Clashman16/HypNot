using UnityEngine;

namespace HypNot.Behaviours.Characters
{
   public class HypnotizedPersonAnimatorBehaviour : MonoBehaviour
   {
      private CharacterType m_type;

      private Animator m_animator;

      public CharacterType Type
      {
         get => m_type;
         set
         {
            m_type = value;

            if(m_animator == null)
            {
               m_animator = GetComponent<Animator>();
            }

            m_animator.SetTrigger(m_type.ToString().ToLower());
         }
      }
   }
}
