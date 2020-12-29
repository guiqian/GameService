using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace GameService {

    public class GameServiceEditor4GenerateCode {

        protected readonly static string YourClassName = "YourClassName";

        [MenuItem("Tools/Game Service/Generate Code/Generate GameObject Path")]
        private static void GenerateCode4GameObjectPath_GenerateCode() {
            GameServiceEditor4GenerateCode obj = new GameServiceEditor4GenerateCode();
            obj.GenerateCode4GameObjectPath_GenerateCode(YourClassName);
        }

        protected void GenerateCode4GameObjectPath_GenerateCode(string yourClassName) {
            GameObject[] selectdObj = Selection.gameObjects;
            if (selectdObj != null) {
                string outputDirPath = Path.Combine(Application.dataPath, "GenerateCode");
                if (Directory.Exists(outputDirPath) == false) {
                    Directory.CreateDirectory(outputDirPath);
                }
                string outputFileName = string.Format("GenerateCode_{0}.cs", yourClassName);
                string outputFullPath = Path.Combine(outputDirPath, outputFileName);
                if (File.Exists(outputFullPath)) {
                    File.Delete(outputFullPath);
                }

                List<string> listFieldValues = new List<string>();
                List<string> listFieldNames = new List<string>();

                List<Transform> collectTrans = new List<Transform>();
                for (int i = 0, count = selectdObj.Length; i < count; i++) {
                    collectTrans.Add(selectdObj[i].transform);
                }
                for (int i = 0, count = collectTrans.Count; i < count; i++) {
                    List<Transform> collectRootTrans = new List<Transform>();
                    CollectTopRoot(collectTrans[i], ref collectRootTrans);

                    StringBuilder sbFieldValues = new StringBuilder();
                    StringBuilder sbFieldNames = new StringBuilder();
                    for (int j = collectRootTrans.Count - 1; j >= 0; j--) {
                        Transform one = collectRootTrans[j];
                        if (j > 0) {
                            sbFieldValues.Append(one.name + "/");
                            sbFieldNames.Append(one.name + "_");
                        } else {
                            sbFieldValues.Append(one.name);
                            sbFieldNames.Append(one.name);
                        }
                    }
                    string valueName = sbFieldNames.ToString();
                    valueName = valueName.Replace(" ", string.Empty);
                    if (IsNumberStart(valueName)) {
                        valueName = "_" + valueName;
                    }
                    string valueString = sbFieldValues.ToString();
                    string[] stringResult = OnFixNameAndValue(valueName, valueString);
                    valueName = stringResult[0]; valueString = stringResult[1];
                    // =====================Save=====================
                    if (listFieldNames.Contains(valueName)) {
                        listFieldNames.Add(valueName + "_" + i);
                    } else {
                        listFieldNames.Add(valueName);
                    }
                    listFieldValues.Add(valueString);
                }

                GenerateCode4GameObjectPath.GenerateCode(outputFullPath, listFieldNames.ToArray(), listFieldValues.ToArray());
                AssetDatabase.Refresh();
            }
        }

        protected virtual string[] OnFixNameAndValue(string name, string value) {
            return new string[2] { name, value };
        }

        private bool IsNumberStart(string str) {
            return char.IsDigit(str[0]);
        }

        private void CollectTopRoot(Transform transform, ref List<Transform> list) {
            list.Add(transform);
            Transform parent = transform.parent;
            while (parent != null) {
                list.Add(parent);
                parent = parent.parent;
            }
        }

    }
}