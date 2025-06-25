using Entitas;

namespace Code.Runtime.Infrastructure.Progress.Extensions
{
    internal static class ContextExtensions
    {
        public static IEntity CreateEntity(this IContext context) =>
            (IEntity)context.GetType().GetMethod(nameof (CreateEntity))?.Invoke(context, null);
    }
}