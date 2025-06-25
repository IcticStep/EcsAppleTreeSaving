namespace Code.Runtime.Infrastructure.View.Registrars
{
    internal abstract class EntityComponentRegistrar : EntityDependant, IEntityComponentRegistrar
    {
        public abstract void RegisterComponents();
        public abstract void UnregisterComponents();
    }
}