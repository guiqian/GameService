using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameService {
    public class AndroidCommunication {


        #region Find_JavaObject
        public T FindObject_CallMethod<T>(string className, string fieldName, bool isFieldStatic, string methodName, object[] methodParams) {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass clazz = new AndroidJavaClass(className);
        AndroidJavaObject obj = isFieldStatic ? clazz.GetStatic<AndroidJavaObject>(fieldName) : clazz.Get<AndroidJavaObject>(fieldName);
        T result = IsAvailable(methodParams) ? obj.Call<T>(methodName, methodParams) : obj.Call<T>(methodName);
        obj.Dispose();
        clazz.Dispose();  
        return result;
#else
            return default(T);
#endif
        }

        public void FindObject_CallMethod(string className, string fieldName, bool isFieldStatic, string methodName, object[] methodParams) {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass clazz = new AndroidJavaClass(className);
        AndroidJavaObject obj = isFieldStatic ? clazz.GetStatic<AndroidJavaObject>(fieldName) : clazz.Get<AndroidJavaObject>(fieldName);
        obj.Call(methodName, methodParams);
        obj.Dispose();
        clazz.Dispose();
#endif
        }

        public T FindObject_CallStaticMethod<T>(string className, string fieldName, bool isFieldStatic, string methodName, object[] methodParams) {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass clazz = new AndroidJavaClass(className);
        AndroidJavaObject obj = isFieldStatic ? clazz.GetStatic<AndroidJavaObject>(fieldName) : clazz.Get<AndroidJavaObject>(fieldName);
        T result = IsAvailable(methodParams) ? obj.CallStatic<T>(methodName, methodParams) : clazz.CallStatic<T>(methodName);
        obj.Dispose();
        clazz.Dispose();
        return result;
#else
            return default(T);
#endif
        }

        public void FindObject_CallStaticMethod(string className, string fieldName, bool isFieldStatic, string methodName, object[] methodParams) {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass clazz = new AndroidJavaClass(className);
        AndroidJavaObject obj = isFieldStatic ? clazz.GetStatic<AndroidJavaObject>(fieldName) : clazz.Get<AndroidJavaObject>(fieldName);
        obj.CallStatic(methodName, methodParams);
        obj.Dispose();
        clazz.Dispose();
#endif
        }
        #endregion Find_JavaObject

        // =============================================================================

        #region Create_JavaObject
        public T CreateObject_CallMethod<T>(string className, object[] classParams, string methodName, object[] methodParams) {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaObject obj = CreateAndroidJavaObject(className, classParams);
        return CallMethod<T>(obj, methodName, methodParams);
#else
            return default(T);
#endif
        }

        public void CreateObject_CallMethod(string className, object[] classParams, string methodName, object[] methodParams) {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaObject obj = CreateAndroidJavaObject(className, classParams);
        CallMethod(obj, methodName, methodParams);
#endif
        }

        public T CreateObject_CallStaticMethod<T>(string className, object[] classParams, string methodName, object[] methodParams) {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaObject obj = CreateAndroidJavaObject(className, classParams);
        return CallStaticMethod<T>(obj, methodName, methodParams);
#else
            return default(T);
#endif
        }

        public void CreateObject_CallStaticMethod(string className, object[] classParams, string methodName, object[] methodParams) {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaObject obj = CreateAndroidJavaObject(className, classParams);
        CallStaticMethod(obj, methodName, methodParams);
#endif
        }
        #endregion Create_JavaObject

        // =============================================================================

        #region Utils
#if UNITY_ANDROID && !UNITY_EDITOR
    public void CallStaticMethod(AndroidJavaObject obj, string methodName, object[] methodParams) {
        if (IsAvailable(methodParams)) {
            obj.CallStatic(methodName, methodParams);
        } else {
            obj.CallStatic(methodName);
        }
        obj.Dispose();
    }
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
    public T CallMethod<T>(AndroidJavaObject obj, string methodName, object[] methodParams) {
        T result = IsAvailable(methodParams) ? obj.Call<T>(methodName, methodParams) : obj.Call<T>(methodName);
        obj.Dispose();
        return result;
    }
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
    public T CallStaticMethod<T>(AndroidJavaObject obj, string methodName, object[] methodParams) {
        T result = IsAvailable(methodParams) ? obj.CallStatic<T>(methodName, methodParams) : obj.CallStatic<T>(methodName);
        obj.Dispose();
        return result;
    }
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
    public void CallMethod(AndroidJavaObject obj, string methodName, object[] methodParams) {
        if (IsAvailable(methodParams)) {
            obj.Call(methodName, methodParams);
        } else {
            obj.Call(methodName);
        }
        obj.Dispose();
    }
#endif

        public bool IsAvailable(object[] param) {
            return param != null && param.Length > 0;
        }

#if UNITY_ANDROID && !UNITY_EDITOR
    public void CallRunnable(AndroidJavaObject obj, string methodName, bool methodIsStatic, System.Action action) {
        if (action != null) {
            if(methodIsStatic) {
                obj.CallStatic(methodName, new AndroidJavaRunnable(() => {
                    action();
                }));
            } else {
                obj.Call(methodName, new AndroidJavaRunnable(() => {
                    action();
                }));
            }
        }
        obj.Dispose();
    }
#endif

        public void RunOnAndroidUIThread(System.Action action) {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaObject activity = GetCurrentActivity;
        CallRunnable(activity, "runOnUiThread", false, action);
#endif
        }

#if UNITY_ANDROID && !UNITY_EDITOR
    private AndroidJavaObject CreateAndroidJavaObject(string className, object[] classParams) {
        AndroidJavaObject obj = null;
        if (IsAvailable(classParams)) {
            obj = new AndroidJavaObject(className, classParams);
        } else {
            obj = new AndroidJavaObject(className);
        }
        return obj;
    }
#endif

        public T GetField<T>(string className, string fieldName, bool isFieldStatic) {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass clazz = new AndroidJavaClass(className);
        return isFieldStatic ? clazz.GetStatic<T>(fieldName) : clazz.Get<T>(fieldName);
#else
            return default(T);
#endif
        }

#if UNITY_ANDROID && !UNITY_EDITOR
    public AndroidJavaObject GetCurrentActivity {
        get {
            AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            return androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
        }
    }
#endif
        #endregion Utils

    }
}
