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
         get => m_citizenCoutDisplay;
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
         int l_score = PlayerScoreSingleton.Instance.Score;

         m_citizenCoutDisplay = GetComponentInChildren<TextMeshProUGUI>();

         m_citizenCoutDisplay.text = l_score <  10 ? "0" + l_score.ToString() : l_score.ToString();

         m_animator = GetComponentInChildren<Animator>();

         Type = PlayerStateSingleton.Instance.FirstCitizenType;
      }
   }
}
