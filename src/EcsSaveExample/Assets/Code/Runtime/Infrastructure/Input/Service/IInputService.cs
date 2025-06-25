using System;
using UnityEngine;
using VContainer.Unity;

namespace Code.Runtime.Infrastructure.Input.Service
{
    internal interface IInputService : ITickable, IInitializable, IDisposable
    {
        bool Enabled { get; }
        void Enable();
        void Disable();
        event Action<Vector2> Pointed;
    }
}