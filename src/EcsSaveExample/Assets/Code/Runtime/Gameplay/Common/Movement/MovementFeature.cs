using Code.Runtime.Gameplay.Common.Movement.Systems;
using Code.Runtime.Infrastructure.Systems;

namespace Code.Runtime.Gameplay.Common.Movement
{
  public class MovementFeature : Feature
  {
    public MovementFeature(ISystemFactory systems)
    {
      Add(systems.Create<MoveDeltaSystem>());
      
      Add(systems.Create<UpdateTransformPositionSystem>());
    }
  }
}