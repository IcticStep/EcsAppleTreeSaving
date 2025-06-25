using System.Collections.Generic;
using Code.Runtime.Common.Extensions;
using Entitas;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Runtime.Gameplay.Apples.Systems
{
    [UsedImplicitly]
    public sealed class ThrowTouchedApplesSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _apples;
        private readonly List<GameEntity> _buffer = new(4);

        public ThrowTouchedApplesSystem(GameContext game)
        {
            _apples = game.GetGroup(
                GameMatcher
                    .AllOf(
                        GameMatcher.Apple,
                        GameMatcher.TouchedThisFrame)
                    .NoneOf(GameMatcher.Falling));
        }

        public void Execute()
        {
            foreach(GameEntity apple in _apples.GetEntities(_buffer))
                apple
                    .With(x => x.isFalling = true)
                    .With(x => x.isMovementAvailable = true)
                    .With(x => x.isMoving = true)
                    .ReplaceDirection(Vector3.down)
                    .ReplaceSpeed(0);
        }
    }
}