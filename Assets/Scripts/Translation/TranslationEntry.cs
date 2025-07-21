using System;
using UnityEngine;

namespace HypNot.Translations
{
   [Serializable]
   public class TranslationEntry
   {
      [SerializeField]
      private string m_id;

      public string Id
      {
         get => m_id;
      }

      [SerializeField]
      private string m_translation;

      public string Translation
      {
         get => m_translation;
      }
   }
}
