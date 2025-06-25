using Code.Runtime.Gameplay.Common.Time;
using Entitas;
using UnityEngine;

namespace Code.Runtime.Gameplay.Common.Movement.Systems
{
  public class MoveDeltaSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _movers;
    private readonly ITimeService _time;

    public MoveDeltaSystem(GameContext game, ITimeService time)
    {
      _time = time;
      _movers = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.WorldPosition, 
          GameMatcher.Direction, 
          GameMatcher.Speed,
          GameMatcher.MovementAvailable, 
          GameMatcher.Moving));
    }

    public void Execute()
    {
      foreach (GameEntity mover in _movers) 
        mover.ReplaceWorldPosition(mover.WorldPosition + (Vector3)mover.Direction * mover.Speed * _time.SmoothedDeltaTime);
    }
  }
}