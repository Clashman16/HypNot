using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HypNot.Behaviours.UI
{
   public abstract class SettingsSliderBehaviour : MonoBehaviour
   {
      private Slider m_slider;

      public Slider Slider
      {
         get => m_slider;
      }

      private TextMeshProUGUI m_valueDisplay;

      public TextMeshProUGUI ValueDisplay
      {
         get => m_valueDisplay;
      }

      public virtual void OnValueChanged()
      {
         // Update the right player preference
      }

      public virtual void Reset()
      {
         if (m_slider == null)
         {
            m_slider = GetComponent<Slider>();
         }

         if (m_valueDisplay == null)
         {
            TextMeshProUGUI[] l_texts = transform.parent.GetComponentsInChildren<TextMeshProUGUI>();

            foreach (TextMeshProUGUI l_txt in l_texts)
            {
               if (l_txt.name.Contains("Value"))
               {
                  m_valueDisplay = l_txt;
               }
            }
         }

         // Get the right player preference
      }
   }
}
