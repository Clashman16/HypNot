using TMPro;
using UnityEngine;

namespace HypNot.Player
{
   public class SwipeControllerBehaviour : MonoBehaviour
   {
      #region Swipe Data
      private float m_swipeThreshold;

      private Vector2 m_startTouchPosition;

      private bool m_isMoving = false;

      private Vector3 m_targetPosition;

      private float m_maxPushDistance;

      private float m_sensitivity;

      #endregion

      #region Clamp Data

      private Camera m_mainCamera;

      private Bounds m_moveArea;

      #endregion

      void Start()
      {
         m_swipeThreshold = 50f;

         m_maxPushDistance = 3;

         m_sensitivity = 0.02f;

         m_mainCamera = Camera.main;

         m_moveArea = FindObjectOfType<PolygonCollider2D>().bounds;
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
                  Vector3 l_pushDirection = Vector3.zero;

                  if (Mathf.Abs(l_swipeDelta.x) > Mathf.Abs(l_swipeDelta.y))
                  {

                     l_pushDirection = (l_swipeDelta.x > 0) ? Vector3.left : Vector3.right;
                  }
                  else
                  {
                     l_pushDirection = (l_swipeDelta.y > 0) ? Vector3.down : Vector3.up;
                  }

                  float l_pushDistance = Mathf.Min(l_swipeDelta.magnitude * m_sensitivity, m_maxPushDistance);

                  Vector3 l_newTarget = m_targetPosition + l_pushDirection * l_pushDistance;

                  l_newTarget.x = Mathf.Clamp(l_newTarget.x, m_moveArea.min.x, m_moveArea.max.x);
                  l_newTarget.y = Mathf.Clamp(l_newTarget.y, m_moveArea.min.y, m_moveArea.max.y);

                  m_targetPosition = l_newTarget;

                  m_isMoving = true;
               }
            }
         }
      }
   }
}