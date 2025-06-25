using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace Code.Runtime.Infrastructure.Input.Service
{
    [UsedImplicitly]
    internal sealed class InputService : IInputService 
    {
        private InputActions _inputSystemActions;

        private InputAction PointProperty => _inputSystemActions?.UI.Point;
        public bool Enabled { get; private set; }

        public event Action<Vector2> Pointed = delegate { };

        void IInitializable.Initialize()
        {
            _inputSystemActions = new InputActions();
            _inputSystemActions.UI.Point.performed += OnPointPerformed;
        }

        void IDisposable.Dispose()
        {
            _inputSystemActions.UI.Point.performed -= OnPointPerformed;
            _inputSystemActions.Dispose();
        }

        public void Enable()
        {
            _inputSystemActions.Enable();
            Enabled = true;
        }

        public void Disable()
        {
            _inputSystemActions.Disable();
            Enabled = false;
        }

        public void Tick()
        {
            if(!Enabled)
                return;
        }

        private void OnPointPerformed(InputAction.CallbackContext context)
        {
            Vector2 touchedScreenPosition = PointProperty.ReadValue<Vector2>();
            Pointed?.Invoke(touchedScreenPosition);
        }
    }
}