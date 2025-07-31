using UnityEngine;

namespace HypNot.Map
{
   public class LevelZoneBehaviour : MonoBehaviour
   {
      private const float m_sizeX = 5f;

      private const float m_sizeY = 10f;

      public Vector2 Size
      {
         get => new Vector2(m_sizeX, m_sizeY);
      }
   }
}

