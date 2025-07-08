using System.Collections.Generic;
using UnityEngine;

namespace HypNot.Spawners
{
   public class ObjectRecycler
   {
      private const int m_maxObjectInQueue = 8;

      private Queue<GameObject> m_objectsRecycleBin;

      public ObjectRecycler()
      {
         m_objectsRecycleBin = new Queue<GameObject>();
      }

      public virtual void AddToRecycleBin(GameObject p_object)
      {
         if (m_objectsRecycleBin.Count >= m_maxObjectInQueue)
         {
            Object.Destroy(p_object);
            return;
         }

         p_object.SetActive(false);
         m_objectsRecycleBin.Enqueue(p_object);
      }

      public virtual GameObject RemoveFromRecycleBin()
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

      public bool IsRecycleBinFullFilled
      {
         get => m_objectsRecycleBin.Count == m_maxObjectInQueue;
      }

      public virtual void Reset()
      {
         m_objectsRecycleBin.Clear();
      }

      public void CopyRecycleBin(Queue<GameObject> p_copy)
      {
         foreach(GameObject l_go in m_objectsRecycleBin)
         {
            p_copy.Enqueue(l_go);
         }
      }

      public void PasteRecycleBin(Queue<GameObject> p_paste)
      {
         foreach (GameObject l_go in p_paste)
         {
            m_objectsRecycleBin.Enqueue(l_go);
         }
      }
   }
}
