using Code.Runtime.Common.Destruct;
using Code.Runtime.Common.Touch;
using Code.Runtime.Gameplay.Apples;
using Code.Runtime.Gameplay.Common.Movement;
using Code.Runtime.Gameplay.Gravity.Systems;
using Code.Runtime.Gameplay.Save;
using Code.Runtime.Infrastructure.Systems;
using Code.Runtime.Infrastructure.View;
using JetBrains.Annotations;

namespace Code.Runtime.Gameplay.Level
{
    [UsedImplicitly]
    public sealed class LevelFeature : Feature
    {
        public LevelFeature(ISystemFactory systems)
        {
            Add(systems.Create<BindViewFeature>());
            Add(systems.Create<TouchFeature>());

            Add(systems.Create<ApplesFeature>());
            Add(systems.Create<GravitySystem>());

            Add(systems.Create<MovementFeature>());
            Add(systems.Create<ProcessDestructedFeature>());

            Add(systems.Create<SaveFeature>());
        }
    }
}