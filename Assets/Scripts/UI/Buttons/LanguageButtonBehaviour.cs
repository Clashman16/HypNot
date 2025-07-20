using HypNot.Player;

namespace HypNot.Behaviours.UI
{
   public class LanguageButtonBehaviour : OneActionButtonBehaviour
   {
      public override void Act()
      {
         base.Act();

         string l_languageId = PlayerSaveSingleton.Instance.LanguageId;

         PlayerSaveSingleton.Instance.LanguageId = l_languageId == "english" ? "french" : "english";
      }
   }
}
