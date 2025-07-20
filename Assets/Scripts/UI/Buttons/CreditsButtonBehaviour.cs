using HypNot.Player;

namespace HypNot.Behaviours.UI
{
   public class CreditsButtonBehaviour : OneActionButtonBehaviour
   {
      public override void Act()
      {
         base.Act();

         PlayerStateSingleton.Instance.GameScreen = GameScreen.CREDITS_SCREEN;
      }
   }
}
