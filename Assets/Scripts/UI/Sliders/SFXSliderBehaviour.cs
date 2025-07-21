using HypNot.Player;

namespace HypNot.Behaviours.UI
{
   public class SFXSliderBehaviour : SettingsSliderBehaviour
   {
      public override void OnValueChanged()
      {
         PlayerSaveSingleton.Instance.SFXVolume = Slider.value;
      }
   }
}
