using HypNot.Behaviours.UI;
using UnityEngine;

namespace HypNot.Player
{
   public sealed class PlayerScoreSingleton
   {
      private static PlayerScoreSingleton m_instance = null;

      private int m_score;

      CitizenIndicatorBehaviour m_scoreIndicator;

      public int Score
      {
         get => m_score;
         set
         {
            m_score = value;

            if(PlayerStateSingleton.Instance.GameState == GameState.PLAYING)
            {
               m_scoreIndicator.CitizenCoutDisplay.text = m_score < 10 ? "0" + m_score.ToString() : m_score.ToString();
            }
         }
      }

      public static PlayerScoreSingleton Instance
      {
         get
         {
            if (m_instance == null)
            {
               m_instance = new PlayerScoreSingleton();
            }
            return m_instance;
         }
      }

      private PlayerScoreSingleton()
      {
         m_score = 1;

         m_scoreIndicator = Object.FindObjectOfType<CitizenIndicatorBehaviour>(true);
      }
   }
}
