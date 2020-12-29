using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameService {

    public class UnityExecutor<VALUE> : IUnityOnDisableListener, IUnityOnDestroyListener, IUnityOnApplicationQuitListener {

        protected Queue<VALUE> mQueueReady = new Queue<VALUE>();
        protected Queue<VALUE> mQueueRunning = new Queue<VALUE>();
        private List<VALUE> mQueueRunningCache = new List<VALUE>();
        protected Queue<VALUE> mQueueStop = new Queue<VALUE>();
        private bool mIsShutDownAll = false;

        void IUnityOnDisableListener.OnDisable() {
            StopAllThings();
        }

        void IUnityOnApplicationQuitListener.OnApplicationQuit() {
            StopAllThings();
        }

        public void Update(Executor executor) { // 处理数据
            if (mIsShutDownAll) return;
            int countReady = mQueueReady.Count;
            if (countReady > 0) {
                VALUE value = mQueueReady.Dequeue();
                if (value != null) {
                    mQueueRunning.Enqueue(value);
                }
            }
        }

        public void LateUpdate(Executor executor) { // 执行
            if (mIsShutDownAll) {
                MoveTo(mQueueReady, mQueueStop);
                mQueueReady.Clear();

                MoveTo(mQueueRunning, mQueueStop);
                mQueueRunning.Clear();

                MoveTo(mQueueRunningCache, mQueueStop);
                mQueueRunningCache.Clear();

                ExecuteOnUpdate_QueueStop(executor);
                mIsShutDownAll = false;
            } else {
                ExecuteOnUpdate_QueueRunning(executor);
                ExecuteOnUpdate_QueueStop(executor);
            }
        }

        private void ExecuteOnUpdate_QueueStop(Executor executor) {
            int countStop = mQueueStop.Count;
            for (int i = 0; i < countStop; i++) {
                VALUE value = mQueueStop.Dequeue();
                if (value != null) {
                    if (value is System.Action) {
                    } else if (value is IEnumerator) {
                        executor.StopCoroutine(value as IEnumerator);
                    }
                    if (mQueueRunningCache.Contains(value)) {
                        mQueueRunningCache.Remove(value);
                    }
                }
            }
            if (countStop > 0) {
                mQueueStop.Clear();
                mQueueRunningCache.Clear();
            }
        }

        private void ExecuteOnUpdate_QueueRunning(Executor executor) {
            int countRunning = mQueueRunning.Count;
            if (countRunning > 0) {
                VALUE value = mQueueRunning.Dequeue();
                if (value != null) {
                    if (value is System.Action) {
                        (value as System.Action)();
                    } else if (value is IEnumerator) {
                        executor.StartCoroutine(value as IEnumerator);
                    }
                    mQueueRunningCache.Add(value);
                }
            }
        }

        private void StopAllThings() {
            ShutDownAll();
            mQueueReady.Clear();
            mQueueRunning.Clear();
            mQueueStop.Clear();
            mQueueRunningCache.Clear();
        }

        public void ShutDownAll() {
            mIsShutDownAll = true;
        }

        private void MoveTo(Queue<VALUE> src, Queue<VALUE> dst) {
            for (int i = 0, count = src.Count; i < count; i++) {
                VALUE value = src.Dequeue();
                dst.Enqueue(value);
            }
            src.Clear();
        }

        private void MoveTo(List<VALUE> src, Queue<VALUE> dst) {
            for (int i = 0, count = src.Count; i < count; i++) {
                VALUE value = src[i];
                dst.Enqueue(value);
            }
            src.Clear();
        }

        void IUnityOnDestroyListener.OnDestroy() {
            StopAllThings();
        }

    }
}
