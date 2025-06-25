using Code.Runtime.Common.Touch.Systems;
using Code.Runtime.Infrastructure.Systems;
using JetBrains.Annotations;

namespace Code.Runtime.Common.Touch
{
    [UsedImplicitly]
    public sealed class TouchFeature : Feature
    {
        public TouchFeature(ISystemFactory systems)
        {
            Add(systems.Create<CleanUpTouchSystem>());
        }
    }
}