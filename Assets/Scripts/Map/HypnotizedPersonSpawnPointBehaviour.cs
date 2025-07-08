using HypNot.Behaviours.Characters;
using UnityEngine;

namespace HypNot.Map
{
   public class HypnotizedPersonSpawnPointBehaviour : MonoBehaviour
   {
      private HypnotizedPersonDataBehaviour m_hypnotizedPerson;

      public HypnotizedPersonDataBehaviour HypnotizedPerson
      {
         get => m_hypnotizedPerson;
         set => m_hypnotizedPerson = value;
      }

      public bool IsSlotOccupied => m_hypnotizedPerson != null;

      [SerializeField]
      private HypnotizedPersonSpawnPointBehaviour m_nearbySpot;

      public HypnotizedPersonSpawnPointBehaviour NearbySpot
      {
         get => m_nearbySpot;
      }
   }
}
