using HypNot.Player;

namespace HypNot.Behaviours.UI
{
   public class SettingsButtonBehaviour : OneActionButtonBehaviour
   {
      public override void Act()
      {
         base.Act();

         PlayerStateSingleton.Instance.GameScreen = GameScreen.SETTINGS_SCREEN;
      }
   }
}
