namespace HypNot.Utils
{
   public sealed class TagDatabaseSingleton
   {
      private static TagDatabaseSingleton m_instance = null;

      public static TagDatabaseSingleton Instance
      {
         get
         {
            if (m_instance == null)
            {
               m_instance = new TagDatabaseSingleton();
            }
            return m_instance;
         }
      }

      private const string m_playerTag = "Player";

      public string PlayerTag
      {
         get => m_playerTag;
      }

      private const string m_firstLevelZoneTag = "FirstLevelZone";

      public string FirstLevelZoneTag
      {
         get => m_firstLevelZoneTag;
      }

      private const string m_backgroundMusicPlayerTag = "BackgroundMusicPlayer";

      public string BackgroundMusicPlayerTag
      {
         get => m_backgroundMusicPlayerTag;
      }

      private const string m_sfxPlayerTag = "SFXPlayer";

      public string SFXPlayerTag
      {
         get => m_sfxPlayerTag;
      }

   }
}
