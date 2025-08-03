namespace HypNot.Behaviours.UI
{
   public class SettingsCanvasBehaviour : CanvasBehaviour
   {
      private SettingsSliderBehaviour[] m_sliders;

      private LanguageButtonBehaviour[] m_languageButtons;

      public override void Reset()
      {
         if(m_sliders == null)
         {
            m_sliders = gameObject.GetComponentsInChildren<SettingsSliderBehaviour>();
         }

         foreach (SettingsSliderBehaviour l_slider in m_sliders)
         {
            l_slider.Reset();
         }

         if (m_languageButtons == null)
         {
            m_languageButtons = gameObject.GetComponentsInChildren<LanguageButtonBehaviour>();
         }

         foreach (LanguageButtonBehaviour l_button in m_languageButtons)
         {
            l_button.Reset();
         }

         base.Reset();
      }
   }
}
