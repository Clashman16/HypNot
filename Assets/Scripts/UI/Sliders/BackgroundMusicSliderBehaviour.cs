using HypNot.Player;

namespace HypNot.Behaviours.UI
{
   public class BackgroundMusicSliderBehaviour : SettingsSliderBehaviour
   {
      public override void OnValueChanged()
      {
         PlayerSaveSingleton.Instance.BackgroundMusicVolume = Slider.value;
      }
   }
}
