using Pathfinding;
using System.Collections.Generic;
using UnityEngine;

namespace HypNot.Map
{
   public class TilemapDataBehaviour : CollidableDataBehaviour
   {
      public override void Start()
      {
         base.Start();

         RegisterCollidableToDatabase();
      }

      public void RegisterCollidableToDatabase()
      {
         CompositeCollider2D l_compositeCollider = GetComponent<CompositeCollider2D>();

         GridGraph l_gridGraph = AstarPath.active.data.gridGraph;

         int l_width = l_gridGraph.width;
         int l_depth = l_gridGraph.depth;
         float nodeSize = l_gridGraph.nodeSize;

         for (int l_x = 0; l_x < l_width; l_x++)
         {
            for (int l_z = 0; l_z < l_depth; l_z++)
            {
               GridNodeBase l_node = l_gridGraph.GetNode(l_x, l_z);

               Vector2 l_nodeWorldPosition = (Vector3)l_node.position;

               if (l_compositeCollider.OverlapPoint(l_nodeWorldPosition))
               {
                  if (!MapManagerSingleton.Instance.Collidables.ContainsKey(l_node))
                  {
                     MapManagerSingleton.Instance.Collidables[l_node] = new List<CollidableDataBehaviour>() { this };
                  }
                  else
                  {
                     MapManagerSingleton.Instance.Collidables[l_node].Add(this);
                  }
               }
            }
         }
      }
   }
}
