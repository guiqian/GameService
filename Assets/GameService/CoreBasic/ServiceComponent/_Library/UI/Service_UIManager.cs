using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameService {
    public class Service_UIManager : IGameService {

        private Stack<IUIViewListener> mStack = new Stack<IUIViewListener>();
        public UIViewConfig UIViewConfig { get; private set; }

        public Service_UIManager() {
            InitConfigFile();
        }

        private void InitConfigFile() {
            TextAsset config = Resources.Load<TextAsset>(GameServiceDefine.Resources_UI_UIViewConfig);
            if (config != null) {
                UIViewConfig = JsonUtility.FromJson<UIViewConfig>(config.text);
                Resources.UnloadAsset(config);
            }
        }

        public Canvas Canvas { get; set; }

        public T OpenUI<T>(params object[] values) where T : IUIViewListener {
            string typeName = typeof(T).Name;
            if (UIViewConfig == null) {
                return _OpenUI_ByDefault<T>(values);
            } else {
                UIViewConfigElement configElement = UIViewConfig.FindUIViewConfigElement(typeName);
                if (configElement == null) {
                    return _OpenUI_ByDefault<T>(values);
                } else {
                    return _OpenUI_ByConfig<T>(configElement, values);
                }
            }
        }

        private T _OpenUI_ByDefault<T>(params object[] values) where T : IUIViewListener {
            return _OpenUI_ByConfig<T>(DefaultConfig(typeof(T).Name), values);
        }

        private UIViewConfigElement DefaultConfig(string name) {
            UIViewConfigElement configElement = new UIViewConfigElement();
            configElement.UIPath = string.Format(GameServiceDefine.Resources_UI_XXX, name);
            configElement.UIName = name;
            configElement.LaunchMode = System.Enum.GetName(typeof(UIViewLaunchMode), UIViewLaunchMode.Default);
            configElement.Layer = System.Enum.GetName(typeof(UIViewLayer), UIViewLayer.Common);
            return configElement;
        }

        private T _OpenUI_ByConfig<T>(UIViewConfigElement configElement, params object[] values) where T : IUIViewListener {
            if (configElement.ConvertLaunchMode == UIViewLaunchMode.Default) {
                return _OpenUI_LauncherMode_Default<T>(configElement, values);
            } else {
                return _OpenUI_LauncherMode_SingleTop<T>(configElement, values);
            }
        }

        private T _OpenUI_LauncherMode_Default<T>(UIViewConfigElement configElement, params object[] values) where T : IUIViewListener {
            GameObject src = Resources.Load<GameObject>(configElement.UIPath);
            if (src != null) {
                GameObject one = GameObject.Instantiate(src);
                one.name = typeof(T).Name;
                one.transform.SetParent(Canvas.transform, true);
                one.transform.localPosition = Vector3.zero;
                one.transform.localRotation = Quaternion.identity;
                one.transform.localScale = Vector3.one;
                // ==================================
                IUIViewListener view = one.GetComponent<IUIViewListener>();
                if (view == null) {
                    view = (IUIViewListener)one.AddComponent(typeof(T));
                }
                mStack.Push(view);
                // ==================================
                view.onInit();
                view.onCreate(values);
                return (T)view;
            }
            return default(T);
        }

        private T _OpenUI_LauncherMode_SingleTop<T>(UIViewConfigElement configElement, params object[] values) where T : IUIViewListener {
            if (HasExsit<T>()) {
                return _OpenUI_StackTop<T>(values);
            } else {
                return _OpenUI_LauncherMode_Default<T>(configElement, values);
            }
        }

        private T _OpenUI_StackTop<T>(params object[] values) where T : IUIViewListener {
            Stack<IUIViewListener> tempStack = new Stack<IUIViewListener>();
            IUIViewListener result = null;
            for (int i = mStack.Count - 1; i >= 0; i--) {
                IUIViewListener current = mStack.Pop();
                if (current.AsGameObject().name == typeof(T).Name) {
                    result = current;
                    break;
                } else {
                    tempStack.Push(current);
                }
            }
            // ========================================
            Stack<IUIViewListener>.Enumerator e = tempStack.GetEnumerator();
            while (e.MoveNext()) {
                mStack.Push(e.Current);
            }
            mStack.Push(result);
            tempStack.Clear();
            result.AsGameObject().transform.SetAsLastSibling();
            return (T)result;
        }

        private bool HasExsit<T>() where T : IUIViewListener {
            Stack<IUIViewListener>.Enumerator e = mStack.GetEnumerator();
            IUIViewListener result = null;
            while (e.MoveNext()) {
                IUIViewListener current = e.Current;
                if (current.AsGameObject().name == typeof(T).Name) {
                    result = current;
                    break;
                }
            }
            return result != null;
        }

        public void CloseUI<T>(params object[] values) where T : IUIViewListener {
            Stack<IUIViewListener>.Enumerator e = mStack.GetEnumerator();
            while (e.MoveNext()) {
                IUIViewListener view = e.Current;
                if (view.AsGameObject().name == typeof(T).Name) {
                    _Close(view, values);
                    break;
                }
            }
        }

        public void CloseUI(IUIViewListener view, params object[] values) {
            _Close(view, values);
        }

        private void _Close(IUIViewListener view, params object[] values) {
            if (view != null) {
                view.onClose();
                mStack.Pop();
                GameObject.DestroyImmediate(view.AsGameObject());
            }
            if (mStack.Count > 0) {
                IUIViewListener current = mStack.Peek();
                current.onRestart(values);
            }
        }

        void System.IDisposable.Dispose() {
            Stack<IUIViewListener>.Enumerator e = mStack.GetEnumerator();
            while (e.MoveNext()) {
                IUIViewListener view = e.Current;
                if (view != null) {
                    view.onClose();
                    GameObject.DestroyImmediate(view.AsGameObject());
                }
            }
            mStack.Clear();
        }

    }
}