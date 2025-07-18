using HypNot.Player;
using TMPro;
using UnityEngine;

namespace HypNot.Behaviours.UI
{
   public class FinalScoreDisplayBehaviour : MonoBehaviour
   {
      private TextMeshProUGUI l_scoreDisplay;

      private TextMeshProUGUI l_congratsText;

      private void Start()
      {
         TextMeshProUGUI[] l_textsTemp = GetComponentsInChildren<TextMeshProUGUI>();

         foreach(TextMeshProUGUI l_text in l_textsTemp)
         {
            if(l_text.name.Contains("Score"))
            {
               l_scoreDisplay = l_text;
            }
            else
            {
               l_congratsText = l_text;
            }
         }
      }

      public void UpdateDisplay()
      {
         int l_score = PlayerScoreSingleton.Instance.Score;

         Color l_scoreColor;

         if (l_score <= 5)
         {
            l_scoreColor = Color.red;

            l_congratsText.text = "Too bad!";
         }
         else
         {
            l_scoreColor = Color.green;

            l_congratsText.text = "Congrats!";
         }

         string l_ColorHex = ColorToHex(l_scoreColor);

         l_scoreDisplay.text = $"You saved <color={l_ColorHex}><size=150%>{l_score}</size></color> citizens.";
      }

      private string ColorToHex(Color p_color)
      {
         Color32 l_color32 = p_color;
         return $"#{l_color32.r:X2}{l_color32.g:X2}{l_color32.b:X2}";
      }
   }
}
