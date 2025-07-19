using HypNot.Behaviours.UI;
using HypNot.Map;
using HypNot.Player;
using HypNot.Spawners;
using UnityEngine;

namespace HypNot.Utils
{
   public static class GameResetter
   {
      public static void Reset()
      {
         CitizenSpawnerSingleton.Instance.Reset();

         HypnotizedPersonSpawnerSingleton.Instance.Reset();

         MapManagerSingleton.Instance.Reset();

         PlayerScoreSingleton.Instance.Score = 1;

         PlayerStateSingleton.Instance.Reset();

         Object.FindObjectOfType<TimerBehaviour>(true).Reset();

         GameObject.FindGameObjectWithTag(PlayerStateSingleton.Instance.PlayerTag).GetComponent<PlayerCameraBehaviour>().Reset();
      }
   }
}
