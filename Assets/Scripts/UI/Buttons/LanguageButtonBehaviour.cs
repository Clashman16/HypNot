using HypNot.Player;
using UnityEngine.UI;

namespace HypNot.Behaviours.UI
{
   public class LanguageButtonBehaviour : OneActionButtonBehaviour
   {
      private Button m_languageButton;

      private Button m_otherLanguageButton;


      public void Reset()
      {
         if(m_languageButton == null)
         {
            LanguageButtonBehaviour[] l_allLanguageButtons = transform.parent.GetComponentsInChildren<LanguageButtonBehaviour>();

            foreach (LanguageButtonBehaviour l_button in l_allLanguageButtons)
            {
               if (l_button != this)
               {
                  m_otherLanguageButton = l_button.GetComponent<Button>();
               }
               else
               {
                  m_languageButton = l_button.GetComponent<Button>();
               }
            }
         }
         
         if(name.ToLower().Contains(PlayerSaveSingleton.Instance.LanguageId))
         {
            m_languageButton.interactable = false;
         }
      }

      public override void Act()
      {
         base.Act();

         string l_languageId = PlayerSaveSingleton.Instance.LanguageId;

         PlayerSaveSingleton.Instance.LanguageId = l_languageId == "english" ? "french" : "english";

         m_otherLanguageButton.interactable = true;

         m_languageButton.interactable = false;
      }
   }
}
