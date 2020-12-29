using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace GameService {
    public class ThreadActionExecutor : IExecutorSchedule<System.Action>, IUnityListener {

        void IUnityAwakeListener.Awake() {
        }

        void IUnityFixedUpdateListener.FixedUpdate() {
        }

        void IUnityLateUpdateListener.LateUpdate() {
        }

        void IUnityOnApplicationQuitListener.OnApplicationQuit() {
        }

        void IUnityOnDestroyListener.OnDestroy() {
        }

        void IUnityOnDisableListener.OnDisable() {
        }

        void IUnityOnEnableListener.OnEnable() {
        }

        public void Schedule(Action value) {
            if (value != null) {
                ThreadPool.QueueUserWorkItem((state) => {
                    var thread = Thread.CurrentThread;
                    value();
                });
            }
        }

        public void ShutDown(Action value) {
        }

        public void ShutDownAll() {
        }

        void IUnityStartListener.Start() {
        }

        void IUnityUpdateListener.Update() {
        }

    }
}