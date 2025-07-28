using HypNot.Behaviours.Characters;
using Pathfinding;
using UnityEngine;

namespace HypNot.Utils
{
   public static class NodePositionConverter
   {
      public static Vector2 ConvertToNodeWorldPosition(Vector2 p_position)
      {
         AstarPath l_activePath = AstarPath.active;

         GridGraph l_graph = l_activePath.data.gridGraph;

         return (Vector3)l_graph.GetNearest(p_position).node.position;
      }

      public static float NodeSize
      {
         get
         {
            return AstarPath.active.data.gridGraph.nodeSize;
         }
      }

      public static Vector2 GetNodeNeighborPosition(Vector2 p_NodePosition, CharacterDirection p_direction)
      {
         Vector2 l_directionVector;

         switch (p_direction)
         {
            case CharacterDirection.UP:
               l_directionVector = new Vector2(0, NodeSize);
               break;
            case CharacterDirection.DOWN:
               l_directionVector = new Vector2(0, -NodeSize);
               break;
            case CharacterDirection.LEFT:
               l_directionVector = new Vector2(-NodeSize, 0);
               break;
            default:
               l_directionVector = new Vector2(NodeSize, 0);
               break;
         }

         return ConvertToNodeWorldPosition(p_NodePosition + l_directionVector);
      }

      public static GraphNode GetNodeFromPosition(Vector2 p_position)
      {
         AstarPath l_activePath = AstarPath.active;

         GridGraph l_graph = l_activePath.data.gridGraph;

         return l_graph.GetNearest(p_position).node;
      }
   }
}
