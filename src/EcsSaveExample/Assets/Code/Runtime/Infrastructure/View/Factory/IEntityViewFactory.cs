namespace Code.Runtime.Infrastructure.View.Factory
{
    internal interface IEntityViewFactory
    {
        EntityBehaviour CreateViewForEntity(GameEntity entity);
    }
}