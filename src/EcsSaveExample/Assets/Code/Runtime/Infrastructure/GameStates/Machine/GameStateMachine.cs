using System;
using System.Linq;
using Code.Runtime.Common.Extensions;
using Code.Runtime.Infrastructure.GameStates.Api;
using Code.Runtime.Infrastructure.GameStates.Factory;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using VContainer.Unity;
using IState = Code.Runtime.Infrastructure.GameStates.Api.IState;

namespace Code.Runtime.Infrastructure.GameStates.Machine
{
    [UsedImplicitly]
    internal sealed class GameStateMachine : IGameStateMachine, ITickable
    {
        private readonly IStateFactory _stateFactory;
        
        private IState _activeState;

        public GameStateMachine(IStateFactory stateFactory)
        {
            _stateFactory = stateFactory;
        }

        public void Tick()
        {
            if(_activeState is IUpdateableState updateableState)
                updateableState.Update();
        }

        public void Enter<TState>()
            where TState : class, IEnterableState =>
            EnterAsync<TState>().Forget();
        
        public void Enter<TState, TPayload>(TPayload payload)
            where TState : class, IEnterablePayloadState<TPayload> =>
            EnterAsync<TState, TPayload>(payload).Forget();

        public async UniTask EnterAsync<TState>()
            where TState : class, IEnterableState
        {
            await ExitActiveState();
            TState state = GetState<TState>();
            _activeState = state;
            EnterState(state);
        }

        public async UniTask EnterAsync<TState, TPayload>(TPayload payload)
            where TState : class, IEnterablePayloadState<TPayload>
        {
            await ExitActiveState();
            TState state = GetState<TState>();
            _activeState = state;
            EnterState(state, payload);
        }

        private async UniTask ExitActiveState()
        {
            if(_activeState is null)
                return;

            if(_activeState is IExitableState exitableState)
                exitableState.Exit();

            if(_activeState is IEndOfFrameExitableState endOfFrameExitableState)
            {
                await UniTask.WaitForEndOfFrame();
                endOfFrameExitableState.ExitOnEndOfFrame();
            }
        }

        private void EnterState<TState>(TState state)
            where TState : class, IEnterableState
        {
            LogStateEntered(state);
            state.Enter();
        }

        private void EnterState<TState, TPayload>(TState state, TPayload payload)
            where TState : class, IEnterablePayloadState<TPayload>
        {
            LogStateEntered(state);
            state.Enter(payload);
        }

        private TState GetState<TState>()
            where TState : class, IState =>
            _stateFactory.GetState<TState>();

        [HideInCallstack]
        private static void LogStateEntered<TState>(TState state)
            where TState : class, IState
        {
            Type type = state.GetType();
            Debug.Log($"{nameof(GameStateMachine)}: <color=#00ffff>{type.GetBeautifulName()}</color> state entered.");
        }
    }
}