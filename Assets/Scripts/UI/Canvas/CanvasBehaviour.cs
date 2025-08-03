using HypNot.Player;
using UnityEngine;

namespace HypNot.Behaviours.UI
{
   public abstract class CanvasBehaviour : MonoBehaviour
   {
      private Translator m_translator;

      public bool HasTranslated
      {
         get
         {
            if (m_translator == null)
            {
               m_translator = new Translator();
            }

            return m_translator.HasTranslated;
         }
      }

      private SwipeControllerBehaviour m_swipeController;

      public SwipeControllerBehaviour SwipeController
      {
         get
         {
            if (m_swipeController == null)
            {
               m_swipeController = FindObjectOfType<SwipeControllerBehaviour>();
            }

            return m_swipeController;
         }
      }

      public virtual void Reset()
      {
         if(!HasTranslated)
         {
            TranslateCanvas();
         }

         SwipeController.enabled = false;
      }

      public void TranslateCanvas()
      {
         if(m_translator == null)
         {
            m_translator = new Translator();
         }

         m_translator.TranslateScreen(gameObject);
      }
   }
}
