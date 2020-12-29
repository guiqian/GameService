using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameService {
    public class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T> {

        private static T _instance;
        public static T Instance {
            get {
                if (_instance == null) {
                    T temp = GameObject.FindObjectOfType<T>();
                    if (temp == null) {
                        GameObject go = new GameObject(typeof(T).FullName);
                        go.transform.localPosition = Vector3.zero;
                        go.transform.localRotation = Quaternion.identity;
                        go.transform.localScale = Vector3.one;
                        DontDestroyOnLoad(go);
                        temp = go.AddComponent<T>();
                    }
                    _instance = temp;
                }
                return _instance;
            }
        }

        public void Dispose() {
            if (_instance != null) {
                GameObject temp = _instance.gameObject;
                if (temp != null) {
                    _instance = null;
                    GameObject.DestroyImmediate(temp.gameObject);
                }
                temp = null;
            } else {
                T temp = GameObject.FindObjectOfType<T>();
                if (temp != null) {
                    _instance = null;
                    GameObject.DestroyImmediate(temp.gameObject);
                }
                temp = null;
            }
        }

    }
}

