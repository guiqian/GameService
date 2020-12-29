using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameService {
    public class Service_SharePrefernces : IGameService {

        public void SaveBoolean(string key, bool value) {
            if (HasKey(key))
                ClearByKey(key);
            PlayerPrefs.SetInt(key, value ? int.MaxValue : int.MinValue);
            PlayerPrefs.Save();
        }

        public void SaveString(string key, string value) {
            if (HasKey(key))
                ClearByKey(key);
            PlayerPrefs.SetString(key, value);
            PlayerPrefs.Save();
        }

        public void SaveInt(string key, int value) {
            if (HasKey(key))
                ClearByKey(key);
            PlayerPrefs.SetInt(key, value);
            PlayerPrefs.Save();
        }

        public void SaveFloat(string key, float value) {
            if (HasKey(key))
                ClearByKey(key);
            PlayerPrefs.SetFloat(key, value);
            PlayerPrefs.Save();
        }

        public string GetString(string key) {
            return HasKey(key) ? PlayerPrefs.GetString(key) : null;
        }

        public bool GetBoolean(string key) {
            return HasKey(key) ? PlayerPrefs.GetInt(key) > 0 : false;
        }

        public int GetInt(string key) {
            return HasKey(key) ? PlayerPrefs.GetInt(key) : int.MinValue;
        }

        public float GetFloat(string key) {
            return HasKey(key) ? PlayerPrefs.GetFloat(key) : float.NaN;
        }

        public bool HasKey(string key) {
            return PlayerPrefs.HasKey(key);
        }

        public void ClearAll() {
            PlayerPrefs.DeleteAll();
        }

        public void ClearByKey(string key) {
            if (HasKey(key)) {
                PlayerPrefs.DeleteKey(key);
            }
        }

        void System.IDisposable.Dispose() {
        }

    }
}