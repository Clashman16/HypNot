using HypNot.Player;

namespace HypNot.Behaviours.UI
{
   public class ResumeButtonBehaviour : OneActionButtonBehaviour
   {
      public override void Act()
      {
         base.Act();

         PlayerStateSingleton.Instance.GameScreen = GameScreen.GAME_SCREEN;
      }
   }
}
