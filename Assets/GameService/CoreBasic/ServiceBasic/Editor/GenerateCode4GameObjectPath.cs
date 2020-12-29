using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Reflection;
using System.IO;

namespace GameService {

    public class GenerateCode4GameObjectPath {

        public static void GenerateCode(string outputFullPath, string[] fieldNames, string[] fieldNameValues) {
            CodeCompileUnit unit = new CodeCompileUnit(); // 准备一个代码编译器单元 

            CodeNamespace sampleNamespace = new CodeNamespace("GameService"); //准备必要的命名空间（这个是指要生成的类的空间）
            sampleNamespace.Imports.Add(new CodeNamespaceImport("System")); //导入必要的命名空间

            string clazzName = Path.GetFileName(outputFullPath).Replace(".cs", string.Empty);
            CodeTypeDeclaration Customerclass = new CodeTypeDeclaration(clazzName);//准备要生成的类的定义
            Customerclass.IsClass = true;
            Customerclass.TypeAttributes = TypeAttributes.Public | TypeAttributes.Sealed;
            sampleNamespace.Types.Add(Customerclass);//把这个类放在这个命名空间下 
            unit.Namespaces.Add(sampleNamespace);//把该命名空间加入到编译器单元的命名空间集合中 

            string outputFile = Path.GetFileName(outputFullPath) + Path.GetExtension(outputFullPath);
            if (File.Exists(outputFullPath)) {
                File.Delete(outputFullPath);
            }
            File.Create(outputFullPath).Dispose();

            // 添加字段
            for (int i = 0, count = fieldNames.Length; i < count; i++) {
                CodeMemberField field = new CodeMemberField(typeof(System.String), fieldNames[i]);
                field.Attributes = MemberAttributes.Public | MemberAttributes.Static | MemberAttributes.Final;
                field.InitExpression = new CodePrimitiveExpression(fieldNameValues[i]);
                Customerclass.Members.Add(field);
            }

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            options.BracingStyle = "C";
            options.BlankLinesBetweenMembers = true;
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(outputFullPath)) {
                provider.GenerateCodeFromCompileUnit(unit, sw, options);
                sw.Flush();
                sw.Close();
                sw.Dispose();
            }

        }

    }

}