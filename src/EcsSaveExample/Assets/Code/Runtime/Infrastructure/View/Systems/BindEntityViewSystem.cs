using System.Collections.Generic;
using Code.Runtime.Infrastructure.View.Factory;
using Entitas;

namespace Code.Runtime.Infrastructure.View.Systems
{
    internal sealed class BindEntityViewSystem : IExecuteSystem
    {
        private readonly IEntityViewFactory _entityViewFactory;
        private readonly IGroup<GameEntity> _entities;
        private readonly List<GameEntity> _buffer = new(32);

        public BindEntityViewSystem(GameContext game, IEntityViewFactory entityViewFactory)
        {
            _entityViewFactory = entityViewFactory;
            _entities = game.GetGroup(
                GameMatcher
                    .AllOf(GameMatcher.EntityType)
                    .NoneOf(GameMatcher.View));
        }

        public void Execute()
        {
            foreach(GameEntity entity in _entities.GetEntities(_buffer))
                _entityViewFactory.CreateViewForEntity(entity);
        }
    }
}