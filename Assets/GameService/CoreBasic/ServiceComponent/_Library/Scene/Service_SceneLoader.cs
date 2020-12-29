using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameService {
    public class Service_SceneLoader : IGameService {

        private Dictionary<string, object[]> mParams = new Dictionary<string, object[]>();

        public void LoadSceneAsync(string name, LoadSceneMode mode = LoadSceneMode.Single, System.Action<float> progress = null, params object[] p) {
            RegisterEventLoaded(name, p);
            AsyncOperation operation = SceneManager.LoadSceneAsync(name, mode);

            IEnumerator task = null;
            task = OnLoadSceneAsync(operation, (percent) => {
                if (progress != null) {
                    progress(percent);
                }
                if (percent >= 1.0f) {
                    Executor.Instance.ShutDown(task);
                    task = null;
                }
            });
            Executor.Instance.Schedule(task);
        }

        public void LoadSceneAsync(int buildIndex, LoadSceneMode mode = LoadSceneMode.Single, System.Action<float> progress = null, params object[] p) {
            Scene scene = SceneManager.GetSceneByBuildIndex(buildIndex);
            if (scene != null) {
                LoadSceneAsync(scene.name, mode, progress, p);
            } else {
                throw new System.Exception(GameServiceDefine.ExceptionMsg_NotFoundScene);
            }
        }

        private IEnumerator OnLoadSceneAsync(AsyncOperation operation, System.Action<float> progress = null) {
            operation.allowSceneActivation = false;
            while (operation.isDone == false || operation.progress < 0.9f) {
                if (progress != null)
                    progress(operation.progress);
                yield return null;
            }
            operation.allowSceneActivation = true;

            while (operation.progress < 1.0f) {
                if (progress != null)
                    progress(operation.progress);
                yield return null;
            }
            if (progress != null)
                progress(1.0f);
        }

        public void LoadScene(string name, LoadSceneMode mode = LoadSceneMode.Single, params object[] p) {
            RegisterEventLoaded(name, p);
            SceneManager.LoadScene(name, mode);
        }

        public void LoadScene(int buildIndex, LoadSceneMode mode = LoadSceneMode.Single, params object[] p) {
            Scene scene = SceneManager.GetSceneByBuildIndex(buildIndex);
            if (scene != null) {
                LoadScene(scene.name, mode, p);
            } else {
                throw new System.Exception(GameServiceDefine.ExceptionMsg_NotFoundScene);
            }
        }

        private void RegisterEventLoaded(string sceneName, params object[] p) {
            if (p != null && p.Length > 0) {
                mParams.Add(sceneName, p);
            }
            SceneManager.sceneLoaded += OnSceneLoaded; // 注册
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            string key = scene.name;
            object[] value = null;
            if (mParams.TryGetValue(key, out value)) {
                mParams.Remove(key);
            }
            GameObject[] array = scene.GetRootGameObjects();
            if (array != null) {
                for (int i = 0, count = array.Length; i < count; i++) {
                    GameObject one = array[i];
                    if (one != null && one.activeSelf) {
                        IServiceSceneLoaderListener listener = one.GetComponent<IServiceSceneLoaderListener>();
                        if (listener != null) {
                            listener.OnSceneLoad(value);
                        }
                    }
                }
            }
            SceneManager.sceneLoaded -= OnSceneLoaded; // 解注册
        }

        void System.IDisposable.Dispose() {
        }
    }
}