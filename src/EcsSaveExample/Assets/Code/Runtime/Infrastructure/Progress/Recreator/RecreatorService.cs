using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Gameplay;
using Code.Runtime.Gameplay.Common;
using Code.Runtime.Infrastructure.Identifiers;
using Code.Runtime.Infrastructure.Progress.Api;
using Code.Runtime.Infrastructure.Progress.Data;
using Code.Runtime.Infrastructure.Progress.Extensions;
using Entitas;
using JetBrains.Annotations;

namespace Code.Runtime.Infrastructure.Progress.Recreator
{
    [UsedImplicitly]
    internal sealed class RecreatorService : IRecreatorService
    {
        private readonly IIdentifierService _identifierService;
        private readonly Dictionary<EntityTypeId, IRecreator> _recreatorsByType;

        public RecreatorService(IEnumerable<IRecreator> hydrators, IIdentifierService identifierService)
        {
            _identifierService = identifierService;
            _recreatorsByType = hydrators.ToDictionary(x => x.EntityType);
        }

        public IEntity Recreate<TContext>(EntitySnapshot snapshot, TContext context)
            where TContext : IContext
        {
            IEntity entity = RecreateEntity(snapshot, context);
            ReserveIdIfAny(entity);
            return entity;
        }

        private IEntity RecreateEntity<TContext>(EntitySnapshot snapshot, TContext context)
            where TContext : IContext
        {
            EntityTypeId type = GetEntityType(snapshot);
            if(type == EntityTypeId.Unknown)
                return RecreateFromNew(snapshot, context);

            bool hydratorFound = _recreatorsByType.TryGetValue(type, out IRecreator recreator);

            return hydratorFound
                ? Recreate(snapshot, recreator)
                : RecreateFromNew(snapshot, context);
        }

        private static IEntity Recreate(EntitySnapshot snapshot, IRecreator recreator) =>
            recreator
                .RecreateBy(snapshot)
                .UpdateWith(snapshot);

        private static IEntity RecreateFromNew<TContext>(EntitySnapshot snapshot, TContext context)
            where TContext : IContext =>
            context
                .CreateEntity()
                .UpdateWith(snapshot);

        private static EntityTypeId GetEntityType(EntitySnapshot snapshot) =>
            snapshot
                .GetComponent<EntityType>()
                ?.Value ?? EntityTypeId.Unknown;

        private void ReserveIdIfAny(IEntity entity)
        {
            Id component = entity.GetComponent<Id>();
            if(component == null)
                return;
            
            _identifierService.SetIdUsed(component.Value);
        }
    }
}