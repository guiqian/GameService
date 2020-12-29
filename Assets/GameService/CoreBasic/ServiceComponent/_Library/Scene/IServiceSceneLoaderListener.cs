using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameService {

    public interface IServiceSceneLoaderListener {
        void OnSceneLoad(params object[] p);
    }

}

