using System.Collections.Generic;
using Entitas;
using JetBrains.Annotations;

namespace Code.Runtime.Gameplay.Apples.Systems
{
    [UsedImplicitly]
    public sealed class CleanAppleSpawnTimerCompletionSystem : ICleanupSystem
    {
        private readonly IGroup<GameEntity> _appleSpawnTimers;
        private readonly List<GameEntity> _buffer = new(2);

        public CleanAppleSpawnTimerCompletionSystem(GameContext game)
        {
            _appleSpawnTimers = game.GetGroup(GameMatcher.AllOf(GameMatcher.AppleSpawnTimer, GameMatcher.Completed));
        }

        public void Cleanup()
        {
            foreach(GameEntity appleSpawnTimer in _appleSpawnTimers.GetEntities(_buffer))
                appleSpawnTimer.isCompleted = false;
        }
    }
}