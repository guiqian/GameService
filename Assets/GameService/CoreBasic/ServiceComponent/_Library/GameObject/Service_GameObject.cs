using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameService {
    public class Service_GameObject : IGameService {

        public T GetOrAddComponent<T>(GameObject obj) where T : MonoBehaviour {
            T com = obj.GetComponent<T>();
            if (com == null) {
                com = obj.AddComponent<T>();
            }
            return com;
        }

        public bool DestroyComponent<T>(GameObject obj) where T : MonoBehaviour {
            T com = obj.GetComponent<T>();
            if (com != null) {
                GameObject.DestroyImmediate(com);
                return true;
            }
            return false;
        }

        void System.IDisposable.Dispose() {
        }

    }
}
