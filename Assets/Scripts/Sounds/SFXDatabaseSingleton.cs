using HypNot.ScriptableObjects;
using UnityEngine;

namespace HypNot.Sounds
{
   public sealed class SFXDatabaseSingleton
   {
      private const string m_databasePath = "ScriptableObjects/SFXDatabase";

      private static SFXDatabaseSingleton m_instance = null;

      private ScriptableSFXDatabase m_database;

      public ScriptableSFXDatabase Database
      {
         get => m_database;
      }

      public static SFXDatabaseSingleton Instance
      {
         get
         {
            if (m_instance == null)
            {
               m_instance = new SFXDatabaseSingleton();
            }
            return m_instance;
         }
      }

      private SFXDatabaseSingleton()
      {
         m_database = Resources.Load<ScriptableSFXDatabase>(m_databasePath);
      }
   }
}
