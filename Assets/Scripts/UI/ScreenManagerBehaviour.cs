using HypNot.Player;
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

         CanvasBehaviour[] l_canvasTemp = FindObjectsByType<CanvasBehaviour>(FindObjectsSortMode.None);

         m_screens = new GameObject[l_canvasTemp.Length];

         foreach (CanvasBehaviour l_canvas in l_canvasTemp)
         {
            if(l_canvas.name.Contains("Title"))
            {
               m_screens[0] = l_canvas.gameObject;

               l_canvas.TranslateCanvas();
            }
            else if (l_canvas.name.Contains("Game"))
            {
               m_screens[1] = l_canvas.gameObject;
               m_screens[1].SetActive(false);
            }
            else if(l_canvas.name.Contains("Pause"))
            {
               m_screens[2] = l_canvas.gameObject;
               m_screens[2].SetActive(false);
            }
            else if (l_canvas.name.Contains("End"))
            {
               m_screens[3] = l_canvas.gameObject;
               m_screens[3].SetActive(false);
            }
            else if (l_canvas.name.Contains("Credits"))
            {
               m_screens[4] = l_canvas.gameObject;
               m_screens[4].SetActive(false);
            }
            else
            {
               m_screens[5] = l_canvas.gameObject;
               m_screens[5].SetActive(false);
            }
         }
      }

      void Update()
      {
         GameScreen l_currentGameScreen = PlayerStateSingleton.Instance.GameScreen;

         if (l_currentGameScreen != m_lastGameScreen)
         {
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

                  break;

               case GameScreen.PAUSE_SCREEN:
                  l_currentScreenObject = m_screens[2];

                  l_currentScreenObject.SetActive(true);

                  break;

               case GameScreen.END_SCREEN:
                  m_screens[1].SetActive(false);

                  l_currentScreenObject = m_screens[3];

                  l_currentScreenObject.SetActive(true);

                  l_currentScreenObject.GetComponentInChildren<FinalScoreDisplayBehaviour>().UpdateDisplay();
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
