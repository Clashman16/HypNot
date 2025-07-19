using HypNot.Player;
using HypNot.Spawners;
using HypNot.Utils;

namespace HypNot.Behaviours.UI
{
   public class ResetButtonBehaviour : OneActionButtonBehaviour
   {
      public override void Act()
      {
         PlayerStateSingleton.Instance.GameState = GameState.PLAYING;

         GameResetter.Reset();

         HypnotizedPersonSpawnerSingleton.Instance.Spawn();
      }
   }
}
