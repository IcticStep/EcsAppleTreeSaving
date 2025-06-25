using System.Collections.Generic;
using Code.Runtime.Infrastructure.GameStates.Machine;
using Code.Runtime.Infrastructure.GameStates.States;
using Entitas;
using JetBrains.Annotations;

namespace Code.Runtime.Gameplay.Save.Systems
{
    [UsedImplicitly]
    public sealed class LoadOnRequestSystem : IExecuteSystem
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IGroup<GameEntity> _requests;
        private readonly List<GameEntity> _buffer = new(2);

        public LoadOnRequestSystem(GameContext game, IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _requests = game.GetGroup(
                GameMatcher
                    .AllOf(GameMatcher.LoadRequest));
        }

        public void Execute()
        {
            foreach(GameEntity _ in _requests.GetEntities(_buffer))
                _gameStateMachine.Enter<ReloadLevelState>();
        }
    }
}