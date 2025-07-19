using HypNot.Player;
using HypNot.Spawners;
using HypNot.Utils;

namespace HypNot.Behaviours.UI
{
   public class ResetButtonBehaviour : OneActionButtonBehaviour
   {
      public override void Act()
      {
         base.Act();

         PlayerStateSingleton.Instance.GameScreen = GameScreen.GAME_SCREEN;

         GameResetter.Reset();

         HypnotizedPersonSpawnerSingleton.Instance.Spawn();
      }
   }
}
