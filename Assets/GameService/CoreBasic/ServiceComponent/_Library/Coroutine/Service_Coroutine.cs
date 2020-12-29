using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameService {
    public class Service_Coroutine : IGameService {

        public void StartCoroutine(IEnumerator enumerator) {
            Executor.Instance.Schedule(enumerator);
        }

        public void StopCoroutine(IEnumerator enumerator) {
            Executor.Instance.ShutDown(enumerator);
        }

        public void StopAllCoroutines() {
            Executor.Instance.ShutDownAll();
        }

        void System.IDisposable.Dispose() {
        }

    }
}
