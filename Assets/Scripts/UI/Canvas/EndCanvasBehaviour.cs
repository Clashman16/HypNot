using HypNot.Sounds;
using HypNot.Utils;
using UnityEngine;

namespace HypNot.Behaviours.UI
{
   public class EndCanvasBehaviour : CanvasBehaviour
   {
      FinalScoreDisplayBehaviour m_finalScoreDisplay;

      AudioSource m_backgroundMusic;

      AudioSource m_sfx;

      public override void Reset()
      {
         if (m_backgroundMusic == null)
         {
            m_backgroundMusic = GameObject.FindGameObjectWithTag(TagDatabaseSingleton.Instance.BackgroundMusicPlayerTag).GetComponent<AudioSource>();
         }

         m_backgroundMusic.Stop();

         if(m_sfx == null)
         {
            m_sfx = GameObject.FindGameObjectWithTag(TagDatabaseSingleton.Instance.SFXPlayerTag).GetComponent<AudioSource>();
         }

         m_sfx.clip = SFXDatabaseSingleton.Instance.Database.EndSound;

         m_sfx.Play();

         if (m_finalScoreDisplay == null)
         {
            m_finalScoreDisplay = GetComponentInChildren<FinalScoreDisplayBehaviour>();
         }

         m_finalScoreDisplay.UpdateDisplay();

         base.Reset();
      }
   }
}
