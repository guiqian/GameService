using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameService {
    public class UnityIEnumeratorExecutor : UnityExecutor<IEnumerator>, IExecutorSchedule<IEnumerator> {

        public void Schedule(IEnumerator value) {
            if (value == null) return;
            lock (mQueueReady) {
                mQueueReady.Enqueue(value);
            }
        }

        public void ShutDown(IEnumerator value) {
            if (value == null) return;
            lock (mQueueStop) {
                mQueueStop.Enqueue(value);
            }
        }

    }
}
