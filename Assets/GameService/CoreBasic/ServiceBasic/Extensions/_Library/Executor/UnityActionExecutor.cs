using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameService {

    public class UnityActionExecutor : UnityExecutor<System.Action>, IExecutorSchedule<System.Action> {

        public void Schedule(System.Action value) {
            if (value == null) return;
            lock (mQueueReady) {
                mQueueReady.Enqueue(value);
            }
        }

        public void ShutDown(System.Action value) {
            if (value == null) return;
            lock (mQueueStop) {
                mQueueStop.Enqueue(value);
            }
        }

    }
}
