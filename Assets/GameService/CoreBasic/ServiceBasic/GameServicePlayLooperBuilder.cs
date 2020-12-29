using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;

namespace GameService {
    public class GameServicePlayLooperBuilder {

        public static PlayerLoopSystem Build(PlayerLoopSystem.UpdateFunction update) {
            PlayerLoopSystem defaultSystem = PlayerLoop.GetCurrentPlayerLoop();
            PlayerLoopSystem newSystem = new PlayerLoopSystem();
            newSystem.updateDelegate += update;

            PlayerLoopSystem[] defaultLoopSystem = defaultSystem.subSystemList;
            if (defaultLoopSystem == null || defaultLoopSystem.Length < 1) {
                defaultLoopSystem = new PlayerLoopSystem[1] { newSystem };
                defaultSystem.subSystemList = defaultLoopSystem;
            } else {
                PlayerLoopSystem[] newLoopSystem = new PlayerLoopSystem[defaultLoopSystem.Length + 1];
                System.Array.Copy(defaultLoopSystem, 0, newLoopSystem, 0, defaultLoopSystem.Length);
                newLoopSystem[newLoopSystem.Length - 1] = newSystem;
                defaultSystem.subSystemList = newLoopSystem;
            }

            PlayerLoop.SetPlayerLoop(defaultSystem);
            return newSystem;
        }

    }
}