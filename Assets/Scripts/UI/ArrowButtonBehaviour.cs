using UnityEngine;
using UnityEngine.EventSystems;

namespace HypNot.Behaviours.UI
{
   public class ArrowButtonBehaviour : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
   {
      private Vector2 m_direction;

      private Camera m_camera;

      private bool m_isPressed;

      private void Start()
      {
         float l_rotation = transform.rotation.eulerAngles.z;

         if(Mathf.Approximately(l_rotation, 90))
         {
            m_direction = Vector2.down;
         }
         else if(Mathf.Approximately(l_rotation, 180))
         {
            m_direction = Vector2.right;
         }
         else if(Mathf.Approximately(l_rotation, 270))
         {
            m_direction = Vector2.up;
         }
         else
         {
            m_direction = Vector2.left;
         }

         m_camera = Camera.main;
      }

      private void Update()
      {
         if (m_isPressed)
         {
            ApplyDirection();
         }
      }

      private void ApplyDirection()
      {
         m_camera.transform.position += (Vector3) m_direction * Time.deltaTime * 2;
      }

      public void OnPointerDown(PointerEventData eventData)
      {
         m_isPressed = true;
      }

      public void OnPointerUp(PointerEventData eventData)
      {
         m_isPressed = false;
      }
   }
}

