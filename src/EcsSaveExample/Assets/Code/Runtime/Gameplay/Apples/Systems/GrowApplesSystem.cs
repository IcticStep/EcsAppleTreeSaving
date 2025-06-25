using Code.Runtime.Infrastructure.StaticData.Service;
using Entitas;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Runtime.Gameplay.Apples.Systems
{
    [UsedImplicitly]
    public sealed class GrowApplesSystem : IExecuteSystem
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IGroup<GameEntity> _apples;

        public GrowApplesSystem(GameContext game, IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
            _apples = game.GetGroup(
                GameMatcher
                    .AllOf(
                        GameMatcher.Apple,
                        GameMatcher.GrowProgress));
        }

        public void Execute()
        {
            foreach(GameEntity apple in _apples)
            {
                float deltaProgress = Time.deltaTime / _staticDataService.AppleConfig.TimeToGrow;
                float progress = Mathf.Clamp01(apple.GrowProgress + deltaProgress);
                     apple.ReplaceGrowProgress(progress);
            }
        }
    }
}