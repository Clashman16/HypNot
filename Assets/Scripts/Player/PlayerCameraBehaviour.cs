using UnityEngine;

namespace HypNot.Player
{
   public class PlayerCameraBehaviour : MonoBehaviour
   {
      Vector3 m_firstPosition;

      private void Start()
      {
         m_firstPosition = transform.position;
      }

      public void Reset()
      {
         transform.position = m_firstPosition;
      }
   }
}
