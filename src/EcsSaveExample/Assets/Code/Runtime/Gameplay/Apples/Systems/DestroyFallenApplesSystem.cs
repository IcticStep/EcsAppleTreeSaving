using Code.Runtime.Infrastructure.StaticData.Service;
using Entitas;
using JetBrains.Annotations;

namespace Code.Runtime.Gameplay.Apples.Systems
{
    [UsedImplicitly]
    public sealed class DestroyFallenApplesSystem : IExecuteSystem
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IGroup<GameEntity> _apples;

        public DestroyFallenApplesSystem(GameContext game, IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
            _apples = game.GetGroup(
                GameMatcher
                    .AllOf(GameMatcher.Apple, GameMatcher.WorldPosition));
        }

        public void Execute()
        {
            foreach(GameEntity apple in _apples)
            {
                if(apple.WorldPosition.y < _staticDataService.AppleConfig.DestroyBeyondY)
                    apple.isDestructed = true;
            }
        }
    }
}