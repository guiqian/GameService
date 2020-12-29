using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace GameService {
    public class DownLoadCommon {

        public float progress { get; private set; } = 0f;
        public bool isDone { get; private set; } = false;
        private bool isStop = false;
        public bool isRunning { get; private set; } = false;
        public readonly static float ErrorCode_DownLoadFail = -101f;
        public readonly static float ErrorCode_DownLoadStop = -102f;
        private bool isRangeEnd_ = true;

        public virtual void DownLoad(string url, string filePath, System.Action<float> callBack = null) {
            DownLoadReal(url, filePath, callBack);
        }

        public DownLoadCommon SetRangeEnd(bool is_) {
            this.isRangeEnd_ = is_;
            return this;
        }

        public void Stop() {
            isStop = true;
        }

        protected void DownLoadReal(string url, string filePath, System.Action<float> callBack = null) {
            Executor instance = Executor.Instance;
            IEnumerator task = null;
            task = OnDownTask(url, filePath, (progress) => {
                if (callBack != null) {
                    callBack(progress);
                }
                if (progress >= 1f || progress == ErrorCode_DownLoadFail || progress == ErrorCode_DownLoadStop) {
                    instance.ShutDown(task);
                }
            });
            instance.Schedule(task);
        }

        protected IEnumerator OnDownTask(string url, string filePath, System.Action<float> callBack = null) {
            isRunning = true;
            isDone = false;
            progress = 0f;
            isStop = false;
            // ============================================
            bool isError = false; // 默认没有错误
            if (string.IsNullOrEmpty(url) == false && string.IsNullOrEmpty(filePath) == false) {
                var headRequest = UnityWebRequest.Head(url); // Get
                UnityWebRequestAsyncOperation requestAsync = headRequest.SendWebRequest();
                yield return requestAsync;

                isError = HasDownLoadError(headRequest);
                if (isError == false) {
                    var totalLength = long.Parse(headRequest.GetResponseHeader("Content-Length"));
                    var dirPath = Path.GetDirectoryName(filePath);
                    if (Directory.Exists(dirPath) == false) {
                        Directory.CreateDirectory(dirPath);
                    }

                    FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                    var fileLength = fs.Length;
                    if (fileLength < totalLength) {
                        fs.Seek(fileLength, SeekOrigin.Begin);

                        var request = UnityWebRequest.Get(url);
                        if (isRangeEnd_) {
                            request.SetRequestHeader("Range", "bytes=" + fileLength + "-");
                        } else {
                            request.SetRequestHeader("Range", "bytes=" + fileLength + "-" + totalLength);
                        }
                        request.SendWebRequest();

                        var index = 0;
                        while (request.isDone == false) {
                            isError = HasDownLoadError(request);
                            if (isError) {
                                break;
                            }
                            if (isStop) {
                                break;
                            }
                            yield return null;
                            Progressing(callBack, totalLength, fs, ref fileLength, request, ref index); ;
                        }
                        if (fileLength < totalLength) {
                            Progressing(null, totalLength, fs, ref fileLength, request, ref index);
                        }
                        request.Dispose();
                        request = null;
                    } else {
                        progress = 1f;
                    }

                    fs.Flush();
                    fs.Close();
                    fs.Dispose();
                    fs = null;
                }
                headRequest.Dispose();
                headRequest = null;
            }
            isRunning = false;
            System.GC.Collect();

            if (isError) {
                progress = 0f;
                isError = false;
                if (callBack != null)
                    callBack(ErrorCode_DownLoadFail);
            } else if (isStop) {
                progress = 0f;
                isStop = false;
                if (callBack != null)
                    callBack(ErrorCode_DownLoadStop);
            } else {
                if (progress >= 1f) {
                    progress = 0f;
                    isDone = true;
                    if (callBack != null) {
                        callBack(1f);
                    }
                }
            }
        }

        private void Progressing(System.Action<float> callBack, long totalLength, FileStream fs, ref long fileLength, UnityWebRequest request, ref int index) {
            var _buff = request.downloadHandler.data;
            if (_buff != null) {
                var length = _buff.Length - index;
                fs.Write(_buff, index, length);
                fs.Flush();
                index += length;
                fileLength += length;

                if (fileLength >= totalLength) {
                    progress = 1f;
                } else {
                    progress = fileLength / (float)totalLength;
                    if (callBack != null) {
                        callBack(progress);
                    }
                }
            }
        }

        protected bool HasDownLoadError(UnityWebRequest request) {
            return string.IsNullOrEmpty(request.error) == false;
        }

    }
}
