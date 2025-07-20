using HypNot.Player;
using HypNot.Translations;
using UnityEngine;

namespace HypNot.Behaviours.UI
{
   public class Translator
   {
      private string m_lastLanguageId;

      public bool HasTranslated
      {
         get => m_lastLanguageId != null && m_lastLanguageId == PlayerSaveSingleton.Instance.LanguageId;
      }

      public void TranslateScreen(GameObject p_screenObject)
      {
         string l_currentLanguageId = PlayerSaveSingleton.Instance.LanguageId;

         TranslatedUIBehaviour[] l_textsToTranslate = p_screenObject.GetComponentsInChildren<TranslatedUIBehaviour>();

         foreach (TranslatedUIBehaviour l_txt in l_textsToTranslate)
         {
            l_txt.Translate();
         }

         if(m_lastLanguageId != l_currentLanguageId)
         {
            m_lastLanguageId = l_currentLanguageId;
         }
      }
   }
}
