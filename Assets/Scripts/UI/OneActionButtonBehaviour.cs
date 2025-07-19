using HypNot.Player;
using UnityEngine;

namespace HypNot.Behaviours.UI
{
   public abstract class OneActionButtonBehaviour : MonoBehaviour
   {
      private AudioSource m_audioPlayer;

      public virtual void Act()
      {
         if(m_audioPlayer == null)
         {
            AudioSource[] l_sources = GameObject.FindGameObjectWithTag(PlayerStateSingleton.Instance.PlayerTag).GetComponents<AudioSource>();

            foreach (AudioSource l_src in l_sources)
            {
               if (l_src.clip.name.Contains("button"))
               {
                  m_audioPlayer = l_src;
               }
            }
         }

         m_audioPlayer.Play();
      }
   }
}
