using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameService;

public class DemoCoroutine : MonoBehaviour {

    private IEnumerator yourTask = null;

    void Start() {
        yourTask = YourTask();
        GameServiceProxy.Service_Coroutine.StartCoroutine(yourTask);
    }

    void OnDisable() {
        GameServiceProxy.Service_Coroutine.StopCoroutine(yourTask);
        yourTask = null;
    }

    void InMethod() {
        IEnumerator yourTempTask = null;
        yourTempTask = YourTempTask(() => {
            // your logic
            GameServiceProxy.Service_Coroutine.StopCoroutine(yourTempTask);
        });
        GameServiceProxy.Service_Coroutine.StartCoroutine(yourTempTask);
    }

    private IEnumerator YourTempTask(System.Action finish) {
        yield return new WaitForSeconds(3f);
        finish();
    }

    private IEnumerator YourTask() {
        yield return null;
    }

}
