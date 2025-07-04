using HypNot.Player;
using HypNot.Spawners;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HypNot.Behaviours.Characters
{
   public class HypnotizedPersonTargetBehaviour : MonoBehaviour, IPointerClickHandler
   {
      private HypnotizedPersonDataBehaviour m_data;

      private void Start()
      {
         m_data = GetComponent<HypnotizedPersonDataBehaviour>();
      }

      public void OnPointerClick(PointerEventData eventData)
      {
         bool l_canReachTarget = PlayerStateSingleton.Instance.CanSendCitizen;

         if (l_canReachTarget)
         {
            int l_playerScore = PlayerScoreSingleton.Instance.Score;
            int l_manaCount = m_data.ManaCount;

            if ((l_playerScore == 1 && l_manaCount == 1)
               || l_playerScore > 1)
            {
               CitizenSpawnerSingleton.Instance.Spawn(transform);

               PlayerStateSingleton.Instance.CanSendCitizen = false;
            }
         }
      }
   }
}
