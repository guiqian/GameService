using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameService {

    public interface IUnityAwakeListener {
        void Awake();
    }

    public interface IUnityStartListener {
        void Start();
    }

    public interface IUnityUpdateListener {
        void Update();
    }

    public interface IUnityLateUpdateListener {
        void LateUpdate();
    }

    public interface IUnityOnDisableListener {
        void OnDisable();
    }

    public interface IUnityOnApplicationQuitListener {
        void OnApplicationQuit();
    }

    public interface IUnityOnEnableListener {
        void OnEnable();
    }

    public interface IUnityFixedUpdateListener {
        void FixedUpdate();
    }

    public interface IUnityOnDestroyListener {
        void OnDestroy();
    }

    public interface IUnityListener : IUnityAwakeListener, IUnityStartListener, 
        IUnityUpdateListener, IUnityLateUpdateListener,
        IUnityOnDisableListener, IUnityOnApplicationQuitListener,
        IUnityOnEnableListener, IUnityFixedUpdateListener, IUnityOnDestroyListener {
    }
}
