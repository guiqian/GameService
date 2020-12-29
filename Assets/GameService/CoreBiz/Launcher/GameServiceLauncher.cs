using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace GameService {

    public class GameServiceLauncher : MonoBehaviour, IModule<int> {

        protected IModule<int> mModuleApplication = new ModuleApplication();
        protected IModule<int> mModuleCheckDownloadUpdater = new ModuleCheckDownloadUpdater();
        protected IModule<int> mModuleHotFix = new ModuleHotFix();
        protected IModule<int> mModuleLoadRes = new ModuleLoadLocalRescources();

        async void Start() {
            OnPreStart();
            int code1 = await (this as IModule<int>).OnInit(null);
            int code2 = await OnLaunch();
            OnPostStart();
        }

        protected virtual void OnPreStart() {
        }

        protected virtual void OnPostStart() {
        }

        protected async virtual Task<int> OnLaunch() {
            int code1 = await (mModuleApplication as IModule<int>).OnInit(null); // 初始化应用信息
            int code2 = await (mModuleCheckDownloadUpdater as IModule<int>).OnInit(null); // 检查下载资源
            int code3 = await (mModuleHotFix as IModule<int>).OnInit(null); // 热更新
            int code4 = await (mModuleLoadRes as IModule<int>).OnInit(null); // 加载(解压)<已经下载好的>或<本地>的资源
            return 0;
        }

        async Task<int> IModule<int>.OnInit(object[] param) {
            return 0;
        }

    }

}
