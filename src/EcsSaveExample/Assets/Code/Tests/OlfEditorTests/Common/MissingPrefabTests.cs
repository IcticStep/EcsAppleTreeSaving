using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Tests.OlfEditorTests.Common
{
  public class PrefabTests
  {
    [Test]
    public void CheckMissingPrefabInScenes()
    {
      foreach(string scenePath in EditorExtensions.GetActiveBuildSScenesPath())
        CheckMissingPrefabInScene(scenePath);
    }
    
    private static void CheckMissingPrefabInScene(string scenePath)
    {
      //Arrange
      Scene openedScene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive);

      //Act
      List<string> missingGameObjects = openedScene.GetComponents<Transform>()
        .Where(x=>x.name.Contains("Missing Prefab"))
        .Select(x => x.name).ToList();

      EditorSceneManager.CloseScene(openedScene, true);

      //Assert
      missingGameObjects.Should().BeEmpty(scenePath, "Missing prefabs in scene");
    }
    
    [Test]
    public void CheckMissingPrefabInPrefabs()
    {
      //Arrange
      List<string> warningPrefab = new();

      //Act
      string[] prefabPaths = AssetDatabase.GetAllAssetPaths();
      foreach (string path in prefabPaths)
      {
        if (path.Contains(".prefab"))
        {
          var prefab = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)) as GameObject;
          if(prefab == null)
            warningPrefab.Add($"Missing prefab {path}\n");
          else
            foreach(Transform child in prefab.transform)
              if(child.gameObject.name.Contains("Missing Prefab"))
                warningPrefab.Add($"Missing prefab in child of prefab at {path}\n");
        }
      }
      
      //Assert
      warningPrefab.Should().BeEmpty();
    }
  }
}