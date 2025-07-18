using UnityEngine;

namespace HypNot.Behaviours.UI
{
   public class QuitButtonBehaviour : OneActionButtonBehaviour
   {
      public override void Act()
      {
         Application.Quit();
      }
   }
}
