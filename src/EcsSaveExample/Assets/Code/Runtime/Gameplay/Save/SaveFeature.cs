using Code.Runtime.Gameplay.Save.Systems;
using Code.Runtime.Infrastructure.Systems;
using JetBrains.Annotations;

namespace Code.Runtime.Gameplay.Save
{
    [UsedImplicitly]
    public sealed class SaveFeature : Feature
    {
        public SaveFeature(ISystemFactory systems)
        {
            Add(systems.Create<InitializeSaveTimerSystem>());
            Add(systems.Create<TickSaveTimerSystem>());
            
            // Add(systems.Create<RequestSaveOnDelayCompletedSystem>());
            Add(systems.Create<SaveOnRequestSystem>());
            Add(systems.Create<LoadOnRequestSystem>());
            
            Add(systems.Create<CleanUpSaveRequestSystem>());
            Add(systems.Create<CleanUpLoadRequestSystem>());
            Add(systems.Create<CleanUpCompletionSaveTimerSystem>());
        }
    }
}