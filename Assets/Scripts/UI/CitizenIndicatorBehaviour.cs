using HypNot.Behaviours.Characters;
using HypNot.Player;
using TMPro;
using UnityEngine;

namespace HypNot.Behaviours.UI
{
   public class CitizenIndicatorBehaviour : MonoBehaviour
   {
      private CharacterType m_type;

      private Animator m_animator;

      private TextMeshProUGUI m_citizenCoutDisplay;

      public TextMeshProUGUI CitizenCoutDisplay
      {
         get
         {
            if(m_citizenCoutDisplay == null)
            {
               m_citizenCoutDisplay = GetComponentInChildren<TextMeshProUGUI>(true);
            }

            return m_citizenCoutDisplay;
         }
      }

      public CharacterType Type
      {
         get => m_type;
         set
         {
            m_type = value;

            m_animator.SetTrigger(m_type.ToString().ToLower());
         }
      }

      void Start()
      {
         m_animator = GetComponentInChildren<Animator>();

         Type = PlayerStateSingleton.Instance.FirstCitizenType;
      }
   }
}
