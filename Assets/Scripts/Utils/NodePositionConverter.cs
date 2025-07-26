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
   }
}
