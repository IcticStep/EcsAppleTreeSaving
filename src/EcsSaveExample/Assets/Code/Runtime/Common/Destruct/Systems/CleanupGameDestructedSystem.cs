using System.Collections.Generic;
using Entitas;

namespace Code.Runtime.Common.Destruct.Systems
{
    internal sealed class CleanupGameDestructedSystem : ICleanupSystem
    {
        private readonly IGroup<GameEntity> _entities;
        private readonly List<GameEntity> _buffer = new(64);

        public CleanupGameDestructedSystem(GameContext game)
        {
            _entities = game.GetGroup(GameMatcher.Destructed);
        }

        public void Cleanup()
        {
            foreach(GameEntity entity in _entities.GetEntities(_buffer))
                entity.Destroy();
        }
    }
}