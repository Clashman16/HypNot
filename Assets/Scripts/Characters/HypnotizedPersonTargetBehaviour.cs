using HypNot.Behaviours.UI;
using HypNot.Map;
using HypNot.Player;
using HypNot.Sounds;
using HypNot.Spawners;
using HypNot.Utils;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HypNot.Behaviours.Characters
{
   public class HypnotizedPersonTargetBehaviour : MonoBehaviour
   {
      private HypnotizedPersonDataBehaviour m_data;

      public HypnotizedPersonDataBehaviour Data
      {
         get => m_data;
      }

      private List<CitizenDataBehaviour> m_citizens;

      public List<CitizenDataBehaviour> Citizens
      {
         get => m_citizens;
      }

      AudioSource m_audioPlayer;

      Button m_button;

      private void Start()
      {
         m_data = GetComponent<HypnotizedPersonDataBehaviour>();

         m_citizens = new List<CitizenDataBehaviour>();

         m_audioPlayer = GameObject.FindGameObjectWithTag(TagDatabaseSingleton.Instance.SFXPlayerTag).GetComponent<AudioSource>();

         m_audioPlayer.volume = PlayerSaveSingleton.Instance.SFXVolume;

         m_button = GetComponentInChildren<Button>();
      }

      public void OnClick()
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
                  AstarPath.active.Scan();

                  CitizenSpawnerSingleton.Instance.Spawn(transform);

                  PlayerStateSingleton.Instance.CanSendCitizen = false;

                  m_button.gameObject.SetActive(false);
               }
            }
         }
      }

      public void OnPersonSaved()
      {
         PlayerScoreSingleton.Instance.Score += 1;

         foreach (CitizenDataBehaviour l_citizen in m_citizens)
         {
            MapManagerSingleton.Instance.RemoveCollidable(l_citizen);

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

         MapManagerSingleton.Instance.RemoveCollidable(m_data);

         HypnotizedPersonSpawnerSingleton.Instance.WaveSpawner.AddToRecycleBin(gameObject);

         m_audioPlayer.clip = SFXDatabaseSingleton.Instance.Database.OneMoreCitizenSound;

         m_audioPlayer.Play();
      }
   }
}
