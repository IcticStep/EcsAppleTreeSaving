using System.Collections.Generic;
using Entitas;
using JetBrains.Annotations;

namespace Code.Runtime.Gameplay.Save.Systems
{
    [UsedImplicitly]
    public sealed class CleanUpCompletionSaveTimerSystem : ICleanupSystem
    {
        private readonly IGroup<GameEntity> _timers;
        private readonly List<GameEntity> _buffer = new(2);

        public CleanUpCompletionSaveTimerSystem(GameContext game)
        {
            _timers = game.GetGroup(
                GameMatcher
                    .AllOf(
                        GameMatcher.SaveTimer,
                        GameMatcher.Completed));
        }

        public void Cleanup()
        {
            foreach(GameEntity entity in _timers.GetEntities(_buffer))
                entity.isCompleted = false;
        }
    }
}