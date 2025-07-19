using HypNot.ScriptableObjects;
using UnityEngine;

namespace HypNot.Spawners.Utils
{
   public sealed class SpawnableDatabaseSingleton
   {
      private const string m_databasePath = "ScriptableObjects/SpawnableDatabase";

      private static SpawnableDatabaseSingleton m_instance = null;

      private ScriptableInstantiatedObjectDatabase m_database;

      public ScriptableInstantiatedObjectDatabase Database
      {
         get => m_database;
      }

      public static SpawnableDatabaseSingleton Instance
      {
         get
         {
            if (m_instance == null)
            {
               m_instance = new SpawnableDatabaseSingleton();
            }
            return m_instance;
         }
      }

      private SpawnableDatabaseSingleton()
      {
         m_database = Resources.Load<ScriptableInstantiatedObjectDatabase>(m_databasePath);
         m_database.Init();
      }
   }
}
