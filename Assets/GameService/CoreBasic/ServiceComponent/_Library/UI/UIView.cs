using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameService {
    public class UIView<T> : MonoBehaviour, IUIViewListener, IUnityPauseListener where T : IViewModelListener, new() {

        protected T ViewModel {
            get;
            private set;
        }

        //void Start() {
        //    (this as IUIViewListener).onInit(); // just test
        //    (this as IUIViewListener).onCreate(null); // just test
        //}

        void IUIViewListener.onInit() {
            ViewModel = new T();
        }

        void IUIViewListener.onCreate(object[] _params) {
            OnCreate(_params);
        }

        void IUIViewListener.onRestart(object[] _params) {
            OnRestart(_params);
        }

        void IUIViewListener.onClose() {
            (ViewModel as System.IDisposable).Dispose();
            ViewModel = default(T);
            OnClose();
        }

        protected virtual void OnCreate(object[] _params) {
        }

        protected virtual void OnRestart(object[] _params) {
        }

        protected virtual void OnClose() {
        }

        void IUnityPauseListener.onPause() {
            OnPause();
        }

        void IUnityPauseListener.onResume() {
            OnResume();
        }

        protected virtual void OnPause() {
        }

        protected virtual void OnResume() {
        }

        protected void Bind<V, CALLBACK>(string uiPath, BindablePropertyUIElement<V, CALLBACK> bindData, 
            BindablePropertyUIElement<V, CALLBACK>.UIElementValueChangedHandler valueChanged) where CALLBACK : UnityEngine.EventSystems.UIBehaviour {
            bindData.BindUI(gameObject, uiPath).BindCallBack(valueChanged);
        }

        GameObject IUIViewListener.AsGameObject() {
            return gameObject;
        }

        protected void CloseUI(params object[] values) {
            CloseUI(this, values);
        }

        protected void OpenUI<UI>(params object[] values) where UI : IUIViewListener {
            GameServiceProxy.Service_UIManager.OpenUI<UI>(values);
        }

        protected void CloseUI(IUIViewListener view, params object[] values) {
            GameServiceProxy.Service_UIManager.CloseUI(view, values);
        }

        protected void CloseUI<UI>(params object[] values) where UI : IUIViewListener {
            GameServiceProxy.Service_UIManager.CloseUI<UI>(values);
        }

    }
}
