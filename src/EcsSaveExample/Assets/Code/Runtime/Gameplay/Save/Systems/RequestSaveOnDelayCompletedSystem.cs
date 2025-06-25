using Code.Runtime.Common.Extensions;
using Code.Runtime.Gameplay.Common.Time;
using Entitas;
using JetBrains.Annotations;

namespace Code.Runtime.Gameplay.Save.Systems
{
    [UsedImplicitly]
    public sealed class RequestSaveOnDelayCompletedSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _saveDelays;

        public RequestSaveOnDelayCompletedSystem(GameContext game, ITimeService timeService)
        {
            _game = game;

            _saveDelays = game.GetGroup(
                GameMatcher.AllOf(
                    GameMatcher.SaveTimer,
                    GameMatcher.Completed));
        }

        public void Execute()
        {
            foreach(GameEntity _ in _saveDelays)
                _game
                    .CreateEntity()
                    .With(x => x.isSaveRequest = true)
                    .With(x => x.isNonSaveEntity = true);
        }
    }
}