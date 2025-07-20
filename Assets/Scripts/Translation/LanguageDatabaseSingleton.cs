using HypNot.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace HypNot.Translations
{
   public sealed class LanguageDatabaseSingleton
   {
      private const string m_databasePath = "ScriptableObjects/LanguageDatabase";

      private static LanguageDatabaseSingleton m_instance = null;

      private ScriptableLanguageDatabase m_database;

      public ScriptableLanguageDatabase Database
      {
         get => m_database;
      }

      public static LanguageDatabaseSingleton Instance
      {
         get
         {
            if (m_instance == null)
            {
               m_instance = new LanguageDatabaseSingleton();
            }
            return m_instance;
         }
      }

      private LanguageDatabaseSingleton()
      {
         m_database = Resources.Load<ScriptableLanguageDatabase>(m_databasePath);

         List<TranslationEntry> l_frenchTranslation = m_database.m_french;
         List<TranslationEntry> l_englishTranslation = m_database.m_english;

         for(int l_i = 0; l_i < l_frenchTranslation.Count; l_i++)
         {
            TranslationEntry l_frenchEntry = l_frenchTranslation[l_i];

            m_database.French.Add(l_frenchEntry.Id, l_frenchEntry.Translation);

            TranslationEntry l_englishEntry = l_englishTranslation[l_i];

            m_database.English.Add(l_englishEntry.Id, l_englishEntry.Translation);
         }
      }
   }
}
