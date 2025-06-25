using Code.Runtime.Infrastructure.View.Registrars;

namespace Code.Runtime.Gameplay.Common.Movement.Registrars
{
  internal sealed class TransformRegistrar : EntityComponentRegistrar
  {
    public override void RegisterComponents() =>
      Entity
        .AddTransform(transform);

    public override void UnregisterComponents()
    {
      if (Entity.hasTransform)
        Entity.RemoveTransform();
    }
  }
}