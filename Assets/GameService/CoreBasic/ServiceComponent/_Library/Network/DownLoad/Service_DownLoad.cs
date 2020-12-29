using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameService {
    public class Service_DownLoad : IGameService {

        private DownLoadCommon mDownLoadCommon = new DownLoadCommon();
        private DownLoadBreakPoint mDownLoadBreakPoint = new DownLoadBreakPoint();

        public void DownLoad(string url, string filePath, System.Action<float> callBack = null, bool isBreakPoint = false) {
            if (isBreakPoint) {
                mDownLoadBreakPoint.SetRangeEnd(true).DownLoad(url, filePath, callBack);
            } else {
                mDownLoadCommon.SetRangeEnd(true).DownLoad(url, filePath, (progress) => {
                    if (progress == DownLoadCommon.ErrorCode_DownLoadFail || progress == DownLoadCommon.ErrorCode_DownLoadStop) {
                        Service_File serviceFile = GameServiceContext.GetService<Service_File>();
                        serviceFile.DeleteFile(filePath);
                    }
                    if (callBack != null) {
                        callBack(progress);
                    }
                });
            }
        }

        public void Stop() {
            mDownLoadCommon.Stop();
            mDownLoadBreakPoint.Stop();
        }

        void System.IDisposable.Dispose() {
        }

    }
}