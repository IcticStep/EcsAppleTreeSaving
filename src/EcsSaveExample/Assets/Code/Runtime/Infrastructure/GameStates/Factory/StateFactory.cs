using Code.Runtime.Infrastructure.GameStates.Api;
using JetBrains.Annotations;
using VContainer;

namespace Code.Runtime.Infrastructure.GameStates.Factory
{
    [UsedImplicitly]
    internal sealed class StateFactory : IStateFactory
    {
        private readonly IObjectResolver _resolver;

        public StateFactory(IObjectResolver resolver)
        {
            _resolver = resolver;
        }

        public T GetState<T>()
            where T : class, IState =>
            _resolver.Resolve<T>();
    }
}