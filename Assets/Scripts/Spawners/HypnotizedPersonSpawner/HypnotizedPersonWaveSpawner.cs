using HypNot.Behaviours.Characters;
using HypNot.Map;
using HypNot.Spawners.Utils;
using HypNot.ScriptableObjects;
using UnityEngine;

namespace HypNot.Spawners
{
   public abstract class HypnotizedPersonWaveSpawner : ObjectRecycler
   {
      private double m_spawnRange;

      internal double SpawnRange
      {
         get => m_spawnRange;
      }

      private int m_spawnPointCount;

      internal int SpawnPointCount
      {
         get => m_spawnPointCount;
         set => m_spawnPointCount = value;
      }

      private CharacterTypeRandomizer m_characterTypeRandomizer;

      private ManaRandomizer m_manaRandomizer;

      public HypnotizedPersonWaveSpawner(double p_spawnRange) : base()
      {
         m_spawnRange = p_spawnRange;

         m_characterTypeRandomizer = new CharacterTypeRandomizer();

         m_manaRandomizer = new ManaRandomizer(m_spawnRange);
      }

      public abstract void SetLevelZones();

      public abstract void SpawnWave();

      internal void Spawn(HypnotizedPersonSpawnPointBehaviour p_spawnPoint)
      {
         GameObject l_instantiatedHypnotizedPerson = null;

         Vector2 l_spawnPointPosition = p_spawnPoint.transform.position;

         if (!IsRecycleBinEmpty)
         {
            l_instantiatedHypnotizedPerson = RemoveFromRecycleBin();

            l_instantiatedHypnotizedPerson.transform.position = l_spawnPointPosition;

            HypnotizedPersonDataBehaviour l_hypnotizedPerson = l_instantiatedHypnotizedPerson.GetComponent<HypnotizedPersonDataBehaviour>();

            InitHypnotizedPerson(l_hypnotizedPerson, p_spawnPoint);
         }

         if (l_instantiatedHypnotizedPerson == null)
         {
            ScriptableInstantiatedObjectDatabase l_database = SpawnableDatabaseSingleton.Instance.Database;

            l_instantiatedHypnotizedPerson = Object.Instantiate(l_database.HypnotizedPerson, l_spawnPointPosition, Quaternion.identity);

            HypnotizedPersonDataBehaviour l_hypnotizedPerson = l_instantiatedHypnotizedPerson.GetComponent<HypnotizedPersonDataBehaviour>();

            InitHypnotizedPerson(l_hypnotizedPerson, p_spawnPoint, true);
         }
      }

      private void InitHypnotizedPerson(HypnotizedPersonDataBehaviour p_hypnotizedPerson, HypnotizedPersonSpawnPointBehaviour p_spawnPoint, bool p_mustPickType = false)
      {
         HypnotizedPersonSpawnPointBehaviour l_nearbySpot = p_spawnPoint.NearbySpot;

         p_hypnotizedPerson.HasNeighbor = l_nearbySpot != null && l_nearbySpot.IsSlotOccupied;

         int l_manaCount = MapManagerSingleton.Instance.HypnotizedPersonMana[1] == 0 ? 1 : m_manaRandomizer.Random(p_hypnotizedPerson.HasNeighbor);
         p_hypnotizedPerson.FirstManaCount = l_manaCount;
         p_hypnotizedPerson.ManaCount = l_manaCount;

         MapManagerSingleton.Instance.HypnotizedPersonMana[p_hypnotizedPerson.FirstManaCount] += 1;

         p_spawnPoint.HypnotizedPerson = p_hypnotizedPerson;

         p_hypnotizedPerson.SpawnPoint = p_spawnPoint;

         if (p_mustPickType)
         {
            p_hypnotizedPerson.Type = m_characterTypeRandomizer.Random();
         }

         MapManagerSingleton.Instance.HypnotizedPersonTypes[p_hypnotizedPerson.Type] += 1;
      }
   }
}
