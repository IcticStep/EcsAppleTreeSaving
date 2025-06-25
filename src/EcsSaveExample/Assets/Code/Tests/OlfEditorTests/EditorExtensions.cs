using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Tests.OlfEditorTests
{
  public static class EditorExtensions
  {
    private static readonly string[] ShadesExtensions = { ".shader", ".shadergraph", ".raytrace", ".compute" }; 
    
    public static string GetScenePath(this string name) =>
      AssetDatabase
        .FindAssets($"{name} t:scene", new[] {"Assets"})
        .Select(AssetDatabase.GUIDToAssetPath)
        .First();

    public static List<T> GetComponents<T>(this Scene openedScene)
    {
      var stageDialogControllers = new List<T>();
      foreach (GameObject rootGameObject in openedScene.GetRootGameObjects())
        stageDialogControllers.AddRange(rootGameObject.GetComponentsInChildren<T>(true));
      return stageDialogControllers;
    }
    
    public static IEnumerable<string> GetAllScenesPath() =>
      AssetDatabase
        .FindAssets("t:Scene", new[] { "Assets" })
        .Select(AssetDatabase.GUIDToAssetPath);

    public static IEnumerable<string> GetAllImagesPaths() =>
      AssetDatabase
        .FindAssets("t:Texture", new[] { "Assets/Graphics" })
        .Select(AssetDatabase.GUIDToAssetPath);
    
    public static IEnumerable<string> GetAllTexturesPaths() =>
      AssetDatabase
        .FindAssets("t:Texture2D", new[] { "Assets/Graphics" })
        .Select(AssetDatabase.GUIDToAssetPath)
        .Where(path => !path.Contains("Assets/Graphics/UI"));
    
    public static IEnumerable<string> GetAllMaterialsPaths() =>
      AssetDatabase
        .FindAssets("t:Material", new[] { "Assets/Graphics" })
        .Select(AssetDatabase.GUIDToAssetPath);

    public static IEnumerable<string> GetAllShadersPaths() =>
      AssetDatabase
        .FindAssets("", new[] { "Assets/Graphics" })
        .Select(AssetDatabase.GUIDToAssetPath)
        .Where(path => ShadesExtensions.Any(x => path.ToLower().EndsWith(x)));

    public static IEnumerable<string> GetAllMeshesPath()
    {
      var meshes = AssetDatabase.FindAssets("t:Mesh", new[] { "Assets/Graphics" });
      var meshFilters = AssetDatabase.FindAssets("t:MeshFilter", new[] { "Assets/Graphics" });
      return meshes.Concat(meshFilters).Select(AssetDatabase.GUIDToAssetPath);
    }
    
    public static IEnumerable<string> GetAllShadersPath() =>
      AssetDatabase
        .FindAssets("t:Shader", new[] { "Assets/Graphics" })
        .Select(AssetDatabase.GUIDToAssetPath);
    
    public static IEnumerable<string> GetAllPrefabsPath() =>
      AssetDatabase
        .FindAssets("t:Prefab", new[] { "Assets/Prefabs" })
        .Select(AssetDatabase.GUIDToAssetPath);
    
    public static IEnumerable<string> GetAllProjectScriptableObjectsPath() =>
      AssetDatabase
        .FindAssets("t:ScriptableObject", new[] { "Assets/Resources" })
        .Select(AssetDatabase.GUIDToAssetPath);
    
    public static IEnumerable<string> GetAllScriptsPath() =>
      AssetDatabase
        .FindAssets("t:Script", new[] { "Assets/Code" })
        .Select(AssetDatabase.GUIDToAssetPath);
    
    public static IEnumerable<string> GetAllModelsPaths() =>
      AssetDatabase
        .FindAssets("t:Model", new[] { "Assets/Graphics" })
        .Select(AssetDatabase.GUIDToAssetPath);
    
    public static IEnumerable<string> GetActiveBuildSScenesPath() => 
      EditorBuildSettings.scenes
        .Where(scene => scene.enabled)
        .Select(scene => scene.path);
    
    public static string GetHierarchyPath(this GameObject gameObject)
    {
      var path = new System.Text.StringBuilder(gameObject.name);
      var parent = gameObject.transform.parent;
			
      while (parent.IsAlive())
      {
        path.Insert(0, parent.name + @"/");
        parent = parent.parent;
      }
			
      return path.ToString();
    }
    
    public static bool IsAlive(this Object @object) => @object;

    public static bool HasMissingScripts(this GameObject gameObject) =>
      GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(gameObject) > 0;

    public static IEnumerable<GameObject> AllGameObjects(this Scene scene)
    {
      Queue<GameObject> gameObjectsQueue = new(scene.GetRootGameObjects());
      while(gameObjectsQueue.Count > 0)
      {
        GameObject gameObject = gameObjectsQueue.Dequeue();

        yield return gameObject;

        foreach(Transform child in gameObject.transform)
          gameObjectsQueue.Enqueue(child.gameObject);
      }
    }
    
    public static IEnumerable<GameObject> GetChildren(this GameObject gameObject, bool includeSelf = false)
    {
      if(includeSelf)
        yield return gameObject;

      IEnumerable<GameObject> children = gameObject
        .transform
        .Cast<Transform>()
        .Select(childTransform => childTransform.gameObject);
      
      foreach(GameObject child in children)
        yield return child;  
    }
  }
}