using HypNot.Player;
using TMPro;
using UnityEngine;

namespace HypNot.Behaviours.UI
{
   public class TimerBehaviour : MonoBehaviour
   {
      private TextMeshProUGUI m_timerDisplay;

      private float m_time;

      private void Start()
      {
         m_timerDisplay = GetComponentInChildren<TextMeshProUGUI>();

         m_time = PlayerSaveSingleton.Instance.TimerMax * 60f;
      }

      private void Update()
      {
         if (PlayerStateSingleton.Instance.GameScreen == GameScreen.GAME_SCREEN)
         {
            m_time -= Time.deltaTime;

            if (m_time <= 0)
            {
               m_time = 0;

               PlayerStateSingleton.Instance.GameScreen = GameScreen.END_SCREEN;
            }
         }

         UpdateDisplay();
      }

      private void UpdateDisplay()
      {
         m_timerDisplay.text = string.Format("{0:0}:{1:00}", Mathf.FloorToInt(m_time / 60f), Mathf.FloorToInt(m_time % 60f));
      }

      public void Reset()
      {
         m_time = PlayerSaveSingleton.Instance.TimerMax * 60f;
      }
   }
}
