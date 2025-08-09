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

      private const int m_sortMultiplier = 100;

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

      private Renderer m_renderer;

      public int OrderInLayer
      {
         get
         {
            if(m_renderer == null)
            {
               m_renderer = GetComponent<Renderer>();
            }

            return m_renderer.sortingOrder;
         }

         set
         {
            if (m_renderer == null)
            {
               m_renderer = GetComponent<Renderer>();
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
            ChangeRenderingOrder();
         }
      }

      private void ChangeRenderingOrder()
      {
         if (m_renderer == null)
         {
            m_renderer = GetComponent<Renderer>();
         }

         float l_y = m_renderer.bounds.min.y;
         int l_order = Mathf.RoundToInt(-l_y * m_sortMultiplier);

         OrderInLayer = l_order;
      }
   }
}
