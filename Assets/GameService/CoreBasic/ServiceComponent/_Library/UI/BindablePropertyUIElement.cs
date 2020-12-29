using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameService {
    public class BindablePropertyUIElement<T, UI> : BindableProperty<T>, System.IDisposable where UI : UnityEngine.EventSystems.UIBehaviour {

        public delegate void UIElementValueChangedHandler(T oldValue, T newValue, UI component);
        public UIElementValueChangedHandler OnUIElementValueChanged;

        private string mUIPath = null;
        private UIElementType mUIType = UIElementType.None;
        private UI mComponent = null;

        public BindablePropertyUIElement() {
            this.OnValueChanged = null;
            this.OnValueChanged = OnValueChanged4ParentClass;
        }

        public BindablePropertyUIElement<T, UI> BindCallBack(UIElementValueChangedHandler valueChanged) {
            this.OnUIElementValueChanged = null;
            this.OnUIElementValueChanged = valueChanged;
            return this;
        }

        public BindablePropertyUIElement<T, UI> BindUI(GameObject obj, string uiPath) {
            if (string.IsNullOrEmpty(uiPath) == false) {
                Transform child = obj.transform.Find(uiPath);
                if (child != null) {
                    mComponent = (UI)child.GetComponent(typeof(UI));
                } else {
                    mComponent = null;
                }
            } else {
                mComponent = null;
            }
            return this;
        }

        private void OnValueChanged4ParentClass(T oldValue, T newValue) {
            if (OnUIElementValueChanged != null) {
                OnUIElementValueChanged(oldValue, newValue, mComponent);
            }
        }

        public void NotifyOnValueChanged() {
            OnValueChanged4ParentClass(_old, _value);
        }

        public void Dispose() {
            this.OnUIElementValueChanged = null;
            this.OnValueChanged = null;
            mUIType = UIElementType.None;
            mUIPath = null;
            mComponent = null;
            Value = default(T);
        }

    }
}
