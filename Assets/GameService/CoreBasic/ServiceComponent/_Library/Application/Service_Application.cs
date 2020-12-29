using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameService {
    public class Service_Application : IGameService {

        public int TargetFrameRate {
            get { return Application.targetFrameRate; }
            set { Application.targetFrameRate = value; }
        }

        public RuntimePlatform RuntimePlatform {
            get { return Application.platform; }
        }

        public string RuntimePlatformName {
            get { return System.Enum.GetName(typeof(RuntimePlatform), RuntimePlatform); }
        }

        public bool RunInBackground {
            get { return Application.runInBackground; }
            set { Application.runInBackground = value; }
        }

        public void Quit() {
            Application.Quit();
        }

        public string PersistentPath {
            get { return GameServiceDefine.PersistentPath; }
        }

        void System.IDisposable.Dispose() {
        }

    }
}
