using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Code.Editor.Toolbar
{
    internal sealed class FileMenu
    {
        [MenuItem("File/Save Scene And Project %#&s")]
        private static void ForceSaveSceneAndProject()
        {
            EditorApplication.ExecuteMenuItem("File/Save");
            EditorApplication.ExecuteMenuItem("File/Save Project");
            Debug.Log("Saved scene and project");
        }

        [MenuItem("File/Domain Reload %#&d")]
        public static void ReloadDomain()
        {
            AssetDatabase.SaveAssets();
            EditorUtility.RequestScriptReload();
            Debug.Log("Domain reloaded.");
        }

        [MenuItem("File/Regenerate Project Files %#&r")]
        public static void RegenerateProjectFiles()
        {
            Type syncVcType = Type.GetType("UnityEditor.SyncVS,UnityEditor");
            MethodInfo syncSolution = syncVcType!.GetMethod("SyncSolution", BindingFlags.Public | BindingFlags.Static);
            syncSolution!.Invoke(null, null);
            Debug.Log("Project Files Regenerated.");
        }
    }
}