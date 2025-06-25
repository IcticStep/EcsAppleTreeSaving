using Code.Runtime.Gameplay.Apples.Behaviours.Animator;
using Code.Runtime.Infrastructure.Progress;
using Entitas;

namespace Code.Runtime.Gameplay.Apples
{
  [Game] public sealed class Apple : IComponent { }
  [Game] public sealed class Falling : IComponent { }
  [Game] public sealed class GrowProgress : IComponent { public float Value; }
  [Game] public sealed class AppleAnimator : NonSerializableComponent { public IAppleAnimator Value; }
  [Game] public sealed class AppleSpawnTimer : IComponent { public float Value; }
}