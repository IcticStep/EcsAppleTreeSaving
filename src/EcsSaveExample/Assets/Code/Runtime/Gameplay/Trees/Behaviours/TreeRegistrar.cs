using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Common.Extensions;
using Code.Runtime.Infrastructure.View.Registrars;
using UnityEngine;

namespace Code.Runtime.Gameplay.Trees.Behaviours
{
    internal sealed class TreeRegistrar : EntityComponentRegistrar
    {
        public List<Transform> ApplesSpawnPoints = new();
        
        public override void RegisterComponents()
        {
            List<Vector3> applesSpawnPositions = ApplesSpawnPoints.Select(x => x.position).ToList();
            
            Entity
                .With(x => x.isTree = true)
                .AddApplesSpawnPoints(applesSpawnPositions)
                .With(x => x.isNonSaveEntity = true);
        }

        public override void UnregisterComponents() { }
    }
}