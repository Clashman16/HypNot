using HypNot.Player;
using HypNot.Sounds;
using UnityEngine;

namespace HypNot.Behaviours.UI
{
   public class ScreenManagerBehaviour : MonoBehaviour
   {
      private GameObject[] m_screens;

      private GameScreen m_lastGameScreen;

      CreditsBehaviour m_scrollableCredits;

      Translator m_translator;

      void Start()
      {
         PlayerStateSingleton.Instance.GameScreen = GameScreen.TITLE_SCREEN;

         m_translator = new Translator();

         m_lastGameScreen = GameScreen.TITLE_SCREEN;

         GameObject[]  l_screensTemp = GameObject.FindGameObjectsWithTag("Screen");

         m_screens = new GameObject[l_screensTemp.Length];

         foreach (GameObject l_go in l_screensTemp)
         {
            if(l_go.name.Contains("Title"))
            {
               m_screens[0] = l_go;

               m_translator.TranslateScreen(l_go);
            }
            else if (l_go.name.Contains("Game"))
            {
               m_screens[1] = l_go;
               m_screens[1].SetActive(false);
            }
            else if(l_go.name.Contains("Pause"))
            {
               m_screens[2] = l_go;
               m_screens[2].SetActive(false);
            }
            else if (l_go.name.Contains("End"))
            {
               m_screens[3] = l_go;
               m_screens[3].SetActive(false);
            }
            else if (l_go.name.Contains("Credits"))
            {
               m_screens[4] = l_go;
               m_screens[4].SetActive(false);
            }
            else
            {
               m_screens[5] = l_go;
               m_screens[5].SetActive(false);
            }
         }
      }

      void Update()
      {
         GameScreen l_currentGameScreen = PlayerStateSingleton.Instance.GameScreen;

         if (l_currentGameScreen != m_lastGameScreen)
         {
            AudioSource l_backgroundMusic = null;

            AudioSource l_endSFX = null;

            AudioSource[] l_sources = GameObject.FindGameObjectWithTag(PlayerStateSingleton.Instance.PlayerTag).GetComponents<AudioSource>();

            foreach (AudioSource l_src in l_sources)
            {
               if (l_src.clip.name.Contains("music"))
               {
                  l_backgroundMusic = l_src;
               }
               else
               {
                  l_endSFX = l_src;
               }
            }

            GameObject l_currentScreenObject;

            switch (l_currentGameScreen)
            {
               case GameScreen.GAME_SCREEN:
                  m_screens[0].SetActive(false);
                  m_screens[2].SetActive(false);
                  m_screens[3].SetActive(false);

                  l_currentScreenObject = m_screens[1];

                  l_currentScreenObject.SetActive(true);

                  l_currentScreenObject.GetComponentInChildren<PauseButtonBehaviour>().ResetIcon();

                  if (!l_backgroundMusic.isPlaying)
                  {
                     if(m_lastGameScreen != GameScreen.PAUSE_SCREEN)
                     {
                        l_backgroundMusic.Play();
                     }
                     else
                     {
                        l_backgroundMusic.UnPause();
                     }
                  }

                  break;

               case GameScreen.PAUSE_SCREEN:
                  l_currentScreenObject = m_screens[2];

                  l_currentScreenObject.SetActive(true);

                  l_backgroundMusic.Pause();

                  break;

               case GameScreen.END_SCREEN:
                  m_screens[1].SetActive(false);

                  l_currentScreenObject = m_screens[3];

                  l_currentScreenObject.SetActive(true);

                  l_currentScreenObject.GetComponentInChildren<FinalScoreDisplayBehaviour>().UpdateDisplay();

                  l_backgroundMusic.Stop();

                  l_endSFX.clip = SFXDatabaseSingleton.Instance.Database.EndSound;

                  l_endSFX.Play();

                  break;

               case GameScreen.CREDITS_SCREEN:
                  m_screens[0].SetActive(false);

                  l_currentScreenObject = m_screens[4];

                  l_currentScreenObject.SetActive(true);

                  if(m_scrollableCredits == null)
                  {
                     m_scrollableCredits = l_currentScreenObject.GetComponentInChildren<CreditsBehaviour>();
                  }

                  m_scrollableCredits.Reset();

                  break;

               case GameScreen.SETTINGS_SCREEN:
                  m_screens[0].SetActive(false);

                  l_currentScreenObject = m_screens[5];

                  l_currentScreenObject.SetActive(true);

                  SettingsSliderBehaviour[] l_sliders = l_currentScreenObject.GetComponentsInChildren<SettingsSliderBehaviour>();

                  foreach(SettingsSliderBehaviour l_slider in  l_sliders)
                  {
                     l_slider.Reset();
                  }

                  break;

               default:
                  l_currentScreenObject = m_screens[0];

                  l_currentScreenObject.SetActive(true);
                  m_screens[1].SetActive(false);
                  m_screens[2].SetActive(false);
                  m_screens[3].SetActive(false);
                  m_screens[4].SetActive(false);
                  m_screens[5].SetActive(false);

                  l_backgroundMusic.Stop();

                  break;
            }

            m_lastGameScreen = l_currentGameScreen;

            m_translator.TranslateScreen(l_currentScreenObject);
         }

         if(!m_translator.HasTranslated)
         {
            m_translator.TranslateScreen(m_screens[(int) l_currentGameScreen]);
         }
      }
   }
}
