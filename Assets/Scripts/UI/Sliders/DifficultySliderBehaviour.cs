using HypNot.Player;

namespace HypNot.Behaviours.UI
{
   public class DifficultySliderBehaviour : SettingsSliderBehaviour
   {
      public override void OnValueChanged()
      {
         int l_difficulty;

         switch (Slider.value)
         {
            case 1:
               l_difficulty = 1;
               break;
            case 2:
               l_difficulty = 2;
               break;
            case 3:
               l_difficulty = 3;
               break;
            case 4:
               l_difficulty = 4;
               break;
            default:
               l_difficulty = 5;
               break;
         }

         PlayerSaveSingleton.Instance.Difficulty = l_difficulty;

         ValueDisplay.text = l_difficulty.ToString();
      }

      public override void Reset()
      {
         base.Reset();

         int l_difficulty = PlayerSaveSingleton.Instance.Difficulty;

         switch (l_difficulty)
         {
            case 1:
               Slider.value = 1;
               break;
            case 2:
               Slider.value = 2;
               break;
            case 3:
               Slider.value = 3;
               break;
            case 4:
               Slider.value = 4;
               break;
            default:
               Slider.value = 5;
               break;
         }
      }
   }
}
