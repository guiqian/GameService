using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameService {
    public interface IUnityPauseListener {

        void onPause();

        void onResume();

    }
}
