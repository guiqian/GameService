using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameService {
    public class Executor : SingletonMono<Executor>, IExecutorSchedule<System.Action>, IExecutorSchedule<IEnumerator> {

        private UnityIEnumeratorExecutor mExecutorUnityEnumerator = new UnityIEnumeratorExecutor();
        private UnityActionExecutor mExecutorUnityAction = new UnityActionExecutor();
        private ThreadActionExecutor mExecutorThreadAction = new ThreadActionExecutor();

        public void ScheduleASync(System.Action value) {
            mExecutorThreadAction.Schedule(value);
        }

        public void ShutDownASync(System.Action value) {
            mExecutorThreadAction.ShutDown(value);
        }

        public void Schedule(System.Action value) {
            mExecutorUnityAction.Schedule(value);
        }

        public void Schedule(IEnumerator value) {
            mExecutorUnityEnumerator.Schedule(value);
        }

        public void ShutDown(System.Action value) {
            mExecutorUnityAction.ShutDown(value);
        }

        public void ShutDown(IEnumerator value) {
            mExecutorUnityEnumerator.ShutDown(value);
        }

        public void ShutDownAll(bool unityAction = true, bool unityEnum = true, bool threadAction = true) {
            if(unityAction)
                mExecutorUnityAction.ShutDownAll();
            if (unityEnum)
                mExecutorUnityEnumerator.ShutDownAll();
            if (threadAction)
                mExecutorThreadAction.ShutDownAll();
        }

        void OnDisable() {
            (mExecutorUnityAction as IUnityOnDisableListener).OnDisable();
            (mExecutorUnityEnumerator as IUnityOnDisableListener).OnDisable();
            (mExecutorThreadAction as IUnityOnDisableListener).OnDisable();
            this.StopAllCoroutines();
        }

        void OnApplicationQuit() {
            (mExecutorUnityAction as IUnityOnApplicationQuitListener).OnApplicationQuit();
            (mExecutorUnityEnumerator as IUnityOnApplicationQuitListener).OnApplicationQuit();
            (mExecutorThreadAction as IUnityOnApplicationQuitListener).OnApplicationQuit();
            this.StopAllCoroutines();
        }

        void Update() {
            mExecutorUnityAction.Update(this);
            mExecutorUnityEnumerator.Update(this);
            (mExecutorThreadAction as IUnityUpdateListener).Update();
        }

        void LateUpdate() {
            mExecutorUnityAction.LateUpdate(this);
            mExecutorUnityEnumerator.LateUpdate(this);
            (mExecutorThreadAction as IUnityLateUpdateListener).LateUpdate();
        }

    }
}
