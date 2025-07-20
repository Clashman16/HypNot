namespace HypNot.Behaviours.UI
{
   public class CreditsCanvasBehaviour : CanvasBehaviour
   {
      CreditsBehaviour m_scrollableCredits;

      public override void Reset()
      {
         if (m_scrollableCredits == null)
         {
            m_scrollableCredits = GetComponentInChildren<CreditsBehaviour>();
         }

         m_scrollableCredits.Reset();

         base.Reset();
      }
   }
}
