using HypNot.Player;
using HypNot.Translations;
using TMPro;
using UnityEngine;

namespace HypNot.Behaviours.UI
{
   public class FinalScoreDisplayBehaviour : MonoBehaviour
   {
      private TextMeshProUGUI l_scoreDisplay;

      private TextMeshProUGUI l_congratsText;

      public void UpdateDisplay()
      {
         TextMeshProUGUI[] l_textsTemp = GetComponentsInChildren<TextMeshProUGUI>();

         if(l_congratsText == null)
         {
            foreach (TextMeshProUGUI l_text in l_textsTemp)
            {
               if (l_text.name.Contains("Score"))
               {
                  l_scoreDisplay = l_text;
               }
               else
               {
                  l_congratsText = l_text;
               }
            }
         }

         int l_score = PlayerScoreSingleton.Instance.Score;

         Color l_scoreColor;

         LanguageDatabaseSingleton l_database = LanguageDatabaseSingleton.Instance;

         PlayerSaveSingleton l_save = PlayerSaveSingleton.Instance;

         if (l_score <= 5)
         {
            l_scoreColor = Color.red;

            l_congratsText.text = l_save.LanguageId == l_database.EnglishId ? "Too bad!" : "Dommage !";
         }
         else
         {
            l_scoreColor = Color.green;

            l_congratsText.text = l_save.LanguageId == l_database.EnglishId ? "Congrats!" : "Bravo !";
         }

         l_congratsText.color = l_scoreColor;

         string l_ColorHex = ColorToHex(l_scoreColor);

         l_scoreDisplay.text = l_save.LanguageId == l_database.EnglishId ?
            $"You saved <color={l_ColorHex}><size=150%>{l_score}</size></color> citizens."
            : $"Vous avez sauvé <color={l_ColorHex}><size=150%>{l_score}</size></color> citoyens.";
      }

      private string ColorToHex(Color p_color)
      {
         Color32 l_color32 = p_color;
         return $"#{l_color32.r:X2}{l_color32.g:X2}{l_color32.b:X2}";
      }
   }
}
