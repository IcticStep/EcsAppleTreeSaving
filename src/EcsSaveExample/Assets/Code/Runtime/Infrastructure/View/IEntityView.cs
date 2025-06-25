using UnityEngine;

namespace Code.Runtime.Infrastructure.View
{
    public interface IEntityView
    {
        GameEntity Entity { get; }
        void SetEntity(GameEntity entity);
        void ReleaseEntity();

        GameObject gameObject { get; }
    }
}