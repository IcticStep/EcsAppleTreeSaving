using Entitas;
using JetBrains.Annotations;

namespace Code.Runtime.Gameplay.Save.Systems
{
    [UsedImplicitly]
    public sealed class InitializeSaveTimerSystem : IInitializeSystem
    {
        private readonly GameContext _game;

        public InitializeSaveTimerSystem(GameContext game)
        {
            _game = game;
        }

        public void Initialize() =>
            _game
                .CreateEntity()
                .AddSaveTimer(1)
                .isNonSaveEntity = true;
    }
}