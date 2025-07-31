using HypNot.Behaviours.UI;
using HypNot.Map;
using HypNot.Player;
using HypNot.Sounds;
using HypNot.Spawners;
using HypNot.Utils;
using Pathfinding;
using UnityEngine;

namespace HypNot.Behaviours.Characters
{
   public class CitizenAIBehaviour : MonoBehaviour
   {
      private CitizenDataBehaviour m_data;

      private HypnotizedPersonTargetBehaviour m_target;

      public HypnotizedPersonTargetBehaviour Target
      {
         set => m_target = value;
      }

      private AILerp m_aiLerp;

      public AILerp AILerp
      {
         get => m_aiLerp;
         set
         {
            m_aiLerp = value;

            if(m_obstacleAvoider == null)
            {
               m_obstacleAvoider = GetComponent<CitizenObstacleAvoiderBehaviour>();
            }

            m_obstacleAvoider.AILerp = m_aiLerp;

            if (m_animator == null)
            {
               m_animator = GetComponent<CitizenAnimatorBehaviour>();
            }
         }
      }

      Vector2 m_destination;

      public Vector2 Destination
      {
         set
         {
            m_destination = value;
            m_aiLerp.destination = value;
         }
      }

      private CitizenAnimatorBehaviour m_animator;

      private CitizenObstacleAvoiderBehaviour m_obstacleAvoider;

      private void Update()
      {
         if (PlayerStateSingleton.Instance.GameScreen != GameScreen.GAME_SCREEN)
         {
            m_aiLerp.canMove = false;
            m_aiLerp.canSearch = false;
            m_animator.IsDestinationReached = true;
         }
         else
         {
            if (m_obstacleAvoider.IsRetreating && m_aiLerp.reachedDestination && m_aiLerp.destination != (Vector3) m_destination)
            {
               m_obstacleAvoider.CalculateCustomPath(m_destination);
            }
            else
            {
               Vector2 l_currentPosition = NodePositionConverter.ConvertToNodeWorldPosition(transform.position);

               if (Vector2.Distance(l_currentPosition, m_destination) <= NodePositionConverter.NodeSize / 2)
               {
                  transform.position = m_destination;
               }
            }
         }
      }

      private void OnTriggerEnter2D(Collider2D p_collider)
      {
         if (m_data == null)
         {
            m_data = GetComponent<CitizenDataBehaviour>();
         }

         PlayCollisionSound();

         HypnotizedPersonTargetBehaviour l_collidedPerson = p_collider.GetComponent<HypnotizedPersonTargetBehaviour>();

         if (l_collidedPerson != null && m_target == l_collidedPerson)
         {
            if(m_aiLerp.canMove && m_aiLerp.canSearch)
            {
               OnTargetCollided();
            }
         }
         else if(m_data.IsMovable)
         {
            m_obstacleAvoider.GoBack(transform.position);
         }
      }

      private void PlayCollisionSound()
      {
         AudioSource l_audioPlayer = GetComponent<AudioSource>();

         l_audioPlayer.clip = SFXDatabaseSingleton.Instance.Database.CollisionSound;

         l_audioPlayer.Play();
      }

      private void OnTargetCollided()
      {
         m_animator.IsDestinationReached = true;

         m_aiLerp.canMove = false;
         m_aiLerp.canSearch = false;

         BecomeObstacle();

         m_target.OnCollided(m_data);

         if (MapManagerSingleton.Instance.LastSpawnedCitizen == gameObject)
         {
            PlayerStateSingleton.Instance.CanSendCitizen = true;
         }

         HypnotizedPersonSpawnerSingleton.Instance.Spawn();
      }

      private void BecomeObstacle()
      {
         m_target.Data.OccupiedCitizenSpots[m_aiLerp.destination] = true;

         GraphNode l_node = NodePositionConverter.GetNodeFromPosition(m_aiLerp.destination);

         if (l_node != null && l_node.Walkable)
         {
            l_node.Walkable = false;
         }

         m_data.IsMovable = false;
      }
   }
}
