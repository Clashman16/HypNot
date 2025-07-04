using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
   [CreateAssetMenu(menuName = "ScriptableObjects/InstantiatedObjectDatabase")]
   public class ScriptableInstantiatedObjectDatabase : ScriptableObject
   {
      [SerializeField]
      private GameObject m_hypnotizedPerson;

      public GameObject HypnotizedPerson
      {
         get => m_hypnotizedPerson;
      }

      [SerializeField]
      private GameObject m_citizen;

      public GameObject Citizen
      {
         get => m_citizen;
      }

      [SerializeField]
      private List<RuntimeAnimatorController> m_animators = new();

      private Dictionary<string, RuntimeAnimatorController> m_animatorsByName = new();

      public Dictionary<string, RuntimeAnimatorController> AnimatorsByName
      {
         get => m_animatorsByName;
      }

      public void Init()
      {
         foreach (RuntimeAnimatorController l_animator in m_animators)
         {
            if (l_animator != null)
            {
               m_animatorsByName[l_animator.name] = l_animator;
            }
         }
      }
   }
}
