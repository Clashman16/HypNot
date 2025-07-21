using HypNot.Player;
using HypNot.Sounds;
using HypNot.Utils;
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
            m_audioPlayer = GameObject.FindGameObjectWithTag(TagDatabaseSingleton.Instance.SFXPlayerTag).GetComponent<AudioSource>();
         }

         m_audioPlayer.volume = PlayerSaveSingleton.Instance.SFXVolume;

         m_audioPlayer.clip = SFXDatabaseSingleton.Instance.Database.ButtonSound;

         m_audioPlayer.Play();
      }
   }
}
