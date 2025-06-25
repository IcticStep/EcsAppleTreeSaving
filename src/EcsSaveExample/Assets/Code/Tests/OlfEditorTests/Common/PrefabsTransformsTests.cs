using FluentAssertions;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace Code.Tests.OlfEditorTests.Common
{
    internal sealed class PrefabsTransformsTests
    {
        private const string ShouldBeDefaultMessage = "should be default";

        [Test]
        public void PrefabsTransforms_ShouldHaveDefaults()
        {
            foreach(string path in EditorExtensions.GetAllPrefabsPath())
                PrefabTransforms_ShouldHaveDefaults(path);
        }

        private static void PrefabTransforms_ShouldHaveDefaults(string prefabPath)
        {
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            
            if(prefab.TryGetComponent(out RectTransform rectTransform))
            {
                rectTransform.position.Should().Be(Vector3.zero, ShouldBeDefaultMessage);
                rectTransform.rotation.Should().Be(Quaternion.identity, ShouldBeDefaultMessage);
            }
            else
            {
                prefab.transform.position.Should().Be(Vector3.zero, ShouldBeDefaultMessage);
                prefab.transform.rotation.Should().Be(Quaternion.identity, ShouldBeDefaultMessage);
                prefab.transform.localScale.Should().Be(Vector3.one, ShouldBeDefaultMessage);
            }
        }
    }
}