using Code.Runtime.Common.Extensions;
using Code.Runtime.Gameplay.Common;
using Code.Runtime.Infrastructure.Progress.Api;
using Code.Runtime.Infrastructure.Progress.Data;
using Entitas;
using UnityEngine;

namespace Code.Runtime.Gameplay.Apples.Factory
{
    internal sealed class ApplesFactory : IApplesFactory, IRecreator
    {
        private readonly GameContext _gameContext;

        public EntityTypeId EntityType => EntityTypeId.Apple;

        public ApplesFactory(GameContext gameContext)
        {
            _gameContext = gameContext;
        }

        public IEntity RecreateBy(EntitySnapshot snapshot)
        {
            Vector3 position = snapshot.GetComponent<WorldPosition>()?.Value ?? Vector3.zero;
            return CreateApple(position);
        }

        public GameEntity CreateApple(Vector3 position) =>
            _gameContext
                .CreateEntity()
                .AddEntityType(EntityTypeId.Apple)
                .With(x => x.isApple = true)
                .AddWorldPosition(position)
                .AddGrowProgress(0);
    }
}