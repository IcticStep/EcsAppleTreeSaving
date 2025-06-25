using System.Collections.Generic;
using Entitas;
using JetBrains.Annotations;

namespace Code.Runtime.Common.Touch.Systems
{
    [UsedImplicitly]
    public sealed class CleanUpTouchSystem : ICleanupSystem
    {
        private readonly IGroup<GameEntity> _entities;
        private readonly List<GameEntity> _buffer = new(32);

        public CleanUpTouchSystem(GameContext game)
        {
            _entities = game.GetGroup(GameMatcher.AllOf(GameMatcher.TouchedThisFrame));
        }

        public void Cleanup()
        {
            foreach(GameEntity entity in _entities.GetEntities(_buffer))
                entity.isTouchedThisFrame = false;
        }
    }
}