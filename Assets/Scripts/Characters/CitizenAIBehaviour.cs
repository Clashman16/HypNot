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
      private CharacterType m_type;

      public CharacterType Type
      {
         get => m_type;
         set => m_type = value;
      }

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
      }

      private void OnTriggerEnter2D(Collider2D p_collider)
      {
         PlayCollisionSound();

         HypnotizedPersonTargetBehaviour l_collidedPerson = p_collider.GetComponent<HypnotizedPersonTargetBehaviour>();;

         if (l_collidedPerson != null && m_target == l_collidedPerson)
         {
            OnTargetCollided();
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

         m_target.Citizens.Add(this);

         m_target.Data.ManaCount -= 1;

         if (MapManagerSingleton.Instance.LastSpawnedCitizen == gameObject)
         {
            PlayerStateSingleton.Instance.CanSendCitizen = true;
         }

         HypnotizedPersonSpawnerSingleton.Instance.Spawn();
      }

      private void BecomeObstacle()
      {
         transform.position = NodePositionConverter.ConvertToNodeWorldPosition(transform.position);

         Bounds l_bounds = GetComponent<BoxCollider2D>().bounds;

         GraphUpdateObject l_graphUpdateObject = new GraphUpdateObject(l_bounds)
         {
            modifyWalkability = true,
            setWalkability = false,
            updatePhysics = false
         };

         AstarPath l_activePath = AstarPath.active;
         l_activePath.UpdateGraphs(l_graphUpdateObject);
         l_activePath.Scan();
      }
   }
}
