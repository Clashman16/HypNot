using HypNot.Behaviours.Characters;
using HypNot.Map;
using HypNot.Utils;
using System.Collections.Generic;

namespace HypNot.Spawners.Utils
{
   public class CharacterTypeRandomizer
   {
      private int m_maxExclusiveValue;

      private int m_lastValue;

      public CharacterTypeRandomizer()
      {
         m_lastValue = -1;

         m_maxExclusiveValue = MapManagerSingleton.Instance.CharacterTypeEnumSize;
      }

      private CharacterType Majority()
      {
         Dictionary<CharacterType, int> l_hypnotizedPersonTypes = MapManagerSingleton.Instance.HypnotizedPersonTypes;

         CharacterType l_majority = 0;

         foreach (KeyValuePair<CharacterType, int> l_pair in l_hypnotizedPersonTypes)
         {
            int l_majorityCount = l_hypnotizedPersonTypes[l_majority];
            int l_currentCount = l_pair.Value;

            if (l_currentCount > l_majorityCount || (l_currentCount == l_majorityCount && UnityEngine.Random.Range(0, 2) == 0))
            {
               l_majority = l_pair.Key;
            }
         }

         return l_majority;
      }

      public CharacterType Random()
      {
         m_lastValue = m_lastValue == -1 ? UnityEngine.Random.Range(0, m_maxExclusiveValue) : m_lastValue;

         int l_newValue = RandomIntHelper.GetRandomValue(m_lastValue, (int) Majority(), m_maxExclusiveValue);

         m_lastValue = l_newValue;

         return (CharacterType) l_newValue;
      }
   }
}
