using Pathfinding;
using UnityEngine;

namespace HypNot.Behaviours.Characters
{
   public class CitizenAnimatorBehaviour : MonoBehaviour
   {
      private Animator m_animator;

      private AIPath m_aiPath;

      private bool m_isDestinationReached;

      public bool IsDestinationReached
      {
         set => m_isDestinationReached = value;
      }

      private bool m_isMoving;

      private bool m_lastIsMoving;

      private CharacterDirection m_currentDirection;

      private CharacterDirection m_lastDirection;

      private int m_animIndex;

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

         m_currentDirection = CharacterDirection.DOWN;
         m_lastDirection = m_currentDirection;
      }

      void Update()
      {
         if(m_isDestinationReached)
         {
            m_isMoving = false;
         }
         else
         {
            if (m_aiPath == null)
            {
               m_aiPath = GetComponent<CitizenAIBehaviour>().AIPath;
            }

            m_isMoving = true;

            Vector3 l_velocity = m_aiPath.velocity;

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
               m_currentDirection = CharacterDirection.RIGHT;
            }
            else if (l_velocity.x < 0)
            {
               m_currentDirection = CharacterDirection.LEFT;
            }
            else if (l_velocity.y > 0)
            {
               m_currentDirection = CharacterDirection.UP;
            }
            else if (l_velocity.y < 0)
            {
               m_currentDirection = CharacterDirection.DOWN;
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
      }
   }
}
