using Entitas;

namespace Code.Runtime.Infrastructure.Systems
{
    public interface ISystemFactory
    {
        T Create<T>()
            where T : ISystem;

        T Create<T>(params object[] args)
            where T : ISystem;
    }
}