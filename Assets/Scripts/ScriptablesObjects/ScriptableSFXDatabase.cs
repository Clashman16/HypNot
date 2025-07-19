using System.Collections.Generic;
using UnityEngine;

namespace HypNot.ScriptableObjects
{
   [CreateAssetMenu(menuName = "ScriptableObjects/SFXDatabase")]
   public class ScriptableSFXDatabase : ScriptableObject
   {
      [SerializeField]
      private AudioClip m_endSound;

      public AudioClip EndSound
      {
         get => m_endSound;
      }

      [SerializeField]
      private AudioClip m_collisionSound;

      public AudioClip CollisionSound
      {
         get => m_collisionSound;
      }

      [SerializeField]
      private AudioClip m_oneMoreCitizenSound;

      public AudioClip OneMoreCitizenSound
      {
         get => m_oneMoreCitizenSound;
      }

      [SerializeField]
      private AudioClip m_buttonSound;

      public AudioClip ButtonSound
      {
         get => m_buttonSound;
      }

      [SerializeField]
      private AudioClip[] m_footstepsSounds;

      public AudioClip[] FootstepsSounds
      {
         get => m_footstepsSounds;
      }
   }
}
