using Code.Runtime.Common.Data;
using Code.Runtime.Infrastructure.Progress;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Code.Runtime.Gameplay.Common
{
  [Game] public sealed class Id : IComponent { [PrimaryEntityIndex] public int Value; }
  [Game] public sealed class EntityLink : NonSerializableComponent { [EntityIndex] public int Value; }
  
  [Game] public sealed class WorldPosition : IComponent { public SerializableVector3 Value; }
  [Game] public sealed class Rotation : IComponent { public SerializableQuaternion Value; }
  [Game] public sealed class TransformComponent : NonSerializableComponent { public Transform Value; }
  
  [Game] public sealed class Proceeded : IComponent { }
  [Game] public sealed class BeingProcessed : IComponent { }
}