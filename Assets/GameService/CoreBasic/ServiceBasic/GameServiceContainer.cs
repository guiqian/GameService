using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameService {
    public class GameServiceContainer : IGameServiceRegister<IGameService> {

        private Dictionary<string, IGameService> mDic = new Dictionary<string, IGameService>();
        
        bool IGameServiceRegister<IGameService>.Register(string key, IGameService service) {
            if (mDic.ContainsKey(key) == false) {
                mDic.Add(key, service);
                return true;
            }
            return false;
        }

        bool IGameServiceRegister<IGameService>.UnRegister(string key) {
            IGameService service = null;
            if (mDic.TryGetValue(key, out service)) {
                service.Dispose();
                mDic.Remove(key);
                return true;
            }
            return false;
        }

        public IGameService TryGetValue(string key) {
            IGameService result = null;
            if (mDic.TryGetValue(key, out result)) {
            }
            return result;
        }

    }
}
