using HypNot.Utils;
using UnityEngine;

namespace HypNot.Behaviours.UI
{
   public class MainMenuCanvasBehaviour : CanvasBehaviour
   {
      private AudioSource m_backgroundMusic;

      public override void Reset()
      {
         if (m_backgroundMusic == null)
         {
            m_backgroundMusic = GameObject.FindGameObjectWithTag(TagDatabaseSingleton.Instance.BackgroundMusicPlayerTag).GetComponent<AudioSource>();
         }

         m_backgroundMusic.Stop();

         base.Reset();
      }
   }
}
