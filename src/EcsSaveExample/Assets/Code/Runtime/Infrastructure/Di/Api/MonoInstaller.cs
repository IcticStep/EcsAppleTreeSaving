using UnityEngine;
using VContainer;

namespace Code.Runtime.Infrastructure.Di.Api
{
    internal class MonoInstaller : MonoBehaviour
    {
        public virtual void InstallBindings(IContainerBuilder builder) { }
    }
}