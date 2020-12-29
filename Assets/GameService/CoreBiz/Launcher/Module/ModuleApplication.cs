using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace GameService {
    public class ModuleApplication : IModule<int> {

        async Task<int> IModule<int>.OnInit(object[] param) {
            GameServiceProxy.Service_Application.TargetFrameRate = 60;
            GameServiceProxy.Service_Application.RunInBackground = true;
            //GameServiceProxy.Service_NetWork.UDPReciver.Init("ip", -1);
            //GameServiceProxy.Service_NetWork.UDPSender.Init("ip", -1);
            return 0;
        }

    }
}
