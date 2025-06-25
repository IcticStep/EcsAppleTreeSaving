using System.Collections.Generic;
using Code.Runtime.Gameplay;
using Code.Runtime.Gameplay.Apples.Configs;
using Code.Runtime.Infrastructure.SceneLoading;
using Code.Runtime.Infrastructure.SceneLoading.Configs;
using Code.Runtime.Infrastructure.View;
using Code.Runtime.Infrastructure.View.Configs;
using Code.Runtime.Infrastructure.Windows;
using Code.Runtime.Infrastructure.Windows.Api;
using Code.Runtime.Infrastructure.Windows.Configs;
using Code.Runtime.UI;
using Eflatun.SceneReference;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Runtime.Infrastructure.StaticData.Service
{
    [UsedImplicitly]
    internal sealed class StaticDataService : IStaticDataService
    {
        private Dictionary<WindowTypeId, BaseWindow> _windows;
        private ScenesConfig _scenesConfig;
        private ViewConfig _viewConfig;

        public UiConfig UiConfig { get; private set; }
        public AppleConfig AppleConfig { get; private set; }

        public void LoadAll()
        {
            LoadScenesConfig();
            LoadUiConfig();
            LoadWindowsConfig();
            LoadViewsConfig();
            LoadApplesConfig();
        }

        public EntityBehaviour GetViewPrefab(EntityTypeId entityType) =>
            _viewConfig.Views[entityType];

        public SceneReference GetSceneReference(SceneTypeId sceneTypeId) =>
            _scenesConfig.Scenes[sceneTypeId];

        public BaseWindow GetWindow(WindowTypeId windowTypeId) =>
            _windows[windowTypeId];

        public ScenesConfig LoadScenesConfig() =>
            _scenesConfig = Resources.Load<ScenesConfig>("Configs/ScenesConfig");

        private void LoadUiConfig() =>
            UiConfig = Resources.Load<UiConfig>("Configs/UI/UiConfig");

        private void LoadWindowsConfig() =>
            _windows = Resources
                .Load<WindowsConfig>("Configs/UI/WindowsConfig")
                .Windows;

        private void LoadViewsConfig() =>
            _viewConfig = Resources.Load<ViewConfig>("Configs/ViewConfig");

        private void LoadApplesConfig() =>
            AppleConfig = Resources.Load<AppleConfig>("Configs/AppleConfig");
    }
}