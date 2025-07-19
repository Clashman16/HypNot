using HypNot.Player;
using HypNot.Utils;

namespace HypNot.Behaviours.UI
{
   public class HomeButtonBehaviour : OneActionButtonBehaviour
   {
      public override void Act()
      {
         base.Act();

         PlayerStateSingleton.Instance.GameScreen = GameScreen.TITLE_SCREEN;

         GameResetter.Reset();
      }
   }
}
