using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameService {
    public class GameServiceContext {

        private volatile static GameServiceContext mInstance = null;
        private static object mInstanceLock = new object();
        private GameServicePlayLooper4Service mServiceLooper = null;
        private GameServiceContainer mServiceContainer = null;

        internal static GameServiceContext INSTANCE {
            get {
                lock (mInstanceLock) {
                    if (mInstance == null) {
                        mInstance = new GameServiceContext();
                    }
                }
                return mInstance;
            }
        }

        private GameServiceContext() {
            mServiceContainer = new GameServiceContainer();
            mServiceLooper = new GameServicePlayLooper4Service();
            mServiceLooper.Start();
        }

        public static bool RegisterService<T>() where T : IGameService, new() {
            return INSTANCE.RegisterService(new T());
        }

        public static bool UnRegisterService<T>() where T : IGameService, new() {
            return INSTANCE.UnRegisterService(new T());
        }

        public static T GetService<T>() where T : IGameService, new() {
            return (T)INSTANCE.GetService(new T());
        }

        // ======================================================

        private bool HasInterface<T>(string className) {
            System.Type clazz = System.Type.GetType(className);
            if (clazz.IsClass || clazz.IsInterface) {
                if (clazz.GetInterface(typeof(T).Name) != null) {
                    return true;
                }
            }
            return false;
        }

        internal bool UnRegisterService(IGameService service) {
            string name = service.GetType().FullName;
            if (HasInterface<IGameServiceUpdate>(name)) {
                (mServiceLooper as IGameServiceRegister<IGameServiceUpdate>).UnRegister(name);
            }
            return (mServiceContainer as IGameServiceRegister<IGameService>).UnRegister(name);
        }

        internal bool RegisterService(IGameService service) {
            string name = service.GetType().FullName;
            if (HasInterface<IGameServiceUpdate>(name)) {
                (mServiceLooper as IGameServiceRegister<IGameServiceUpdate>).Register(name, service as IGameServiceUpdate);
            }
            return (mServiceContainer as IGameServiceRegister<IGameService>).Register(name, service);
        }

        internal IGameService GetService(IGameService service) {
            string name = service.GetType().FullName;
            IGameService result = mServiceContainer.TryGetValue(name);
            if (result == null) {
                string msg = string.Format(GameServiceDefine.ExceptionMsg_NotFoundService, typeof(GameServiceContext).Name, name);
                throw new System.Exception(msg);
            }
            return result;
        }

    }
}

