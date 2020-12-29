using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GameService {
    public class FSMState {

        public virtual void OnEnter() { }

        public virtual void OnEnter(object userData) { }

        public virtual void Reason() { }

        public virtual void OnUpdate() { }

        public virtual void OnFixedUpdate() { }

        public virtual void OnLateUpdate() { }

        public virtual void OnExit() { }

        public void print(string str) {
            Debug.Log(str);
        }

    }
}
