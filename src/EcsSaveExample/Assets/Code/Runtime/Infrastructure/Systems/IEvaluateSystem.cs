using Entitas;

namespace Code.Runtime.Infrastructure.Systems
{
    public interface IEvaluateSystem<out TResult> : ISystem
    {
        TResult Evaluate();
    }
}