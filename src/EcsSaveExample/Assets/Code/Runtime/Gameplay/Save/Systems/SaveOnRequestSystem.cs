using Code.Runtime.Infrastructure.Progress.SaveLoad;
using Entitas;
using JetBrains.Annotations;

namespace Code.Runtime.Gameplay.Save.Systems
{
    [UsedImplicitly]
    public sealed class SaveOnRequestSystem : IExecuteSystem
    {
        private readonly ISaveLoadService _saveLoadService;
        private readonly IGroup<GameEntity> _requests;

        public SaveOnRequestSystem(GameContext game, ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
            _requests = game.GetGroup(
                GameMatcher
                    .AllOf(GameMatcher.SaveRequest));
        }

        public void Execute()
        {
            foreach(GameEntity _ in _requests)
                _saveLoadService.SaveProgress();
        }
    }
}