using UnityEngine;

namespace HypNot.Player
{
   public sealed class PlayerSaveSingleton
   {
      private static PlayerSaveSingleton m_instance = null;

      private const string m_prefPrefix = "Hypnot.";

      public static PlayerSaveSingleton Instance
      {
         get
         {
            if (m_instance == null)
            {
               m_instance = new PlayerSaveSingleton();
            }
            return m_instance;
         }
      }

      private PlayerSaveSingleton()
      {

      }

      public int TimerMax
      {
         get => PlayerPrefs.GetInt(string.Concat(m_prefPrefix, "TimerMax"), 1);
         set => PlayerPrefs.SetInt(string.Concat(m_prefPrefix, "TimerMax"), value);
      }

      public int Difficulty
      {
         get => PlayerPrefs.GetInt(string.Concat(m_prefPrefix, "Difficulty"), 1);
         set => PlayerPrefs.SetInt(string.Concat(m_prefPrefix, "Difficulty"), value);
      }
   }
}
