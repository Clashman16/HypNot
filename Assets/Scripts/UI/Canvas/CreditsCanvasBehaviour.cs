namespace HypNot.Behaviours.UI
{
   public class CreditsCanvasBehaviour : CanvasBehaviour
   {
      ScrollingCreditsBehaviour m_scrollableCredits;

      public override void Reset()
      {
         base.Reset();

         if (m_scrollableCredits == null)
         {
            m_scrollableCredits = GetComponentInChildren<ScrollingCreditsBehaviour>();
         }

         m_scrollableCredits.Reset();
      }
   }
}
