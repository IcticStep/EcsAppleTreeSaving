using Entitas;
using JetBrains.Annotations;

namespace Code.Runtime.Gameplay.InitializeLevel.Systems
{
    [UsedImplicitly]
    public sealed class InitializeAppleSpawnTimerSystem : IInitializeSystem
    {
        private readonly GameContext _game;

        public InitializeAppleSpawnTimerSystem(GameContext game)
        {
            _game = game;
        }

        public void Initialize() =>
            _game
                .CreateEntity()
                .AddAppleSpawnTimer(0);
    }
}