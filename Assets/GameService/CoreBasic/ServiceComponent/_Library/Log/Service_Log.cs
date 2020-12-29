using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameService {
    public class Service_Log : IGameService {

        public void Log2Console(string msg) {
            Debug.Log(msg);
        }

        public void Log2Console(int msg) {
            Log2Console(msg.ToString());
        }

        private void GetPath(out string basePath, out string fullPath) {
            basePath = GameServiceDefine.PersistentPath;
            fullPath = System.IO.Path.Combine(basePath, "GameServiceLog.txt");
        }

        public void Log2FileDelete() {
            string path = null, fullPath = null;
            GetPath(out path, out fullPath);
            if (System.IO.File.Exists(fullPath)) {
                System.IO.File.Delete(fullPath);
            }
        }

        public void Log2File(string msg) {
            string path = null, fullPath = null;
            GetPath(out path, out fullPath);
            if (System.IO.Directory.Exists(path) == false) {
                System.IO.Directory.CreateDirectory(path);
            }

            string date = System.DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
            string line = string.Format(System.Environment.NewLine + "[{0}]>>>{1}", date, msg);
            if (System.IO.File.Exists(fullPath)) {
                System.IO.FileStream stream = System.IO.File.OpenWrite(fullPath);
                System.Text.Encoding encoder = System.Text.Encoding.UTF8;
                byte[] bytes = encoder.GetBytes(line);
                stream.Position = stream.Length;
                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
                stream.Close();
                stream.Dispose();
            } else {
                System.IO.StreamWriter writer = System.IO.File.CreateText(fullPath);
                writer.WriteLine(line);
                writer.Flush();
                writer.Close();
                writer.Dispose();
            }
        }

        void System.IDisposable.Dispose() {
        }

    }
}