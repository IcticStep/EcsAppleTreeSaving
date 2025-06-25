using Entitas;
using JetBrains.Annotations;

namespace Code.Runtime.Gameplay.Apples.Systems
{
    [UsedImplicitly]
    public sealed class UpdateApplesViewSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _apples;

        public UpdateApplesViewSystem(GameContext game)
        {
            _apples = game.GetGroup(
                GameMatcher
                    .AllOf(
                        GameMatcher.Apple,
                        GameMatcher.GrowProgress,
                        GameMatcher.AppleAnimator));
        }

        public void Execute()
        {
            foreach(GameEntity apple in _apples)
                apple.AppleAnimator.SetGrowProgress(apple.GrowProgress);
        }
    }
}