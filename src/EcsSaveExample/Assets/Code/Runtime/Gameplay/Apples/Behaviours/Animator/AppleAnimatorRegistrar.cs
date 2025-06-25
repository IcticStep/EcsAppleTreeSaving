using Code.Runtime.Infrastructure.View.Registrars;

namespace Code.Runtime.Gameplay.Apples.Behaviours.Animator
{
    internal sealed class AppleAnimatorRegistrar : EntityComponentRegistrar
    {
        public AppleAnimator AppleAnimator;

        public override void RegisterComponents() =>
            Entity.AddAppleAnimator(AppleAnimator);

        public override void UnregisterComponents()
        {
            if(Entity.hasAppleAnimator)
                Entity.RemoveAppleAnimator();
        }
    }
}