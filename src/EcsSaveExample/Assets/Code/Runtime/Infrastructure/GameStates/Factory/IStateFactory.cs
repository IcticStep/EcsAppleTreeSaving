using Code.Runtime.Infrastructure.GameStates.Api;

namespace Code.Runtime.Infrastructure.GameStates.Factory
{
    internal interface IStateFactory
    {
        T GetState<T>()
            where T : class, IState;
    }
}