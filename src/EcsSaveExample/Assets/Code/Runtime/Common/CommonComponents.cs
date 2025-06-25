using Code.Runtime.Infrastructure.Progress;
using Code.Runtime.Infrastructure.View;
using Entitas;

namespace Code.Runtime.Common
{
  [Game] public sealed class Destructed : IComponent { }
  [Game] public sealed class View : NonSerializableComponent { public IEntityView Value; }
  [Game] public sealed class SelfDestructTimer : IComponent { public float Value; }
  [Game] public sealed class Radius : IComponent { public float Value; }
  [Game] public sealed class Completed : IComponent { }
}