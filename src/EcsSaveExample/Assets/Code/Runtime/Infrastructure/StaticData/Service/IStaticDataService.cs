using Code.Runtime.Gameplay;
using Code.Runtime.Gameplay.Apples.Configs;
using Code.Runtime.Infrastructure.SceneLoading;
using Code.Runtime.Infrastructure.SceneLoading.Configs;
using Code.Runtime.Infrastructure.View;
using Code.Runtime.Infrastructure.Windows;
using Code.Runtime.Infrastructure.Windows.Api;
using Code.Runtime.UI;
using Eflatun.SceneReference;

namespace Code.Runtime.Infrastructure.StaticData.Service
{
    public interface IStaticDataService
    {
        void LoadAll();
        SceneReference GetSceneReference(SceneTypeId sceneTypeId);
        BaseWindow GetWindow(WindowTypeId windowTypeId);
        ScenesConfig LoadScenesConfig();
        UiConfig UiConfig { get; }
        AppleConfig AppleConfig { get; }
        EntityBehaviour GetViewPrefab(EntityTypeId entityType);
    }
}