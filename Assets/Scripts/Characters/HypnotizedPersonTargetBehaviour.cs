using HypNot.Behaviours.UI;
using HypNot.Map;
using HypNot.Player;
using HypNot.Sounds;
using HypNot.Spawners;
using HypNot.Utils;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HypNot.Behaviours.Characters
{
   public class HypnotizedPersonTargetBehaviour : MonoBehaviour, IPointerClickHandler
   {
      private HypnotizedPersonDataBehaviour m_data;

      public HypnotizedPersonDataBehaviour Data
      {
         get => m_data;
      }

      private List<CitizenAIBehaviour> m_citizens;

      public List<CitizenAIBehaviour> Citizens
      {
         get => m_citizens;
      }

      AudioSource m_audioPlayer;

      private void Start()
      {
         m_data = GetComponent<HypnotizedPersonDataBehaviour>();

         m_citizens = new List<CitizenAIBehaviour>();

         m_audioPlayer = GameObject.FindGameObjectWithTag(TagDatabaseSingleton.Instance.SFXPlayerTag).GetComponent<AudioSource>();
      }

      public void OnPointerClick(PointerEventData eventData)
      {
         if(PlayerStateSingleton.Instance.GameScreen == GameScreen.GAME_SCREEN)
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

      public void OnPersonSaved()
      {
         PlayerScoreSingleton.Instance.Score += 1;

         foreach (CitizenAIBehaviour l_citizen in m_citizens)
         {
            CitizenSpawnerSingleton.Instance.AddToRecycleBin(l_citizen.gameObject);
         }

         m_citizens.Clear();

         m_data.SpawnPoint.HypnotizedPerson = null;

         CharacterType l_type = m_data.Type;

         int l_typeCount = MapManagerSingleton.Instance.HypnotizedPersonTypes[l_type];
         MapManagerSingleton.Instance.HypnotizedPersonTypes[l_type] = l_typeCount == 0 ? 0 : l_typeCount - 1;

         int l_mana = m_data.FirstManaCount;

         int l_manaCount = MapManagerSingleton.Instance.HypnotizedPersonMana[l_mana];
         MapManagerSingleton.Instance.HypnotizedPersonMana[l_mana] = l_manaCount == 0 ? 0 : l_manaCount - 1;

         CitizenSpawnerSingleton.Instance.AddToTypeRecycleBin(l_type);

         HypnotizedPersonSpawnerSingleton.Instance.WaveSpawner.AddToRecycleBin(gameObject);

         m_audioPlayer.clip = SFXDatabaseSingleton.Instance.Database.OneMoreCitizenSound;

         m_audioPlayer.Play();
      }
   }
}
