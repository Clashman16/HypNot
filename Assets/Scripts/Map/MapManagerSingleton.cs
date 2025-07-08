using HypNot.Behaviours.Characters;
using System.Collections.Generic;

namespace HypNot.Map
{
   public sealed class MapManagerSingleton
   {
      private static MapManagerSingleton m_instance = null;

      public static MapManagerSingleton Instance
      {
         get
         {
            if (m_instance == null)
            {
               m_instance = new MapManagerSingleton();
            }
            return m_instance;
         }
      }

      private const int m_characterTypeEnumSize = 15;

      public int CharacterTypeEnumSize
      {
         get => m_characterTypeEnumSize;
      }

      private Dictionary<CharacterType, int> m_hypnotizedPersonTypes;

      public Dictionary<CharacterType, int> HypnotizedPersonTypes
      {
         get => m_hypnotizedPersonTypes;
      }

      private MapManagerSingleton()
      {
         m_hypnotizedPersonTypes = new Dictionary<CharacterType, int>();

         for(int l_i = 0; l_i < m_characterTypeEnumSize; l_i++)
         {
            m_hypnotizedPersonTypes.Add((CharacterType) l_i, 0);
         }
      }
   }
}
