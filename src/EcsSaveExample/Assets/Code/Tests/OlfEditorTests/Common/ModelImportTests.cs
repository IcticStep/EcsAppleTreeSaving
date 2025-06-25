using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace Code.Tests.OlfEditorTests.Common
{
    internal sealed class ModelImportTests
    {
        [Test]
        public void AllModelsImport_ShouldNotHaveWarningsOrErrors()
        {
            foreach(string path in EditorExtensions.GetAllModelsPaths())
                ModelImport_ShouldNotHaveWarningsOrErrors(path);
        }

        [Test]
        public void Models_ShouldBeOrientedCorrectly()
        {
            foreach(string path in EditorExtensions.GetAllModelsPaths())
                Model_ShouldBeOrientedCorrectly(path);
        }

        private static void Model_ShouldBeOrientedCorrectly(string path)
        {
            GameObject model = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            List<Transform> meshesTransforms = GetTransformsOfMeshes(model).ToList();
            ModelImporter importer = (ModelImporter)AssetImporter.GetAtPath(path);
            
            // Only check if the model has meshes and no clips
            if (!(meshesTransforms.Any() && importer.clipAnimations.Length == 0))
                return;

            foreach(Transform meshesTransform in meshesTransforms)
                meshesTransform.localRotation.ToString("F6")
                    .Should().Be(Quaternion.identity.ToString("F6"));
        }

        private static void ModelImport_ShouldNotHaveWarningsOrErrors(string path)
        {
            ImportLog importLog = AssetImporter.GetImportLog(path);

            IEnumerable<string> warnings = GetLogs(importLog, ImportLogFlags.Warning);
            IEnumerable<string> errors = GetLogs(importLog, ImportLogFlags.Error);

            warnings.Should().BeEmpty();
            errors.Should().BeEmpty();
        }

        private static List<string> GetLogs(ImportLog importLog, ImportLogFlags logType) =>
            importLog?
                .logEntries
                .Where(x => x.flags.HasFlag(logType))
                .Select(x => x.message)
                .ToList() ?? new();

        private static IEnumerable<Transform> GetTransformsOfMeshes(GameObject model)
        {
            var meshFilteredMeshes = model
                .GetComponentsInChildren<MeshFilter>()
                .Select(m => m.transform);

            var skinnedRenderedMeshes = model
                .GetComponentsInChildren<SkinnedMeshRenderer>()
                .Select(m => m.transform);

            return meshFilteredMeshes.Concat(skinnedRenderedMeshes);
        }
    }
}