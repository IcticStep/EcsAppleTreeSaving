using Code.Runtime.Common.Data;
using Entitas;

namespace Code.Runtime.Gameplay.Common.Movement
{
  [Game] public class Direction : IComponent { public SerializableVector3 Value; }
  [Game] public class Speed : IComponent { public float Value; }
  [Game] public class Moving : IComponent { }
  [Game] public class MovementAvailable : IComponent { }
}