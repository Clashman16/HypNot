using HypNot.Behaviours.Utils;
using HypNot.Sounds;
using Pathfinding;
using UnityEngine;

namespace HypNot.Behaviours.Characters
{
   public class CitizenAnimatorBehaviour : MonoBehaviour
   {
      private Animator m_animator;

      private AILerp m_aiLerp;

      private bool m_isDestinationReached;

      public bool IsDestinationReached
      {
         set => m_isDestinationReached = value;
      }

      private bool m_isMoving;

      private bool m_lastIsMoving;

      private Direction m_currentDirection;

      private Direction m_lastDirection;

      private int m_animIndex;

      FootStepAudioPlayerBehaviour m_audioPlayer;

      private void UpdateAnimator()
      {
         int l_animIndex = (int) m_currentDirection;

         if (m_isMoving)
         {
            l_animIndex += 4;
         }

         if(m_animIndex != l_animIndex)
         {
            m_animIndex = l_animIndex;

            m_animator.SetInteger("animIndex", m_animIndex);
         }
      }

      private void Start()
      {
         m_isDestinationReached = false;

         m_isMoving = false;
         m_lastIsMoving = false;

         m_animIndex = 0;

         m_animator = GetComponent<Animator>();

         m_currentDirection = Direction.DOWN;
         m_lastDirection = m_currentDirection;

         m_audioPlayer = GetComponent<FootStepAudioPlayerBehaviour>();
      }

      void Update()
      {
         if(m_isDestinationReached)
         {
            m_isMoving = false;
         }
         else
         {
            if (m_aiLerp == null)
            {
               m_aiLerp = GetComponent<CitizenAIBehaviour>().AILerp;
            }

            m_isMoving = true;

            Vector3 l_velocity = m_aiLerp.velocity;

            if (Mathf.Abs(l_velocity.x) > Mathf.Abs(l_velocity.y))
            {
               l_velocity.y = 0;
            }
            else
            {
               l_velocity.x = 0;
            }

            if (l_velocity.x > 0)
            {
               m_currentDirection = Direction.RIGHT;
            }
            else if (l_velocity.x < 0)
            {
               m_currentDirection = Direction.LEFT;
            }
            else if (l_velocity.y > 0)
            {
               m_currentDirection = Direction.UP;
            }
            else if (l_velocity.y < 0)
            {
               m_currentDirection = Direction.DOWN;
            }
            else
            {
               float l_speed = l_velocity.magnitude;

               if (Mathf.Approximately(l_speed, 0))
               {
                  m_isMoving = false;
               }
            }
         }

         if (m_lastIsMoving != m_isMoving || m_currentDirection != m_lastDirection)
         {
            m_lastIsMoving = m_isMoving;
            m_lastDirection = m_currentDirection;

            UpdateAnimator();
         }

         if(m_isMoving && !m_audioPlayer.IsPlaying)
         {
            m_audioPlayer.Play();
         }
      }
   }
}
