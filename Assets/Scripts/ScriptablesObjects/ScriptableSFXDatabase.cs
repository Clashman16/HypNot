using System.Collections.Generic;
using UnityEngine;

namespace HypNot.ScriptableObjects
{
   [CreateAssetMenu(menuName = "ScriptableObjects/SFXDatabase")]
   public class ScriptableSFXDatabase : ScriptableObject
   {
      [SerializeField]
      private AudioClip m_victorySound;

      [SerializeField]
      private AudioClip m_defeatSound;

      [SerializeField]
      private AudioClip[] m_footstepsSounds;

      public AudioClip[] FootstepsSounds
      {
         get => m_footstepsSounds;
      }
   }
}
