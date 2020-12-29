using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameService {
    public interface IUIViewListener {

        void onInit();

        void onCreate(object[] _params);

        void onRestart(object[] _params);

        void onClose();

        GameObject AsGameObject();

    }
}
