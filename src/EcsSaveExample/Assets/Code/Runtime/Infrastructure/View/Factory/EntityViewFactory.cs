using Code.Runtime.Infrastructure.StaticData.Service;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Runtime.Infrastructure.View.Factory
{
    internal sealed class EntityViewFactory : IEntityViewFactory
    {
        private readonly IObjectResolver _resolver;
        private readonly IStaticDataService _staticDataService;
        private readonly Vector3 _farAway = new(-999, 999, 0);

        public EntityViewFactory(IObjectResolver resolver, IStaticDataService staticDataService)
        {
            _resolver = resolver;
            _staticDataService = staticDataService;
        }

        public EntityBehaviour CreateViewForEntity(GameEntity entity)
        {
            EntityBehaviour view = _resolver.Instantiate(
                _staticDataService.GetViewPrefab(entity.EntityType),
                position: _farAway,
                entity.hasRotation ? entity.Rotation : Quaternion.identity,
                parent: null);

            view.SetEntity(entity);

            return view;
        }
    }
}