using HypNot.Player;
using HypNot.Sounds;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HypNot.Behaviours.UI
{
   public class ArrowButtonBehaviour : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
   {
      private Vector2 m_direction;

      private GameObject m_target;

      private bool m_isPressed;

      private float m_moveSpeed = 4;

      private AudioSource m_audioPlayer;

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

         m_target = GameObject.FindGameObjectWithTag(PlayerStateSingleton.Instance.PlayerTag);

         AudioSource[] l_sources = GameObject.FindGameObjectWithTag(PlayerStateSingleton.Instance.PlayerTag).GetComponents<AudioSource>();

         foreach(AudioSource l_src in l_sources)
         {
            if(!l_src.clip.name.Contains("music"))
            {
               m_audioPlayer = l_src;
            }
         }
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

         m_audioPlayer.clip = SFXDatabaseSingleton.Instance.Database.ButtonSound;

         m_audioPlayer.Play();
      }

      public void OnPointerUp(PointerEventData eventData)
      {
         m_isPressed = false;
      }
   }
}

