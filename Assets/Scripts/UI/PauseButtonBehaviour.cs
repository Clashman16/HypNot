using HypNot.Player;
using UnityEngine;
using UnityEngine.UI;

namespace HypNot.Behaviours.UI
{
   public class PauseButtonBehaviour : OneActionButtonBehaviour
   {
      private Image m_icon;

      private Sprite[] m_sprites;

      private const string m_spritesheetPath = "Spritesheets/UI/pause_button";

      public override void Act()
      {
         PlayerStateSingleton.Instance.GameScreen = GameScreen.PAUSE_SCREEN;

         m_icon.sprite = m_sprites[0];
      }

      public void ResetIcon()
      {
         if(m_icon == null)
         {
            Image[] l_images = GetComponentsInChildren<Image>();
            foreach (Image l_img in l_images)
            {
               if (l_img.gameObject != gameObject)
               {
                  m_icon = l_img;
               }
            }
         }

         if(m_sprites == null)
         {
            m_sprites = Resources.LoadAll<Sprite>(m_spritesheetPath);
         }

         m_icon.sprite = m_sprites[1];
      }
   }
}