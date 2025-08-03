using HypNot.Behaviours.Utils;
using UnityEngine;

namespace HypNot.Player
{
   public class SwipeControllerBehaviour : MonoBehaviour
   {
      public float m_swipeThreshold = 50f;

      private Vector2 m_startTouchPosition;

      private bool m_isMoving = false;

      private Vector3 m_targetPosition;

      private Vector3[] m_roomPositions;

      private int m_currentRoomIndex;

      void Start()
      {
         m_roomPositions = new Vector3[]
         {
            new Vector3(-5, 10, 0),
            new Vector3(0, 10, 0),
            new Vector3(5, 10, 0),
            new Vector3(-5, 0, 0),
            Vector3.zero,
            new Vector3(5, 0, 0)
         };

         m_currentRoomIndex = 4;

         transform.position = m_roomPositions[m_currentRoomIndex];
      }

      void Update()
      {
         HandleSwipe();

         if (m_isMoving)
         {
            transform.position = m_targetPosition;
            m_isMoving = false;
         }
      }

      private void HandleSwipe()
      {
         if (Input.touchCount > 0)
         {
            Touch l_touch = Input.GetTouch(0);

            if (l_touch.phase == TouchPhase.Began)
            {
               m_startTouchPosition = l_touch.position;
            }
            else if (l_touch.phase == TouchPhase.Ended)
            {
               Vector2 l_endTouchPosition = l_touch.position;

               Vector2 l_swipeDelta = l_endTouchPosition - m_startTouchPosition;

               if (l_swipeDelta.magnitude > m_swipeThreshold)
               {
                  Direction l_swipeDirection;

                  if (Mathf.Abs(l_swipeDelta.x) > Mathf.Abs(l_swipeDelta.y))
                  {
                     l_swipeDirection = l_swipeDelta.x > 0 ? Direction.LEFT : Direction.RIGHT;
                  }
                  else
                  {
                     l_swipeDirection = l_swipeDelta.y > 0 ? Direction.DOWN : Direction.UP;
                  }

                  MoveToRoom(l_swipeDirection);
               }
            }
         }
      }

      private void MoveToRoom(Direction p_swipeDirection)
      {
         int l_newIndex = m_currentRoomIndex;

         switch (m_currentRoomIndex)
         {
            case 0:
               if (p_swipeDirection == Direction.RIGHT)
               {
                  l_newIndex = 1;
               }
               else if (p_swipeDirection == Direction.DOWN)
               {
                  l_newIndex = 3;
               }
               break;
            case 1:
               if (p_swipeDirection == Direction.LEFT)
               {
                  l_newIndex = 0;
               }
               else if (p_swipeDirection == Direction.RIGHT)
               {
                  l_newIndex = 2;
               }
               else if (p_swipeDirection == Direction.DOWN)
               {
                  l_newIndex = 4;
               }
               break;
            case 2:
               if (p_swipeDirection == Direction.LEFT)
               {
                  l_newIndex = 1;
               }
               else if (p_swipeDirection == Direction.DOWN)
               {
                  l_newIndex = 5;
               }
               break;
            case 3:
               if (p_swipeDirection == Direction.RIGHT)
               {
                  l_newIndex = 4;
               }
               else if (p_swipeDirection == Direction.UP)
               {
                  l_newIndex = 0;
               }
               break;
            case 4:
               if (p_swipeDirection == Direction.LEFT)
               {
                  l_newIndex = 3;
               }
               else if (p_swipeDirection == Direction.RIGHT)
               {
                  l_newIndex = 5;
               }
               else if (p_swipeDirection == Direction.UP)
               {
                  l_newIndex = 1;
               }
               break;
            case 5:
               if (p_swipeDirection == Direction.LEFT)
               {
                  l_newIndex = 4;
               }
               else if (p_swipeDirection == Direction.UP)
               {
                  l_newIndex = 2;
               }
               break;
         }

         if (l_newIndex != m_currentRoomIndex)
         {
            m_currentRoomIndex = l_newIndex;

            m_targetPosition = m_roomPositions[m_currentRoomIndex];

            m_isMoving = true;
         }
      }
   }
}