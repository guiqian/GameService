using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameService {

    public class Service_XPlatformCommunication : IGameService {

        private WindowsCommucation commWindows = new WindowsCommucation();
        private AndroidCommunication commAndroid = new AndroidCommunication();
        private IOSCommunication commIOS = new IOSCommunication();

        public AndroidCommunication Android {
            get { return commAndroid; }
        }

        public IOSCommunication IOS {
            get { return commIOS; }
        }

        public WindowsCommucation Windows {
            get { return commWindows; }
        }

        void System.IDisposable.Dispose() {
        }

    }
}