using System.Collections.Generic;
using Entitas;
using JetBrains.Annotations;

namespace Code.Runtime.Gameplay.Save.Systems
{
    [UsedImplicitly]
    public sealed class CleanUpLoadRequestSystem : ICleanupSystem
    {
        private readonly IGroup<GameEntity> _requests;
        private readonly List<GameEntity> _buffer = new(2);

        public CleanUpLoadRequestSystem(GameContext game)
        {
            _requests = game.GetGroup(GameMatcher.AllOf(GameMatcher.LoadRequest));
        }

        public void Cleanup()
        {
            foreach(GameEntity request in _requests.GetEntities(_buffer))
                request.Destroy();
        }
    }
}