using Code.Runtime.Infrastructure.GameStates.Machine;
using Code.Runtime.Infrastructure.GameStates.States;
using VContainer.Unity;

namespace Code.Runtime.Infrastructure
{
    internal sealed class EntryPoint : IInitializable
    {
        private readonly IGameStateMachine _stateMachine;

        public EntryPoint(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Initialize() =>
            _stateMachine.Enter<BootstrapState>();
    }
}