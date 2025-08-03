using HypNot.Player;

namespace HypNot.Behaviours.UI
{
   public class PauseButtonBehaviour : OneActionButtonBehaviour
   {
      public override void Act()
      {
         base.Act();

         PlayerStateSingleton.Instance.GameScreen = GameScreen.PAUSE_SCREEN;
      }
   }
}