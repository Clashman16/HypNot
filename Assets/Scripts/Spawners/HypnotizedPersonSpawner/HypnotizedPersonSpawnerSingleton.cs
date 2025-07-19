using HypNot.Behaviours.UI;
using HypNot.Player;
using System.Collections.Generic;
using UnityEngine;

namespace HypNot.Spawners
{
   public sealed class HypnotizedPersonSpawnerSingleton
   {
      private static HypnotizedPersonSpawnerSingleton m_instance = null;

      public static HypnotizedPersonSpawnerSingleton Instance
      {
         get
         {
            if (m_instance == null)
            {
               m_instance = new HypnotizedPersonSpawnerSingleton();
            }
            return m_instance;
         }
      }

      HypnotizedPersonWaveSpawner m_waveSpawner;

      public HypnotizedPersonWaveSpawner WaveSpawner
      {
         get => m_waveSpawner;
      }

      double m_spawnRange;

      public HypnotizedPersonSpawnerSingleton() : base()
      {
         SetSpawnRange();

         m_waveSpawner = new FirstWaveSpawner(m_spawnRange);
      }

      private void SetSpawnRange()
      {
         int l_difficulty = PlayerSaveSingleton.Instance.Difficulty;

         switch (l_difficulty)
         {
            case 4:
            case 5:
               m_spawnRange = 0.8;
               break;
            default:
               m_spawnRange = (double) l_difficulty / 5; // 0.2, 0.4 or 0.6
               break;
         }
      }

      public void Spawn()
      {
         if (PlayerStateSingleton.Instance.GameScreen == GameScreen.GAME_SCREEN)
         {
            m_waveSpawner.SpawnWave();

            // If the first wave appeared
            if (m_waveSpawner.GetType() == typeof(FirstWaveSpawner))
            {
               Queue<GameObject> l_recycleBin = new Queue<GameObject>();
               m_waveSpawner.CopyRecycleBin(l_recycleBin);

               m_waveSpawner = new RandomWaveSpawner(m_spawnRange, l_recycleBin);
            }
         }
      }
   }
}
