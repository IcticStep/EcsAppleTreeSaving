using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;

namespace Code.Tests.OlfEditorTests.Common
{
    internal sealed class ShadersTests
    {
        [Test]
        public void Shaders_ShouldHaveNoErrors()
        {
            foreach(string path in EditorExtensions.GetAllShadersPaths())
                ShaderShouldHaveNoErrors(path);
        }

        private void ShaderShouldHaveNoErrors(string path)
        {
            Object shaderObject = AssetDatabase.LoadAssetAtPath<Object>(path);

            switch(shaderObject)
            {
                case Shader shader:
                    AssertShaderHasNoErrors(path, shader);

                    break;
                case ComputeShader shader:
                    AsserComputeShaderHasNoErrors(path, shader);
                    
                    break;
                case RayTracingShader shader:
                    AssertRayTracingShaderHasNoErrors(path, shader);
                    break;
            }
        }

        private static void AssertShaderHasNoErrors(string path, Shader shader)
        {
            IEnumerable<string> errorMessages = ShaderUtil
                .GetShaderMessages(shader)
                .Where(x => x.severity == ShaderCompilerMessageSeverity.Error)
                .Select(x => $"{x.message} at {x.file}:{x.line}");

            errorMessages.Should().BeEmpty(path);
        }

        private static void AsserComputeShaderHasNoErrors(string path, ComputeShader shader)
        {
            IEnumerable<string> errorMessages = ShaderUtil
                .GetComputeShaderMessages(shader)
                .Where(x => x.severity == ShaderCompilerMessageSeverity.Error)
                .Select(x => $"{x.message} at {x.file}:{x.line}");
            
            errorMessages.Should().BeEmpty(path);
        }

        private void AssertRayTracingShaderHasNoErrors(string path, RayTracingShader shader)
        {
            IEnumerable<string> errorMessages = ShaderUtil
                .GetRayTracingShaderMessages(shader)
                .Where(x => x.severity == ShaderCompilerMessageSeverity.Error)
                .Select(x => $"{x.message} at {x.file}:{x.line}");
            
            errorMessages.Should().BeEmpty(path);
        }
    }
}