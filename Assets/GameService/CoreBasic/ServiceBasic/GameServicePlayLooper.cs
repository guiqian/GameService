using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameService {
    public class GameServicePlayLooper {

        public void Start() {
            GameServicePlayLooperBuilder.Build(Update);
        }

        private void Update() {
            OnUpdate();
            OnLateUpdate();
        }

        protected virtual void OnUpdate() {
        }

        protected virtual void OnLateUpdate() {
        }

    }
}
