using Code.Runtime.Gameplay;
using Code.Runtime.Infrastructure.Progress.Data;
using Entitas;

namespace Code.Runtime.Infrastructure.Progress.Api
{
    public interface IRecreator
    {
        EntityTypeId EntityType { get; }
        IEntity RecreateBy(EntitySnapshot snapshot);
    }
}