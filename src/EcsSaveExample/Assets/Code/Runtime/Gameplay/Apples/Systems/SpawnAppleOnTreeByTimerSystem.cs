using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Gameplay.Apples.Factory;
using Code.Runtime.Gameplay.Common.Random;
using Code.Runtime.Infrastructure.StaticData.Service;
using Entitas;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Runtime.Gameplay.Apples.Systems
{
    [UsedImplicitly]
    public sealed class SpawnAppleOnTreeByTimerSystem : IExecuteSystem
    {
        private readonly IRandomService _randomService;
        private readonly IStaticDataService _staticDataService;
        private readonly IApplesFactory _applesFactory;
        private readonly IGroup<GameEntity> _appleTimers;
        private readonly IGroup<GameEntity> _trees;
        private readonly IGroup<GameEntity> _apples;

        public SpawnAppleOnTreeByTimerSystem(
            GameContext game,
            IRandomService randomService,
            IStaticDataService staticDataService,
            IApplesFactory applesFactory)
        {
            _randomService = randomService;
            _staticDataService = staticDataService;
            _applesFactory = applesFactory;
            _appleTimers = game.GetGroup(
                GameMatcher
                    .AllOf(
                        GameMatcher.AppleSpawnTimer,
                        GameMatcher.Completed));

            _trees = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Tree,
                GameMatcher.ApplesSpawnPoints));
            
            _apples = game.GetGroup(
                GameMatcher
                    .AllOf(
                        GameMatcher.Apple,
                        GameMatcher.WorldPosition));
        }

        public void Execute()
        {
            foreach(GameEntity _ in _appleTimers)
            foreach(GameEntity tree in _trees)
            {
                List<Vector3> appleSpawns = tree.ApplesSpawnPoints;
                List<Vector3> availableSpawnPoints = GetAvailableSpawnPoints(appleSpawns);
                
                if(!availableSpawnPoints.Any())
                    continue;
                
                Vector3 randomAppleSpawn = _randomService.GetRandomElementFromList(availableSpawnPoints);
                _applesFactory.CreateApple(randomAppleSpawn);
            }
        }

        private List<Vector3> GetAvailableSpawnPoints(IEnumerable<Vector3> appleSpawns) =>
            appleSpawns
                .Where(IsFree)
                .ToList();

        private bool IsFree(Vector3 spawn)
        {
            foreach(GameEntity apple in _apples)
                if(Vector3.Distance(apple.WorldPosition, spawn) < _staticDataService.AppleConfig.DistanceCheckAccuracy)  
                    return false;
            
            return true;
        }
    }
}