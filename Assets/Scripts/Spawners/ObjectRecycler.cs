using System.Collections.Generic;
using UnityEngine;

namespace HypNot.Spawners
{
   public class ObjectRecycler
   {
      private const int m_maxObjectInQueue = 5;

      private Queue<GameObject> m_objectsRecycleBin;

      public ObjectRecycler()
      {
         m_objectsRecycleBin = new Queue<GameObject>();
      }

      internal void AddToRecycleBin(GameObject p_object)
      {
         if (m_objectsRecycleBin.Count >= m_maxObjectInQueue)
         {
            Object.Destroy(p_object);
            return;
         }

         p_object.SetActive(false);
         m_objectsRecycleBin.Enqueue(p_object);
      }

      internal GameObject RemoveFromRecycleBin()
      {
         if (m_objectsRecycleBin.Count > 0)
         {
            GameObject l_object = m_objectsRecycleBin.Dequeue();

            l_object.SetActive(true);

            return l_object;
         }

         return null;
      }

      public bool IsRecycleBinEmpty
      {
         get => m_objectsRecycleBin.Count == 0;
      }

      public void Reset()
      {
         m_objectsRecycleBin.Clear();
      }
   }
}
