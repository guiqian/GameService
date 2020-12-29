using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameService {
    public class GameServiceDefine {

        public static readonly string ExceptionMsg_NotFoundScene = "Not Found Scene";
        public static readonly string ExceptionMsg_NotFoundService = "In {0}. No Service Name: {1}";
        public static readonly string Resources_UI_UIViewConfig = "UI/UIViewConfig";
        public static readonly string Resources_UI_XXX = "UI/{0}";

        public static string PersistentPath {
            get {
#if UNITY_EDITOR
                return Application.streamingAssetsPath;
#elif UNITY_STANDALONE
                return Application.persistentDataPath;
#elif UNITY_IOS || UNITY_IPHONE
                return Application.persistentDataPath;
#elif UNITY_ANDROID
                return Application.persistentDataPath;
#endif
            }
        }

    }
}

