using Code.Runtime.Gameplay.Apples.Configs;
using Code.Runtime.Gameplay.Common.Time;
using Code.Runtime.Infrastructure.StaticData.Service;
using Entitas;
using JetBrains.Annotations;

namespace Code.Runtime.Gameplay.Apples.Systems
{
    [UsedImplicitly]
    public sealed class TickAppleSpawnTimerSystem : IExecuteSystem
    {
        private readonly ITimeService _timeService;
        private readonly IStaticDataService _staticDataService;
        private readonly IGroup<GameEntity> _timers;

        private AppleConfig AppleConfig => _staticDataService.AppleConfig;

        public TickAppleSpawnTimerSystem(GameContext game, ITimeService timeService, IStaticDataService staticDataService)
        {
            _timeService = timeService;
            _staticDataService = staticDataService;
            _timers = game.GetGroup(
                GameMatcher
                    .AllOf(GameMatcher.AppleSpawnTimer));
        }

        public void Execute()
        {
            foreach(GameEntity timer in _timers)
            {
                timer.ReplaceAppleSpawnTimer(timer.AppleSpawnTimer - _timeService.DeltaTime);
                if(timer.AppleSpawnTimer <= 0)
                {
                    timer.isCompleted = true;
                    timer.ReplaceAppleSpawnTimer(AppleConfig.SpawnInterval);
                }
            }
        }
    }
}