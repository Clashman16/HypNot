using HypNot.Player;
using HypNot.Spawners;
using HypNot.Utils;
using UnityEngine;

namespace HypNot.Behaviours.UI
{
   public class ScreenManagerBehaviour : MonoBehaviour
   {
      private GameObject[] m_screens;

      private GameScreen m_lastGameScreen;

      void Start()
      {
         PlayerStateSingleton.Instance.GameScreen = GameScreen.TITLE_SCREEN;

         m_lastGameScreen = GameScreen.TITLE_SCREEN;

         m_screens = new GameObject[4];

         GameObject[]  l_screensTemp = GameObject.FindGameObjectsWithTag("Screen");

         foreach(GameObject l_go in l_screensTemp)
         {
            if(l_go.name.Contains("Title"))
            {
               m_screens[0] = l_go;
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
            else
            {
               m_screens[3] = l_go;
               m_screens[3].SetActive(false);
            }
         }
      }

      void Update()
      {
         GameScreen l_currentGameScreen = PlayerStateSingleton.Instance.GameScreen;

         if (l_currentGameScreen != m_lastGameScreen)
         {
            switch(l_currentGameScreen)
            {
               case GameScreen.GAME_SCREEN:
                  m_screens[0].SetActive(false);
                  m_screens[1].SetActive(true);
                  //m_screens[2].SetActive(false);
                  m_screens[3].SetActive(false);
               break;
               case GameScreen.PAUSE_SCREEN:
                  m_screens[2].SetActive(true);
                  break;
               case GameScreen.END_SCREEN:
                  m_screens[1].SetActive(false);

                  GameObject l_endScreen = m_screens[3];

                  l_endScreen.SetActive(true);

                  l_endScreen.GetComponentInChildren<FinalScoreDisplayBehaviour>().UpdateDisplay();

                  break;

               default:
                  m_screens[0].SetActive(true);
                  m_screens[1].SetActive(false);
                  //m_screens[2].SetActive(false);
                  m_screens[3].SetActive(false);

                  break;
            }

            m_lastGameScreen = l_currentGameScreen;
         }
      }
   }
}
