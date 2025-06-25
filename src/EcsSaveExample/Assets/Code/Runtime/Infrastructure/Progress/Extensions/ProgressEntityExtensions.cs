using System;
using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Common.Extensions;
using Code.Runtime.Infrastructure.Progress.Data;
using Entitas;
using JetBrains.Annotations;

namespace Code.Runtime.Infrastructure.Progress.Extensions
{
    public static class ProgressEntityExtensions
    {
        public static IEntity UpdateWith(this IEntity entity, EntitySnapshot snapshot)
        {
            foreach(IComponent component in snapshot.Components)
            {
                int lookupIndex = LookupIndexOf(component, entity);
                entity.With(x => x.ReplaceComponent(lookupIndex, component), when: lookupIndex >= 0);
            }
            
            return entity;
        }

        public static bool RequiresSaving(this IEntity entity) =>
            !HasComponent<NonSaveEntity>(entity);

        public static EntitySnapshot AsSavedEntity(this IEntity entity) =>
            new()
            {
                Components = entity
                    .GetComponents()
                    .Where(x => x is not NonSerializableComponent)
                    .ToList(),
            };
        
        [CanBeNull]
        public static TComponent GetComponent<TComponent>(this IEntity entity)
            where TComponent : IComponent
        {
            int index = LookupIndexOf<TComponent>(entity);

            try
            {
                return entity.HasComponent(index)
                    ? (TComponent)entity.GetComponent(index)
                    : default(TComponent);
            }
            catch (Exception)
            {
                return default(TComponent);
            }
        }

        private static bool HasComponent<TComponent>(this IEntity entity) =>
            entity.HasComponent(LookupIndexOf<TComponent>(entity));

        private static int LookupIndexOf(IComponent component, IEntity entity) =>
            Array.IndexOf(ComponentTypes(entity), component.GetType());

        private static int LookupIndexOf<TComponent>(IEntity entity) =>
            Array.IndexOf(ComponentTypes(entity), typeof(TComponent));

        private static Type[] ComponentTypes(IEntity entity) =>
            entity switch
            {
                GameEntity => GameComponentsLookup.componentTypes,
                _ => throw new ArgumentException($"Requested lookup for entity of type {entity.GetType().Name} is not implemented."),
            };
    }
}