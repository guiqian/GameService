using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameService;

public class DemoNetWork : MonoBehaviour {

    private void DownLoadFile() {
        GameServiceProxy.Service_DownLoad.DownLoad("https://www.baidu.com/A.mp4", "D:/A.mp4", (progress) => {
            Debug.Log("progress:" + progress);
        }, true);
    }

    private void UDP() {
        GameServiceProxy.Service_NetWork.UDP_Sender.Init("ip", -1);
        GameServiceProxy.Service_NetWork.UDP_Reciver.Init("ip", -1);
        // ==========send msg
        byte[] data = null;
        GameServiceProxy.Service_NetWork.UDP_Sender.SendMsg(data);
        GameServiceProxy.Service_NetWork.Event.AddEventListener(100, OnNetWorkMessageCallBack);
    }

    private void OnNetWorkMessageCallBack(object msg) { // msg from server
    }

}
