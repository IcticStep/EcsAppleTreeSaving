using System.Collections.Generic;
using Entitas;
using JetBrains.Annotations;

namespace Code.Runtime.Gameplay.Save.Systems
{
    [UsedImplicitly]
    public sealed class CleanUpSaveRequestSystem : ICleanupSystem
    {
        private readonly IGroup<GameEntity> _requests;
        private readonly List<GameEntity> _buffer = new(2);

        public CleanUpSaveRequestSystem(GameContext game)
        {
            _requests = game.GetGroup(GameMatcher.AllOf(GameMatcher.SaveRequest));
        }

        public void Cleanup()
        {
            foreach(GameEntity request in _requests.GetEntities(_buffer))
                request.Destroy();
        }
    }
}