using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Code.Runtime.Gameplay.Trees
{
  [Game] public sealed class Tree : IComponent { }
  [Game] public sealed class ApplesSpawnPoints : IComponent { public List<Vector3> Value; }
}