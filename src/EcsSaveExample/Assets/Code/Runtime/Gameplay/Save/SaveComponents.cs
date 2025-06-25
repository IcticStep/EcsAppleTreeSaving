using Code.Runtime.Infrastructure.Progress;
using Entitas;

namespace Code.Runtime.Gameplay.Save
{
    [Game] public sealed class SaveRequest : NonSerializableComponent { }
    [Game] public sealed class LoadRequest : NonSerializableComponent { }
    [Game] public sealed class SaveProgress : IComponent { }
    [Game] public sealed class SaveTimer : IComponent { public float Value; }
}