using HypNot.Behaviours.Utils;
using HypNot.Utils;
using Pathfinding;
using System.Collections.Generic;
using UnityEngine;

namespace HypNot.Map
{
   public class CollidableDataBehaviour : MonoBehaviour
   {
      GraphNode m_occupiedNode;

      public GraphNode OccupiedNode
      {
         get => m_occupiedNode;
         set => m_occupiedNode = value;
      }

      private bool m_isMovable;

      public bool IsMovable
      {
         get => m_isMovable;
         set => m_isMovable = value;
      }

      private SpriteRenderer m_renderer;

      public int OrderInLayer
      {
         get
         {
            if(m_renderer == null)
            {
               m_renderer = GetComponent<SpriteRenderer>();
            }

            return m_renderer.sortingOrder;
         }

         set
         {
            if (m_renderer == null)
            {
               m_renderer = GetComponent<SpriteRenderer>();
            }

            m_renderer.sortingOrder = value;
         }
      }

      public virtual void Start()
      {
         m_isMovable = false;
      }

      void Update()
      {
         if(m_isMovable)
         {
            Vector2 l_currentPosition = transform.position;

            GraphNode l_currentNode = NodePositionConverter.GetNodeFromPosition(l_currentPosition);

            if (l_currentNode != m_occupiedNode)
            {
               MapManagerSingleton.Instance.RemoveCollidable(this);

               m_occupiedNode = l_currentNode;

               MapManagerSingleton.Instance.AddCollidable(this);

               if (MapManagerSingleton.Instance.Collidables.TryGetValue(m_occupiedNode, out List<CollidableDataBehaviour> l_mainNeighborhood))
               {
                  foreach (CollidableDataBehaviour l_collidable in l_mainNeighborhood)
                  {
                     if (l_collidable != this)
                     {
                        ChangeRenderingOrder(l_collidable);
                     }
                  }
               }

               Vector2 l_nodePosition = (Vector3) m_occupiedNode.position;

               for (int l_i = 0; l_i < 8; l_i++)
               {
                  Vector2 l_neighborhoodPosition = NodePositionConverter.GetNodeNeighborPosition(l_nodePosition, (Direction) l_i);
                  GraphNode l_node = NodePositionConverter.GetNodeFromPosition(l_neighborhoodPosition);

                  if (MapManagerSingleton.Instance.Collidables.TryGetValue(l_node, out List<CollidableDataBehaviour> l_neighborhood))
                  {
                     foreach (CollidableDataBehaviour l_collidable in l_neighborhood)
                     {
                        if (l_collidable != this)
                        {
                           ChangeRenderingOrder(l_collidable);
                        }
                     }
                  }
               }
            }
         }
      }

      private void ChangeRenderingOrder(CollidableDataBehaviour p_collidable)
      {
         Vector2 l_position = p_collidable.transform.position;
         Vector2 l_thisPosition = transform.position;

         if (l_position.y > l_thisPosition.y)
         {
            OrderInLayer = p_collidable.OrderInLayer + 1;
         }
         else
         {
            OrderInLayer = p_collidable.OrderInLayer - 1;
         }
      }
   }
}
