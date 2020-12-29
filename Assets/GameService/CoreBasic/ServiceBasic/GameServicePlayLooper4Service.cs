using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameService {

    public class GameServicePlayLooper4Service : GameServicePlayLooper, IGameServiceRegister<IGameServiceUpdate> {

        private List<KeyValuePair<string, IGameServiceUpdate>> mList = new List<KeyValuePair<string, IGameServiceUpdate>>();
        private List<int> mListPrepareRemove = new List<int>();
        private bool mListClearFlag = false;

        bool IGameServiceRegister<IGameServiceUpdate>.Register(string key, IGameServiceUpdate update) {
            if (string.IsNullOrEmpty(key) == false && update != null) {
                int index = mList.FindIndex(x => x.Key == key);
                if (index < 0) {
                    mList.Add(new KeyValuePair<string, IGameServiceUpdate>(key, update));
                    return true;
                }
            }
            return false;
        }

        bool IGameServiceRegister<IGameServiceUpdate>.UnRegister(string key) {
            int index = mList.FindIndex(x => x.Key == key);
            if (index >= 0 && mListPrepareRemove.Contains(index) == false) {
                mListPrepareRemove.Add(index);
                return true;
            }
            return false;
        }

        public void Clear() {
            mListClearFlag = true;
        }

        protected override void OnUpdate() {
            base.OnUpdate();
            HandleOnUpdate();
        }

        protected override void OnLateUpdate() {
            base.OnLateUpdate();
            HandleOnLateUpdateRemove();
            HandleOnLateUpdateClear();
        }

        private void HandleOnLateUpdateClear() {
            if (mListClearFlag) {
                mListPrepareRemove.Clear();
                mList.Clear();
                mListClearFlag = false;
            }
        }

        private void HandleOnLateUpdateRemove() {
            int count = mListPrepareRemove.Count;
            for (int i = 0; i < count; i++) {
                int value = mListPrepareRemove[i];
                mList.RemoveAt(value);
                mListPrepareRemove.Remove(value);
                count--;
                i--;
            }
        }

        private void HandleOnUpdate() {
            int count = mList.Count;
            for (int i = 0; i < count; i++) {
                KeyValuePair<string, IGameServiceUpdate> pair = mList[i];
                if (pair.Value != null) {
                    pair.Value.OnUpdate();
                }
            }
        }

    }
}