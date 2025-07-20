using HypNot.Behaviours.Characters;
using HypNot.Behaviours.UI;
using HypNot.Map;
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

      private GameScreen m_gameScreen;

      public GameScreen GameScreen
      {
         get => m_gameScreen;
         set => m_gameScreen = value;
      }


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

         m_firstCitizenType = (CharacterType)Random.Range(0, MapManagerSingleton.Instance.CharacterTypeEnumSize);
      }

      public void Reset()
      {
         m_canSendCitizen = true;

         m_firstCitizenType = (CharacterType)Random.Range(0, MapManagerSingleton.Instance.CharacterTypeEnumSize);
      }
   }
}
