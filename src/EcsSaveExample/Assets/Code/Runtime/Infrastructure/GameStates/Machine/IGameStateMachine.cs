using Code.Runtime.Infrastructure.GameStates.Api;
using Cysharp.Threading.Tasks;

namespace Code.Runtime.Infrastructure.GameStates.Machine
{
    public interface IGameStateMachine
    {
        void Tick();

        void Enter<TState>()
            where TState : class, IEnterableState;

        void Enter<TState, TPayload>(TPayload payload)
            where TState : class, IEnterablePayloadState<TPayload>;

        UniTask EnterAsync<TState>()
            where TState : class, IEnterableState;

        UniTask EnterAsync<TState, TPayload>(TPayload payload)
            where TState : class, IEnterablePayloadState<TPayload>;
    }
}