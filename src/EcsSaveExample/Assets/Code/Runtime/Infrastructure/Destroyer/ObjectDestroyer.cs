using JetBrains.Annotations;
using UnityEngine;

namespace Code.Runtime.Infrastructure.Destroyer
{
    [UsedImplicitly]
    public class ObjectDestroyer : IObjectDestroyer
    {
        public void Destroy(Object obj, float delay = 0f) =>
            Object.Destroy(obj, delay);

        public void DestroyImmediate(Object obj) =>
            Object.DestroyImmediate(obj);
    }
}