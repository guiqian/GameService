using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameService;

public class DemoScene : MonoBehaviour {

    void Start() {
        string yourDataString = "your data";
        int yourDataInteger = 100;
        float yourDataFloat = -2f;
        GameServiceProxy.Service_SceneLoader.LoadSceneAsync("your scene name", LoadSceneMode.Single, (percent) => {
        }, yourDataString, yourDataInteger, yourDataFloat);
    }

}

// guiqian: this script in new scene(note: this gameobject is root gameobject!!!)
public class MonoNextScene : MonoBehaviour, GameService.IServiceSceneLoaderListener {

    public void OnSceneLoad(params object[] p) {
        // param is your data
    }

}