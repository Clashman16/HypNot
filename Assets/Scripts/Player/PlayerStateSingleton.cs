using HypNot.Behaviours.Characters;
using UnityEngine;

namespace HypNot.Player
{
   public sealed class PlayerStateSingleton
   {
      private static PlayerStateSingleton m_instance = null;

      private bool m_canSendCitizen;

      public bool CanSendCitizen
      {
         get => m_canSendCitizen;
         set => m_canSendCitizen = value;
      }

      private CharacterType m_firstCitizenType;

      public CharacterType FirstCitizenType
      {
         get => m_firstCitizenType;
      }

      private const int m_characterTypeEnumSize = 15;

      public static PlayerStateSingleton Instance
      {
         get
         {
            if (m_instance == null)
            {
               m_instance = new PlayerStateSingleton();
            }
            return m_instance;
         }
      }

      private PlayerStateSingleton()
      {
         m_canSendCitizen = true;

         m_firstCitizenType = (CharacterType) Random.Range(0, m_characterTypeEnumSize);
      }
   }
}
