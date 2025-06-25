using Code.Runtime.Gameplay.Apples.Systems;
using Code.Runtime.Infrastructure.Systems;
using JetBrains.Annotations;

namespace Code.Runtime.Gameplay.Apples
{
    [UsedImplicitly]
    public sealed class ApplesFeature : Feature
    {
        public ApplesFeature(ISystemFactory systems)
        {
            Add(systems.Create<TickAppleSpawnTimerSystem>());
            
            Add(systems.Create<SpawnAppleOnTreeByTimerSystem>());
            Add(systems.Create<GrowApplesSystem>());
            Add(systems.Create<ThrowTouchedApplesSystem>());
            Add(systems.Create<UpdateApplesViewSystem>());
            
            Add(systems.Create<DestroyFallenApplesSystem>());
            
            Add(systems.Create<CleanAppleSpawnTimerCompletionSystem>());
        }
    }
}