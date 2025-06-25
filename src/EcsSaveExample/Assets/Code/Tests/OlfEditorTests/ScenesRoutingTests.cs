using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Code.Runtime.Common.Extensions;
using Code.Runtime.Infrastructure.SceneLoading;
using Code.Runtime.Infrastructure.SceneLoading.Configs;
using Code.Runtime.Infrastructure.StaticData.Service;
using Eflatun.SceneReference;
using FluentAssertions;
using NUnit.Framework;

namespace Code.Tests.OlfEditorTests
{
    [TestFixture]
    public class ScenesRoutingTests
    {
        private IStaticDataService _staticDataService;
        private ScenesConfig _scenesConfig;

        [SetUp]
        public void SetUp()
        {
            _staticDataService = new StaticDataService();
            _staticDataService.LoadScenesConfig();
            _scenesConfig = typeof(StaticDataService)
                .GetField("_scenesConfig", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.GetValue(_staticDataService) as ScenesConfig;
        }
        
        [Test]
        public void ScenesConfig_ShouldNotBeNull() =>
            _scenesConfig.Should().NotBeNull();

        [Test]
        public void ScenesConfig_ShouldContainAllSceneTypes()
        {
            List<SceneTypeId> allSceneTypes = GetAllSceneTypes();
            IEnumerable<SceneTypeId> presentTypes = _scenesConfig.Scenes.Keys;
            List<SceneTypeId> missingScenesTypes = allSceneTypes.Except(presentTypes).ToList();
            
            missingScenesTypes.Should().BeEmpty();
        }
        
        [Test]
        public void SceneConfig_ReferencesAreValid()
        {
            var brokenSceneTypes = _scenesConfig
                .Scenes
                .Where(x => x.Value == null || x.Value.State == SceneReferenceState.Unsafe)
                .Select(x => new
                {
                    Scene = x.Key.ToString(),
                    Problem = x.Value?.UnsafeReason.ToString() ?? "reference is null",
                });
            
            brokenSceneTypes.Should().BeEmpty();
        }

        [Test]
        public void SceneConfig_TypesAreUnique()
        {
            IEnumerable<SceneTypeId> duplicateSceneTypes = _scenesConfig
                .Scenes
                .GroupBy(x => x.Key)
                .Where(x => x.Count() > 1)
                .Select(x => x.Key);
            
            duplicateSceneTypes.Should().BeEmpty();
        }

        private static List<SceneTypeId> GetAllSceneTypes() =>
            typeof(SceneTypeId)
                .GetEnumValues()
                .Cast<SceneTypeId>()
                .Except(SceneTypeId.Unknown)
                .ToList();
    }
}