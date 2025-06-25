using Code.Runtime.Gameplay.InitializeLevel.Systems;
using Code.Runtime.Infrastructure.Systems;
using JetBrains.Annotations;

namespace Code.Runtime.Gameplay.InitializeLevel
{
    [UsedImplicitly]
    public sealed class InitializeLevelFeature : Feature
    {
        public InitializeLevelFeature(ISystemFactory systems)
        {
            Add(systems.Create<InitializeAppleSpawnTimerSystem>());
        }
    }
}