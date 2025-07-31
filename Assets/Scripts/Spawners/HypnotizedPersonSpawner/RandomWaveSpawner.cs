using HypNot.Map;
using System.Collections.Generic;
using UnityEngine;

namespace HypNot.Spawners
{
   public class RandomWaveSpawner : HypnotizedPersonWaveSpawner
   {
      private Camera m_camera;

      private List<HypnotizedPersonSpawnPointBehaviour> m_spawnPoints;

      public RandomWaveSpawner(double p_spawnRange, Queue<GameObject> p_recycleBin) : base(p_spawnRange)
      {
         m_camera = Camera.main;

         PasteRecycleBin(p_recycleBin);
      }

      public override void SetLevelZones()
      {
         if(m_spawnPoints == null)
         {
            m_spawnPoints = new List<HypnotizedPersonSpawnPointBehaviour>();
         }
         else
         {
            m_spawnPoints.Clear();
         }

         LevelZoneBehaviour[] l_levelZonesTemp = Object.FindObjectsByType<LevelZoneBehaviour>(FindObjectsSortMode.None);

         Vector2 l_bottomLeft = m_camera.ViewportToWorldPoint(new Vector2(0, 0));
         Vector2 l_topRight = m_camera.ViewportToWorldPoint(new Vector2(1, 1));

         Rect l_camRect = new Rect(
             l_bottomLeft.x,
             l_bottomLeft.y,
             l_topRight.x - l_bottomLeft.x,
             l_topRight.y - l_bottomLeft.y
         );

         SpawnPointCount = 0;

         foreach (LevelZoneBehaviour l_zone in l_levelZonesTemp)
         {
            Transform l_zoneTrf = l_zone.transform;

            Vector2 l_zoneCenter = l_zoneTrf.position;

            Vector2 l_zoneSize = l_zone.Size;

            Rect l_zoneRect = new Rect(
               l_zoneCenter.x - l_zoneSize.x / 2f,
               l_zoneCenter.y - l_zoneSize.y / 2f,
               l_zoneSize.x,
               l_zoneSize.y
            );

            if (!l_camRect.Overlaps(l_zoneRect, true))
            {
               for(int l_i = 0; l_i  < l_zoneTrf.childCount; l_i++)
               {
                  HypnotizedPersonSpawnPointBehaviour l_spawnPoint = l_zoneTrf.GetChild(l_i).GetComponent<HypnotizedPersonSpawnPointBehaviour>();

                  if(!l_spawnPoint.IsSlotOccupied)
                  {
                     m_spawnPoints.Add(l_spawnPoint);
                     SpawnPointCount += 1;
                  }  
               }
            }
         }
      }

      public override void SpawnWave()
      {
         SetLevelZones();

         int l_maxSpawnCount = (int) (SpawnPointCount * SpawnRange);

         for (int l_i = 0; l_i < l_maxSpawnCount; l_i++)
         {
            HypnotizedPersonSpawnPointBehaviour l_spawnPoint = m_spawnPoints[l_i];

            Spawn(l_spawnPoint);
         }
      }
   }
}
