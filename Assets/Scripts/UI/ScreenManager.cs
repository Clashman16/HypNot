using HypNot.Player;
using UnityEngine;

namespace HypNot.Behaviours.UI
{
   public class ScreenManager : MonoBehaviour
   {
      private GameObject[] m_screens;

      private GameState m_lastGameState;

      void Start()
      {
         m_lastGameState = GameState.TITLESCREEN;

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
               //m_screens[1].SetActive(false);
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
         GameState l_currentGameState = PlayerStateSingleton.Instance.GameState;

         if (l_currentGameState != m_lastGameState)
         {
            switch(l_currentGameState)
            {
               case GameState.PLAYING:
                  //m_screens[0].SetActive(false);
                  m_screens[1].SetActive(true);
                  //m_screens[2].SetActive(false);
                  m_screens[3].SetActive(false);
               break;
               case GameState.PAUSE:
                  m_screens[1].SetActive(false);
                  m_screens[2].SetActive(true);
                  break;
               case GameState.ENDING:
                  m_screens[1].SetActive(false);

                  GameObject l_endScreen = m_screens[3];

                  l_endScreen.SetActive(true);

                  l_endScreen.GetComponentInChildren<FinalScoreDisplayBehaviour>().UpdateDisplay();

                  break;

               default:
                  m_screens[0].SetActive(true);
                  m_screens[1].SetActive(false);
                  m_screens[2].SetActive(false);
                  m_screens[3].SetActive(false);

                  break;
            }

            m_lastGameState = l_currentGameState;
         }
      }
   }
}
