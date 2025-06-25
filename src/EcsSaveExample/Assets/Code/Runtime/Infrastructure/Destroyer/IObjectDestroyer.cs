using UnityEngine;

namespace Code.Runtime.Infrastructure.Destroyer
{
    public interface IObjectDestroyer
    {
        void Destroy(Object obj, float delay = 0f);
        void DestroyImmediate(Object obj);
    }
}