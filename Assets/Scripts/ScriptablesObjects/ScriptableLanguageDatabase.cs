using HypNot.Translations;
using System.Collections.Generic;
using UnityEngine;

namespace HypNot.ScriptableObjects
{
   [CreateAssetMenu(menuName = "ScriptableObjects/LanguagesDatabase")]
   public class ScriptableLanguageDatabase : ScriptableObject
   {
      public List<TranslationEntry> m_french;

      public List<TranslationEntry> m_english;

      private Dictionary<string, string> m_frenchDictionary = new();

      public Dictionary<string, string> French
      {
         get => m_frenchDictionary;
      }

      private Dictionary<string, string> m_englishDictionary = new();

      public Dictionary<string, string> English
      {
         get => m_englishDictionary;
      }
   }
}
