using Code.Runtime.Gameplay.Common.Time;
using Entitas;
using JetBrains.Annotations;

namespace Code.Runtime.Gameplay.Save.Systems
{
    [UsedImplicitly]
    public sealed class TickSaveTimerSystem : IExecuteSystem
    {
        private readonly ITimeService _time;
        private readonly IGroup<GameEntity> _timers;

        public TickSaveTimerSystem(GameContext game, ITimeService time)
        {
            _time = time;
            _timers = game.GetGroup(
                GameMatcher
                    .AllOf(GameMatcher.SaveTimer));
        }

        public void Execute()
        {
            foreach(GameEntity timer in _timers)
            {
                timer.ReplaceSaveTimer(timer.SaveTimer - _time.DeltaTime);
                if(timer.SaveTimer <= 0)
                {
                    timer.isCompleted = true;
                    timer.ReplaceSaveTimer(1);
                }
            }
        }
    }
}