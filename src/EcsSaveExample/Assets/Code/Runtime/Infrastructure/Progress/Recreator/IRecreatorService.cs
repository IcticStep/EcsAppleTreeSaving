using Code.Runtime.Infrastructure.Progress.Data;
using Entitas;

namespace Code.Runtime.Infrastructure.Progress.Recreator
{
    internal interface IRecreatorService
    {
        IEntity Recreate<TContext>(EntitySnapshot snapshot, TContext context)
            where TContext : IContext;
    }
}