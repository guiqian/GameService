using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameService;

public class DemoEvent : MonoBehaviour {

    private readonly uint msgCode = 100;

    void Start() {
        GameServiceProxy.Service_EventCommon.AddEventListener(msgCode, OnMessageCallBack);

        // ============Dispatch
        object yourData = null;
        GameServiceProxy.Service_EventCommon.Dispatch(msgCode, yourData);
    }

    void OnDestroy() {
        GameServiceProxy.Service_EventCommon.RemoveEventListener(msgCode, OnMessageCallBack);
    }

    private void OnMessageCallBack(object msg) { // msg from server
    }

}
