using HypNot.Behaviours.Utils;
using HypNot.Utils;
using Pathfinding;
using System.Collections.Generic;
using UnityEngine;

namespace HypNot.Behaviours.Characters
{
   public class CitizenObstacleAvoiderBehaviour : Seeker
   {
      private AILerp m_aiLerp;

      public AILerp AILerp
      {
         set => m_aiLerp = value;
      }

      private bool m_isRetreating;

      public bool IsRetreating
      {
         get => m_isRetreating;
      }

      private List<Vector2> m_encounteredObstacles;

      public List<Vector2> EncounteredObstacles
      {
         get => m_encounteredObstacles;
      }

      private const float m_avoidanceAngle = 45f;

      private const float m_avoidanceDistance = 60f;

      private const int m_maxRetryAttempts = 8;

      private Vector2 m_spawnPosition;

      private void Start()
      {
         m_encounteredObstacles = new List<Vector2>();

         m_isRetreating = false;

         m_spawnPosition = transform.position;
      }

      public void CalculateCustomPath(Vector2 p_targetPosition)
      {
         foreach (Vector2 l_obstacle in m_encounteredObstacles)
         {
            Bounds l_bounds = new Bounds(l_obstacle, new Vector3(3f, 3f, 1f));

            GraphUpdateObject l_GraphUpdateObject = new GraphUpdateObject(l_bounds)
            {
               addPenalty = 10,
               updatePhysics = false
            };

            AstarPath.active.UpdateGraphs(l_GraphUpdateObject);
         }

         m_aiLerp.destination = p_targetPosition;

         m_aiLerp.SearchPath();

         m_isRetreating = false;
      }

      public void GoBack(Vector3 p_obstaclePosition)
      {
         Vector2 l_obstacleNodePosition = NodePositionConverter.ConvertToNodeWorldPosition(p_obstaclePosition);

         m_encounteredObstacles.Add(l_obstacleNodePosition);

         Vector2 l_newDestination = FindValidRetreatPosition(l_obstacleNodePosition);

         GraphNode l_node = NodePositionConverter.GetNodeFromPosition(p_obstaclePosition);
         if (l_node != null && l_node.Walkable)
         {
            l_node.Walkable = false;
         }

         m_aiLerp.destination = l_newDestination;
         m_aiLerp.SearchPath();
         

         m_isRetreating = true;
      }

      private Vector2 FindValidRetreatPosition(Vector2 p_obstaclePosition)
      {
         Vector2 l_currentDirection = transform.position - (Vector3) p_obstaclePosition;

         List<Direction> l_directionsToBan = new List<Direction>();

         float l_x = l_currentDirection.x;

         if (!Mathf.Approximately(l_x, 0))
         {
            if(l_x > 0)
            {
               l_directionsToBan.Add(Direction.RIGHT);
               l_directionsToBan.Add(Direction.UP_RIGHT);
               l_directionsToBan.Add(Direction.DOWN_RIGHT);
            }
            else
            {
               l_directionsToBan.Add(Direction.LEFT);
               l_directionsToBan.Add(Direction.UP_LEFT);
               l_directionsToBan.Add(Direction.DOWN_LEFT);
            }
         }

         float l_y = l_currentDirection.y;

         if (!Mathf.Approximately(l_y, 0))
         {
            if (l_y > 0)
            {
               l_directionsToBan.Add(Direction.UP);

               if(!l_directionsToBan.Contains(Direction.UP_RIGHT))
               {
                  l_directionsToBan.Add(Direction.UP_RIGHT);
               }
               if (!l_directionsToBan.Contains(Direction.UP_LEFT))
               {
                  l_directionsToBan.Add(Direction.UP_LEFT);
               }
            }
            else
            {
               l_directionsToBan.Add(Direction.DOWN);

               if (!l_directionsToBan.Contains(Direction.DOWN_RIGHT))
               {
                  l_directionsToBan.Add(Direction.DOWN_RIGHT);
               }
               if (!l_directionsToBan.Contains(Direction.DOWN_LEFT))
               {
                  l_directionsToBan.Add(Direction.DOWN_LEFT);
               }
            }
         }

         List<Vector2> l_nodPositions = new List<Vector2>();

         for (int l_i = 0; l_i < 8; l_i++)
         {
            Direction l_direction = (Direction) l_i;

            if(!l_directionsToBan.Contains(l_direction))
            {
               l_nodPositions.Add(NodePositionConverter.GetNodeNeighborPosition(p_obstaclePosition, l_direction));
            }
         }

         l_nodPositions.Sort((l_a, l_b) => Vector3.Distance(transform.position, l_a).CompareTo(Vector3.Distance(transform.position, l_b)));

         return l_nodPositions[0];
      }
   }
}
