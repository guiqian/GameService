using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameService {

    [System.Serializable]
    public class UIViewConfig {

        public List<UIViewConfigElement> Datas = new List<UIViewConfigElement>();

        public UIViewConfigElement FindUIViewConfigElement(string uiName) {
            return Datas.Find(x => x.UIName == uiName);
        }

    }

    [System.Serializable]
    public class UIViewConfigElement {

        public string UIName = null;
        public string UIPath = null;
        public string LaunchMode = null;
        public string Layer = null;

        public UIViewLayer ConverLayer {
            get { return (UIViewLayer)System.Enum.Parse(typeof(UIViewLayer), Layer); }
        }

        public UIViewLaunchMode ConvertLaunchMode {
            get { return (UIViewLaunchMode)System.Enum.Parse(typeof(UIViewLaunchMode), LaunchMode); }
        }

    }

    public enum UIViewLayer {
        Common = 0,
        Dialog = 1,
        Popup,
    }

    public enum UIViewLaunchMode {
        Default = 0,
        Singletop = 1,
    }

}