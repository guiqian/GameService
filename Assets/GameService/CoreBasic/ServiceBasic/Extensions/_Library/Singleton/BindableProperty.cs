using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameService {

    public class BindableProperty<T> {

        public delegate void ValueChangedHandler(T oldValue, T newValue);

        public ValueChangedHandler OnValueChanged;

        protected T _value, _old;
        public T Value {
            get {
                return _value;
            }
            set {
                if (!object.Equals(_value, value)) {
                    _old = _value;
                    _value = value;
                    ValueChanged(_old, _value);
                }
            }
        }

        private void ValueChanged(T oldValue, T newValue) {
            if (OnValueChanged != null) {
                OnValueChanged(oldValue, newValue);
            }
        }

        public override string ToString() {
            return (Value != null ? Value.ToString() : "null");
        }

    }
}

