using HypNot.Player;
using TMPro;
using UnityEngine;

namespace HypNot.Translations
{
   public class TranslatedUIBehaviour : MonoBehaviour
   {
      [SerializeField]
      private string m_TranslationId;

      private TextMeshProUGUI m_textToTranslate;

      public void Translate()
      {
         if(m_textToTranslate == null)
         {
            m_textToTranslate = GetComponent<TextMeshProUGUI>();
         }

         LanguageDatabaseSingleton l_language = LanguageDatabaseSingleton.Instance;

         m_textToTranslate.text = PlayerSaveSingleton.Instance.LanguageId == l_language.EnglishId ?
            l_language.Database.English[m_TranslationId]
            : l_language.Database.French[m_TranslationId];
      }
   }
}
