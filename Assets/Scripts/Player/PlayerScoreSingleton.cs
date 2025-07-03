namespace HypNot.Player
{
   public sealed class PlayerScoreSingleton
   {
      private static PlayerScoreSingleton m_instance = null;

      private int m_score;

      public int Score
      {
         get => m_score;
         set => m_score = value;
      }

      public static PlayerScoreSingleton Instance
      {
         get
         {
            if (m_instance == null)
            {
               m_instance = new PlayerScoreSingleton();
            }
            return m_instance;
         }
      }

      private PlayerScoreSingleton()
      {
         m_score = 1;
      }
   }
}
