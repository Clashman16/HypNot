using HypNot.Player;
using HypNot.Utils;
using UnityEngine;

namespace HypNot.Behaviours.UI
{
   public class GameCanvasBehaviour : CanvasBehaviour
   {
      private AudioSource m_backgroundMusic;

      private GameScreen m_lastGameScreen;

      private CitizenIndicatorBehaviour m_citizenIndicator;

      public GameScreen LastGameScreen
      {
         set => m_lastGameScreen = value;
      }

      public override void Reset()
      {
         if(m_backgroundMusic == null)
         {
            m_backgroundMusic = GameObject.FindGameObjectWithTag(TagDatabaseSingleton.Instance.BackgroundMusicPlayerTag).GetComponent<AudioSource>();
         }

         m_backgroundMusic.volume = PlayerSaveSingleton.Instance.BackgroundMusicVolume;

         if (!m_backgroundMusic.isPlaying)
         {
            if (m_lastGameScreen != GameScreen.PAUSE_SCREEN)
            {
               m_backgroundMusic.Play();
            }
            else
            {
               m_backgroundMusic.UnPause();
            }
         }

         if(m_citizenIndicator == null)
         {
            m_citizenIndicator = GetComponentInChildren<CitizenIndicatorBehaviour>();
         }

         m_citizenIndicator.Reset();

         SwipeController.enabled = true;
      }
   }
}
