using Code.Runtime.Gameplay.Common.Time;
using Entitas;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Runtime.Gameplay.Gravity.Systems
{
    [UsedImplicitly]
    public sealed class GravitySystem : IExecuteSystem
    {
        private readonly ITimeService _timeService;
        private readonly IGroup<GameEntity> _entities;

        public GravitySystem(GameContext game, ITimeService timeService)
        {
            _timeService = timeService;
            _entities = game.GetGroup(
                GameMatcher
                    .AllOf(
                        GameMatcher.Falling,
                        GameMatcher.Speed));
        }

        public void Execute()
        {
            foreach(GameEntity entity in _entities)
                entity.ReplaceSpeed(entity.Speed + Mathf.Abs(Physics2D.gravity.y) * _timeService.SmoothedDeltaTime);
        }
    }
}