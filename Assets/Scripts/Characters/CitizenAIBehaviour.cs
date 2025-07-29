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

      private AIPath m_aiPath;

      public AIPath AIPath
      {
         get => m_aiPath;
         set => m_aiPath = value;
      }

      private CitizenAnimatorBehaviour m_animator;

      private const float m_avoidanceAngle = 45f;

      private const float m_avoidanceDistance = 0.025f;

      private Vector2 m_firstDestination;

      public Vector2 FirstDestination
      {
         set => m_firstDestination = value;
      }

      private void Start()
      {
         m_animator = GetComponent<CitizenAnimatorBehaviour>();
      }

      private void Update()
      {
         if(PlayerStateSingleton.Instance.GameScreen != GameScreen.GAME_SCREEN)
         {
            m_aiPath.canMove = false;
            m_aiPath.canSearch = false;
            m_animator.IsDestinationReached = true;
         }
         else
         {
            Vector2 l_currentPosition = NodePositionConverter.ConvertToNodeWorldPosition(transform.position);

            if (m_aiPath.reachedDestination && m_aiPath.destination != (Vector3)m_firstDestination)
            {
               m_aiPath.destination = m_firstDestination;
               m_aiPath.SearchPath();
            }

            else if (Vector2.Distance(l_currentPosition, m_aiPath.destination) <= NodePositionConverter.NodeSize/2)
            {
               transform.position = m_aiPath.destination;
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
            if(m_aiPath.canMove && m_aiPath.canSearch)
            {
               OnTargetCollided();
            }
         }
         else if(m_data.IsMovable)
         {
            Vector2 l_curentDirection = (transform.position - p_collider.transform.position).normalized;

            Vector2 l_newDirection = m_avoidanceDistance * (transform.position + 
               Quaternion.Euler(0, 0, Random.Range(0, 1f) < 0.5 ? -m_avoidanceAngle : m_avoidanceAngle)
               * -l_curentDirection);

            m_aiPath.destination = l_newDirection;
            m_aiPath.SearchPath();
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

         m_aiPath.canMove = false;
         m_aiPath.canSearch = false;

         BecomeObstacle();

         m_target.Citizens.Add(m_data);

         m_target.Data.ManaCount -= 1;

         if (MapManagerSingleton.Instance.LastSpawnedCitizen == gameObject)
         {
            PlayerStateSingleton.Instance.CanSendCitizen = true;
         }

         HypnotizedPersonSpawnerSingleton.Instance.Spawn();
      }

      private void BecomeObstacle()
      {
         m_target.Data.OccupiedCitizenSpots[m_aiPath.destination] = true;

         GraphNode l_node = NodePositionConverter.GetNodeFromPosition(m_aiPath.destination);

         if (l_node != null && l_node.Walkable)
         {
            l_node.Walkable = false;
         }

         m_data.IsMovable = false;
      }
   }
}
