using HypNot.Behaviours.Characters;
using HypNot.Utils;
using Pathfinding;
using System.Collections.Generic;
using UnityEngine;

namespace HypNot.Map
{
   public sealed class MapManagerSingleton
   {
      private static MapManagerSingleton m_instance = null;

      public static MapManagerSingleton Instance
      {
         get
         {
            if (m_instance == null)
            {
               m_instance = new MapManagerSingleton();
            }
            return m_instance;
         }
      }

      private const int m_characterTypeEnumSize = 15;

      public int CharacterTypeEnumSize
      {
         get => m_characterTypeEnumSize;
      }

      private Dictionary<CharacterType, int> m_hypnotizedPersonTypes;

      public Dictionary<CharacterType, int> HypnotizedPersonTypes
      {
         get => m_hypnotizedPersonTypes;
      }

      private Dictionary<int, int> m_hypnotizedPersonMana;

      public Dictionary<int, int> HypnotizedPersonMana
      {
         get => m_hypnotizedPersonMana;
      }

      private const int m_hypnotizedPersonManaMax = 4;

      private GameObject m_lastSpawnedCitizen;

      public GameObject LastSpawnedCitizen
      {
         get => m_lastSpawnedCitizen;
         set => m_lastSpawnedCitizen = value;
      }

      private int m_orderInLayer;

      public int OrderInLayer
      {
         get => m_orderInLayer;
         set => m_orderInLayer = value;
      }

      private Dictionary<GraphNode, List<CollidableDataBehaviour>> m_collidables;

      public Dictionary<GraphNode, List<CollidableDataBehaviour>> Collidables
      {
         get => m_collidables;
      }

      public void AddCollidable(CollidableDataBehaviour p_collidable)
      {
         if(p_collidable.OccupiedNode == null)
         {
            p_collidable.OccupiedNode = NodePositionConverter.GetNodeFromPosition(p_collidable.transform.position);
         }

         GraphNode l_node = p_collidable.OccupiedNode;

         if (m_collidables.ContainsKey(l_node))
         {
            m_collidables[l_node].Add(p_collidable);
         }
         else
         {
            m_collidables.Add(l_node, new List<CollidableDataBehaviour>() { p_collidable });
         }
      }

      public void RemoveCollidable(CollidableDataBehaviour p_collidable)
      {
         GraphNode l_node = p_collidable.OccupiedNode;

         if (p_collidable.OccupiedNode == null)
         {
            p_collidable.OccupiedNode = NodePositionConverter.GetNodeFromPosition(p_collidable.transform.position);
         }

         if (m_collidables.ContainsKey(l_node))
         {
            m_collidables[l_node].Remove(p_collidable);

            if(m_collidables[l_node].Count == 0)
            {
               m_collidables.Remove(l_node);
            }
         }
      }


      private MapManagerSingleton()
      {
         m_orderInLayer = 0;

         m_collidables = new Dictionary<GraphNode, List<CollidableDataBehaviour>>();

         m_hypnotizedPersonTypes = new Dictionary<CharacterType, int>();

         m_hypnotizedPersonMana = new Dictionary<int, int>();

         for (int l_i = 0; l_i < m_characterTypeEnumSize; l_i++)
         {
            m_hypnotizedPersonTypes.Add((CharacterType) l_i, 0);

            if(l_i <= m_hypnotizedPersonManaMax)
            {
               m_hypnotizedPersonMana.Add(l_i, 0);
            }
         }
      }

      public void Reset()
      {
         m_orderInLayer = 0;

         for (int l_i = 0; l_i < m_characterTypeEnumSize; l_i++)
         {
            m_hypnotizedPersonTypes[(CharacterType)l_i] = 0;

            if (l_i <= m_hypnotizedPersonManaMax)
            {
               m_hypnotizedPersonMana[l_i] = 0;
            }
         }

         m_collidables.Clear();

         TilemapDataBehaviour[] l_tilemaps = Object.FindObjectsByType<TilemapDataBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None);

         foreach (TilemapDataBehaviour l_tilemap in l_tilemaps)
         {
            l_tilemap.RegisterCollidableToDatabase();
         }

         CitizenAIBehaviour[] l_citizens = Object.FindObjectsByType<CitizenAIBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None);

         foreach (CitizenAIBehaviour l_citizen in l_citizens)
         {
            Object.Destroy(l_citizen.gameObject);
         }

         HypnotizedPersonDataBehaviour[] l_hypnotizedPersons = Object.FindObjectsByType<HypnotizedPersonDataBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None);

         foreach (HypnotizedPersonDataBehaviour l_person in l_hypnotizedPersons)
         {
            Object.Destroy(l_person.gameObject);
         }
      }
   }
}
