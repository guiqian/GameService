using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameService {
    public class Service_EventCommon : IGameService {

        public delegate void OnActionHandler(object userData);
        private Dictionary<uint, LinkedList<OnActionHandler>> dic = new Dictionary<uint, LinkedList<OnActionHandler>>();

        public void AddEventListener(uint key, OnActionHandler handler) {
            LinkedList<OnActionHandler> lstHandler = null;
            dic.TryGetValue(key, out lstHandler);
            if (lstHandler == null) {
                lstHandler = new LinkedList<OnActionHandler>();
                dic[key] = lstHandler;
            }
            lstHandler.AddLast(handler);
        }

        public void RemoveEventListener(uint key, OnActionHandler handler) {
            LinkedList<OnActionHandler> lstHandler = null;
            dic.TryGetValue(key, out lstHandler);
            if (lstHandler != null) {
                lstHandler.Remove(handler);
                if (lstHandler.Count == 0) {
                    dic.Remove(key);
                }
            }
        }

        public void Dispatch(uint key, object userData = null) {
            LinkedList<OnActionHandler> lstHandler = null;
            dic.TryGetValue(key, out lstHandler);
            if (lstHandler != null) {
                for (LinkedListNode<OnActionHandler> curr = lstHandler.First; curr != null; curr = curr.Next) {
                    OnActionHandler handler = curr.Value;
                    if (handler != null) {
                        handler(userData);
                    }
                }
            }
        }

        void System.IDisposable.Dispose() {
            dic.Clear();
        }

    }
}