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

      private const string l_englishId = "english";

      public void Translate()
      {
         if(m_textToTranslate == null)
         {
            m_textToTranslate = GetComponent<TextMeshProUGUI>();
         }

         m_textToTranslate.text = PlayerSaveSingleton.Instance.LanguageId == l_englishId ?
            LanguageDatabaseSingleton.Instance.Database.English[m_TranslationId]
            : LanguageDatabaseSingleton.Instance.Database.French[m_TranslationId];
      }
   }
}
