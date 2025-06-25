using Code.Runtime.Infrastructure.Di.Api;
using Entitas;
using VContainer;

namespace Code.Runtime.Infrastructure.Systems
{
    internal sealed class SystemFactory : ISystemFactory
    {
        private readonly IObjectResolver _resolver;

        public SystemFactory(IObjectResolver resolver) =>
            _resolver = resolver;

        public T Create<T>()
            where T : ISystem =>
            _resolver.Instantiate<T>(Lifetime.Transient);

        public T Create<T>(params object[] args)
            where T : ISystem =>
            _resolver.Instantiate<T>(Lifetime.Transient, args);
    }
}