namespace Code.Runtime.Infrastructure.View.Registrars
{
    internal interface IEntityComponentRegistrar
    {
        void RegisterComponents();
        void UnregisterComponents();
    }
}