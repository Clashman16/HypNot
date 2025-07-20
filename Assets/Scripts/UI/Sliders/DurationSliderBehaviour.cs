using HypNot.Player;

namespace HypNot.Behaviours.UI
{
   public class DurationSliderBehaviour : SettingsSliderBehaviour
   {
      public override void OnValueChanged()
      {
         int l_duration;

         switch (Slider.value)
         {
            case 0:
               l_duration = 1;
               break;
            case 1:
               l_duration = 3;
               break;
            default:
               l_duration = 5;
               break;
         }

         PlayerSaveSingleton.Instance.TimerMax = l_duration;

         ValueDisplay.text = string.Concat("0", l_duration.ToString(), ":00");
      }

      public override void Reset()
      {
         base.Reset();

         int l_duration = PlayerSaveSingleton.Instance.TimerMax;

         switch (l_duration)
         {
            case 1:
               Slider.value = 0;
               break;
            case 3:
               Slider.value = 1;
               break;
            default:
               Slider.value = 2;
               break;
         }
      }
   }
}
