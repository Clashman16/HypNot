using TMPro;
using UnityEngine;

namespace HypNot.Behaviours.UI
{
   public class CreditsBehaviour : MonoBehaviour
   {
      private Animator m_textanimator;

      private Animator m_companyLogoAnimator;

      public void Reset()
      {
         if(m_textanimator == null)
         {
            Animator[] l_animators = transform.parent.GetComponentsInChildren<Animator>();

            foreach(Animator l_animator in l_animators)
            {
               if(l_animator.GetComponent<TextMeshProUGUI>())
               {
                  m_textanimator = l_animator;
               }
               else
               {
                  m_companyLogoAnimator = l_animator;
               }
            }
         }

         m_textanimator.SetTrigger("Scroll");

         m_companyLogoAnimator.SetTrigger("Reset");
      }

      public void MakeCompanyLogoToScroll()
      {
         m_companyLogoAnimator.ResetTrigger("Reset");
         m_companyLogoAnimator.SetTrigger("Scroll");
      }
   }
}