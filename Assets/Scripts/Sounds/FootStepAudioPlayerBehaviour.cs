using HypNot.Player;
using HypNot.Utils;
using UnityEngine;

namespace HypNot.Sounds
{
   public class FootStepAudioPlayerBehaviour : MonoBehaviour
   {
      AudioSource m_audioPlayer;

      AudioClip[] m_footstepsSounds;

      private int m_footstepsIndex;

      private void Start()
      {
         m_audioPlayer = GetComponent<AudioSource>();

         m_audioPlayer.volume = PlayerSaveSingleton.Instance.SFXVolume;

         m_footstepsSounds = SFXDatabaseSingleton.Instance.Database.FootstepsSounds;

         m_footstepsIndex = Random.Range(0, m_footstepsSounds.Length);
      }

      public bool IsPlaying
      {
         get => m_audioPlayer.isPlaying;
      }

      public void Play()
      {
         m_audioPlayer.clip = m_footstepsSounds[m_footstepsIndex];

         m_audioPlayer.Play();

         m_footstepsIndex = RandomIntHelper.GetRandomValue(m_footstepsIndex, m_footstepsIndex, m_footstepsSounds.Length);
      }
   }
}
