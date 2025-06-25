using Code.Runtime.Gameplay;
using Entitas;

namespace Code.Runtime.Infrastructure.Progress
{
    public abstract class NonSerializableComponent : IComponent { }
    [Game] public class NonSaveEntity : NonSerializableComponent { }
    [Game] public class EntityType : IComponent { public EntityTypeId Value; }
}