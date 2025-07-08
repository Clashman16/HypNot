using HypNot.Map;
using System.Collections.Generic;
using UnityEngine;

namespace HypNot.Spawners
{
   public class FirstWaveSpawner : HypnotizedPersonWaveSpawner
   {
      private const string m_firstLevelZoneTag = "FirstLevelZone";

      private Dictionary<LevelZoneBehaviour, int> m_levelZones;

      public FirstWaveSpawner(double p_spawnRange) : base(p_spawnRange)
      {
         SetLevelZones();
      }

      public override void SetLevelZones()
      {
         if(m_levelZones == null)
         {
            m_levelZones = new Dictionary<LevelZoneBehaviour, int>();
         }
         else
         {
            m_levelZones.Clear();
         }

         GameObject l_firstLevelZone = GameObject.FindGameObjectWithTag(m_firstLevelZoneTag);

         m_levelZones.Add(l_firstLevelZone.GetComponent<LevelZoneBehaviour>(), l_firstLevelZone.transform.childCount);

         LevelZoneBehaviour[] l_levelZonesTemp = Object.FindObjectsByType<LevelZoneBehaviour>(FindObjectsSortMode.None);

         SpawnPointCount = 0;

         foreach (LevelZoneBehaviour l_zone in l_levelZonesTemp)
         {
            int l_localSpawnPointCount = l_zone.transform.childCount;

            if (!m_levelZones.ContainsKey(l_zone))
            {
               m_levelZones.Add(l_zone, l_localSpawnPointCount);
            }

            SpawnPointCount += l_localSpawnPointCount;
         }
      }

      public override void SpawnWave()
      {
         int l_zonesCount = m_levelZones.Count;

         int l_exploredZoneCount = 0;

         double[] l_spawnOccurency = GetSpawnOccurency(l_zonesCount);

         foreach (KeyValuePair<LevelZoneBehaviour, int> l_zone in m_levelZones)
         {
            int l_maxSpawnCount = Mathf.CeilToInt( l_zone.Value * (float) l_spawnOccurency[l_exploredZoneCount]);

            for (int l_i = 0; l_i < l_maxSpawnCount; l_i++)
            {
               HypnotizedPersonSpawnPointBehaviour l_spawnPoint = l_zone.Key.transform.GetChild(l_i).GetComponent<HypnotizedPersonSpawnPointBehaviour>();

               Spawn(l_spawnPoint);
            }

            l_exploredZoneCount += 1;
         }
      }

      private double[] GetSpawnOccurency(int p_zonesCount)
      {
         double[] l_SpawnOccurency = new double[p_zonesCount];
         l_SpawnOccurency[0] = 1 - SpawnRange;

         List<float> l_weights = new List<float>();
         Vector3 l_cameraPos = Camera.main.transform.position;

         float l_epsilon = 0.01f;
         float l_totalWeight = 0f;

         Dictionary<LevelZoneBehaviour, int>.Enumerator l_enumerator = m_levelZones.GetEnumerator();
         bool l_isFirstLevelZone = true;

         while (l_enumerator.MoveNext())
         {
            if (!l_isFirstLevelZone)
            {
               Vector3 l_zonePos = l_enumerator.Current.Key.transform.position;
               float l_distance = Vector3.Distance(l_zonePos, l_cameraPos);

               float l_weight = 1f / (l_distance + l_epsilon);
               l_weights.Add(l_weight);
               l_totalWeight += l_weight;
            }
            else
            {
               l_isFirstLevelZone = false;
            }
         }

         for (int l_i = 1; l_i < p_zonesCount; l_i++)
         {
            float l_normalized = l_weights[l_i - 1] / l_totalWeight;

            l_SpawnOccurency[l_i] = SpawnRange * l_normalized;
         }

         return l_SpawnOccurency;
      }
   }
}
