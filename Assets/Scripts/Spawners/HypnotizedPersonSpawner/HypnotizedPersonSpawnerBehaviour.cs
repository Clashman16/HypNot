using UnityEngine;

namespace HypNot.Spawners
{
   public class HypnotizedPersonSpawnerBehaviour : MonoBehaviour
   {
      private void Start()
      {
         HypnotizedPersonSpawnerSingleton.Instance.Spawn();
      }
   }
}
