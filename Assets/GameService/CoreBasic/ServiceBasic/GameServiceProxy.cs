using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameService {

    public class GameServiceProxy {

        static GameServiceProxy() {
            Service_Application = GetSetGameService<Service_Application>();
            Service_Coroutine = GetSetGameService<Service_Coroutine>();
            Service_XPlatformCommunication = GetSetGameService<Service_XPlatformCommunication>();
            Service_Log = GetSetGameService<Service_Log>();
            Service_File = GetSetGameService<Service_File>();
            Service_GameObject = GetSetGameService<Service_GameObject>();
            Service_Time = GetSetGameService<Service_Time>();
            Service_EventCommon = GetSetGameService<Service_EventCommon>();
            Service_String = GetSetGameService<Service_String>();
            Service_Code = GetSetGameService<Service_Code>();
            Service_SceneLoader = GetSetGameService<Service_SceneLoader>();
            Service_SharePrefernces = GetSetGameService<Service_SharePrefernces>();
            Service_NetWork = GetSetGameService<Service_NetWork>();
            Service_Animation = GetSetGameService<Service_Animation>();
            Service_DownLoad = GetSetGameService<Service_DownLoad>();
            Service_UIManager = GetSetGameService<Service_UIManager>();
        }

        private static T GetSetGameService<T>() where T : IGameService, new() {
            bool result = GameServiceContext.RegisterService<T>();
            return result ? GameServiceContext.GetService<T>() : default(T);
        }

        public static Service_Coroutine Service_Coroutine { get; private set; }
        public static Service_SceneLoader Service_SceneLoader { get; private set; }
        public static Service_Code Service_Code { get; private set; }
        public static Service_String Service_String { get; private set; }
        public static Service_EventCommon Service_EventCommon { get; private set; }
        public static Service_Time Service_Time { get; private set; }
        public static Service_GameObject Service_GameObject { get; private set; }
        public static Service_XPlatformCommunication Service_XPlatformCommunication { get; private set; }
        public static Service_File Service_File { get; private set; }
        public static Service_Log Service_Log { get; private set; }
        public static Service_Application Service_Application { get; private set; }
        public static Service_SharePrefernces Service_SharePrefernces { get; private set; }
        public static Service_NetWork Service_NetWork { get; private set; }
        public static Service_Animation Service_Animation { get; private set; }
        public static Service_DownLoad Service_DownLoad { get; private set; }
        public static Service_UIManager Service_UIManager { get; private set; }
    }

}

