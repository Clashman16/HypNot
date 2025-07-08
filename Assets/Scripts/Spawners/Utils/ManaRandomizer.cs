using System.Collections.Generic;
using UnityEngine;

namespace HypNot.Spawners.Utils
{
   public class ManaRandomizer
   {
      private const int m_maxExclusiveValue = 5;

      private double m_spawnRange;

      public ManaRandomizer(double p_spawnRange)
      {
         m_spawnRange = p_spawnRange;
      }

      public int Random(bool p_hasNeighbors)
      {
         Dictionary <int, int> l_chances = new Dictionary <int, int>();

         switch((int)m_spawnRange * 10)
         {
            case 2:
               l_chances.Add(2, 4);
               l_chances.Add(4, 3);
               l_chances.Add(6, 2);
               l_chances.Add(8, 1);
               break;
            case 4:
               l_chances.Add(2, 3);
               l_chances.Add(4, 4);
               l_chances.Add(6, 1);
               l_chances.Add(8, 2);
               break;
            case 6:
               l_chances.Add(2, 1);
               l_chances.Add(4, 3);
               l_chances.Add(6, 4);
               l_chances.Add(8, 2);
               break;
            default:
               l_chances.Add(2, 1);
               l_chances.Add(4, 2);
               l_chances.Add(6, 3);
               l_chances.Add(8, 4);
               break;
         }

         double l_roll = UnityEngine.Random.Range(0, 9);

         for (int l_i = 2; l_i < 10; l_i += 2)
         {
            if(l_roll < l_i)
            {
               if(l_chances[l_i] == 4 && p_hasNeighbors)
               {
                  return 3;
               }

               return l_chances[l_i];
            }
         }

         int l_mana = UnityEngine.Random.Range(1, m_maxExclusiveValue);

         if(l_mana == 4 && p_hasNeighbors)
         {
            return 3;
         }

         return l_mana;
      }
   }
}
