using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameService {
    public class GameServiceEditor4GenerateCodeUI : GameServiceEditor4GenerateCode {

        [MenuItem("Tools/Game Service/Generate Code/Generate GameObject Path (For UI)")]
        private static void GenerateCode4GameObjectPath_GenerateCodeUI() {
            GameServiceEditor4GenerateCodeUI obj = new GameServiceEditor4GenerateCodeUI();
            obj.GenerateCode4GameObjectPath_GenerateCode(GameServiceEditor4GenerateCode.YourClassName);
        }

        protected override string[] OnFixNameAndValue(string name, string value) {
            // your biz fix name and value
            return base.OnFixNameAndValue(name, value);
        }

    }
}