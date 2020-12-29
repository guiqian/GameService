using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace GameService {
    public class Service_File : IGameService {

        public DirectoryInfo CreateDirectory(string path, bool isDeleteExists = true) {
            if (isDeleteExists) {
                DeleteDirectory(path);
                return Directory.CreateDirectory(path);
            } else {
                if (Directory.Exists(path) == false) {
                    return Directory.CreateDirectory(path);
                }
                return null;
            }
        }

        public bool DeleteDirectory(string path) {
            if (Directory.Exists(path)) {
                Directory.Delete(path, true);
                return true;
            }
            return false;
        }

        public bool CreateFile(string fileFullPath) {
            if (File.Exists(fileFullPath)) {
                File.Delete(fileFullPath);
            }
            File.Create(fileFullPath).Dispose();
            return true;
        }

        public bool DeleteFile(string fileName) {
            if (File.Exists(fileName)) {
                File.Delete(fileName);
                return true;
            }
            return false;
        }

        public FileStream OpenOrCreate(string filePath) {
            return new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
        }

        public void CloseDispose(FileStream fileStream) {
            fileStream.Close();
            fileStream.Dispose();
        }

        public string Path_GetDirectoryName(string filePath) {
            return Path.GetDirectoryName(filePath);
        }

        public string MD5(string fileName) {
            if (File.Exists(fileName)) {
                try {
                    FileStream file = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                    System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                    byte[] retVal = md5.ComputeHash(file);
                    file.Close();
                    file.Dispose();
                    file = null;
                    StringBuilder Ac = new StringBuilder();
                    for (int i = 0; i < retVal.Length; i++) {
                        Ac.Append(retVal[i].ToString("x2"));
                    }
                    return Ac.ToString();
                } catch (System.Exception e) {
                    return null;
                }
            } else {
                return null;
            }
        }

        void System.IDisposable.Dispose() {
        }

    }
}
