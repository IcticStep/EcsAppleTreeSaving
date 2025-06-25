using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Scene = UnityEngine.SceneManagement.Scene;

namespace Code.Tests.OlfEditorTests.Common
{
  [TestFixture]
  public class MissingComponentsTests
  {
    [Test]
    public void AllSceneGameObjectsShouldNotHaveMissingScripts()
    {
      foreach(string path in EditorExtensions.GetAllScenesPath())
        SceneGameObjectsShouldNotHaveMissingScripts(path);
    }

    [Test]
    public void AllPrefabsShouldNotHaveMissingScripts()
    {
      foreach(string prefabPath in EditorExtensions.GetAllPrefabsPath())
        PrefabShouldNotHaveMissingScripts(prefabPath);
    }

    private void PrefabShouldNotHaveMissingScripts(string prefabPath)
    {
      if(prefabPath.Contains("Plugins"))
        Assert.Ignore();
      // Act
      List<string> gameObjectsWithMissingScripts = AssetDatabase
        .LoadAssetAtPath<GameObject>(prefabPath)
        .GetChildren(includeSelf: true)
        .Where(x => x.HasMissingScripts())
        .GroupBy(gameObject => gameObject.name)
        .Select(grouping => $"{grouping.Key} ({grouping.Count()})")
        .ToList();

      // Assert
      gameObjectsWithMissingScripts.Should().BeEmpty(prefabPath, "Prefab has missing scripts");
    }

    private void SceneGameObjectsShouldNotHaveMissingScripts(string scenePath)
    {
      // Act
      Scene scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive);

      List<string> gameObjectsWithMissingScripts = scene
        .AllGameObjects()
        .Where(x => x.HasMissingScripts())
        .GroupBy(gameObject => gameObject.name)
        .Select(grouping => $"{grouping.Key} ({grouping.Count()})")
        .ToList();

      EditorSceneManager.CloseScene(scene, removeScene: true);

      // Assert
      gameObjectsWithMissingScripts.Should().BeEmpty(scenePath, "Scene has missing scripts");
    }
  }
}