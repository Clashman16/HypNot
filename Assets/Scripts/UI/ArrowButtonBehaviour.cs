using HypNot.Player;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HypNot.Behaviours.UI
{
   public class ArrowButtonBehaviour : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
   {
      private Vector2 m_direction;

      private GameObject m_target;

      private bool m_isPressed;

      private const string m_playerTag = "Player";

      private float m_moveSpeed = 4;

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

         m_target = GameObject.FindGameObjectWithTag(m_playerTag);
      }

      private void Update()
      {
         if (m_isPressed && PlayerStateSingleton.Instance.GameScreen == GameScreen.GAME_SCREEN)
         {
            ApplyDirection();
         }
      }

      private void ApplyDirection()
      {
         m_target.transform.position += (Vector3) m_direction * Time.deltaTime * m_moveSpeed;
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

