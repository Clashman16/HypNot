using TMPro;
using UnityEngine;

namespace HypNot.Behaviours.UI
{
   public class ScrollingCreditsBehaviour : MonoBehaviour
   {
      private Animator m_textAnimator;

      private Animator m_companyLogoAnimator;

      public void Reset()
      {
         if(m_textAnimator == null)
         {
            Animator[] l_animators = transform.parent.GetComponentsInChildren<Animator>();

            foreach(Animator l_animator in l_animators)
            {
               if(l_animator.GetComponent<TextMeshProUGUI>())
               {
                  m_textAnimator = l_animator;
               }
               else
               {
                  m_companyLogoAnimator = l_animator;
               }
            }
         }

         m_textAnimator.SetTrigger("Scroll");

         m_companyLogoAnimator.SetTrigger("Reset");
      }

      public void MakeCompanyLogoToScroll()
      {
         m_companyLogoAnimator.ResetTrigger("Reset");
         m_companyLogoAnimator.SetTrigger("Scroll");
      }
   }
}