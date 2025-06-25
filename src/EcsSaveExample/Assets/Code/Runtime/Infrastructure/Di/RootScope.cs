using Code.Runtime.Infrastructure.Di.Api;
using VContainer;

namespace Code.Runtime.Infrastructure.Di
{
    internal sealed class RootScope : AutoCollectScope
    {
        public static bool HasInstance { get; private set; }

        protected override void OnConfigure(IContainerBuilder builder) =>
            HasInstance = true;

        protected override void OnDestroy()
        {
            base.OnDestroy();
            HasInstance = false;
        }
    }
}