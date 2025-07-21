namespace HypNot.Behaviours.UI
{
   public class SettingsCanvasBehaviour : CanvasBehaviour
   {
      private SettingsSliderBehaviour[] m_sliders;

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

         base.Reset();
      }
   }
}
