using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace Code.Tests.OlfEditorTests.Common
{
    internal sealed class MissingObjectsTests
    {
        [Test]
        public void TestNoMissingScriptableObjects()
        {
            foreach(string path in GetAllObjectsPaths())
            {
                Object loadedObject = AssetDatabase.LoadAssetAtPath<Object>(path);
                loadedObject.Should().NotBeNull(path, "Object is missing or not loaded correctly.");
            }
        }

        private static IEnumerable<string> GetAllObjectsPaths() =>
            AssetDatabase
                .FindAssets("t:Object", new[] { "Assets/Prefabs", "Assets/Resources" })
                .Select(AssetDatabase.GUIDToAssetPath);
    }
}