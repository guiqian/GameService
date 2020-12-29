using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameService {
    public class Service_String : IGameService {

        public T[] SplitString<T>(string src, char flag) {
            if (src.Contains(flag.ToString())) {
                string[] array = src.Split(flag);
                if (typeof(T) == typeof(int)) {
                    int[] result = System.Array.ConvertAll<string, int>(array, value => System.Convert.ToInt32(value));
                    return result as T[];
                } else if (typeof(T) == typeof(string)) {
                    return array as T[];
                } else if (typeof(T) == typeof(float)) {
                    float[] result = System.Array.ConvertAll<string, float>(array, value => System.Convert.ToSingle(value));
                    return result as T[];
                }
            }
            return null;
        }

        public T[] FromJsonByArray<T>(string json) {
            string newJson = "{ \"array\": " + json + "}";
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
            return wrapper.array;
        }

        [System.Serializable]
        private class Wrapper<T> {
            public T[] array;
        }

        void System.IDisposable.Dispose() {
        }

    }
}
