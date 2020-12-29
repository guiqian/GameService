using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameService {
    public class DownLoadBreakPoint : DownLoadCommon {

        public override void DownLoad(string url, string filePath, Action<float> callBack = null) {
            base.DownLoadReal(url, filePath, callBack);
        }

    }
}
