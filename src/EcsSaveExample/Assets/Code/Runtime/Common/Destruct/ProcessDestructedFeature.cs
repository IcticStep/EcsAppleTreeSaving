using Code.Runtime.Common.Destruct.Systems;
using Code.Runtime.Infrastructure.Systems;

namespace Code.Runtime.Common.Destruct
{
    internal sealed class ProcessDestructedFeature : Feature
    {
        public ProcessDestructedFeature(ISystemFactory systems)
        {
            Add(systems.Create<SelfDestructTimerSystem>());

            Add(systems.Create<CleanupGameDestructedViewSystem>());
            Add(systems.Create<CleanupGameDestructedSystem>());
        }
    }
}