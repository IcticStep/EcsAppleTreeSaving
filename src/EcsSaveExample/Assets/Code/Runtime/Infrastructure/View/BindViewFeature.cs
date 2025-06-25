using Code.Runtime.Infrastructure.Systems;
using Code.Runtime.Infrastructure.View.Systems;

namespace Code.Runtime.Infrastructure.View
{
    internal sealed class BindViewFeature : Feature
    {
        public BindViewFeature(ISystemFactory systems)
        {
            Add(systems.Create<BindEntityViewSystem>());
        }
    }
}