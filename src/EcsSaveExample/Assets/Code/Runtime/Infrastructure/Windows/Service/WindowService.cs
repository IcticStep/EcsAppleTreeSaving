using System;
using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Infrastructure.Destroyer;
using Code.Runtime.Infrastructure.Windows.Api;
using Code.Runtime.Infrastructure.Windows.Factory;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

namespace Code.Runtime.Infrastructure.Windows.Service
{
    [UsedImplicitly]
    internal sealed class WindowService : IWindowService
    {
        private readonly IWindowFactory _windowFactory;
        private readonly IObjectDestroyer _objectDestroyer;

        private readonly Dictionary<WindowTypeId, BaseWindow> _openedWindows = new();

        public event Action<WindowTypeId> Closed;
        public event Action<WindowTypeId> CloseRequested;

        public WindowService(IWindowFactory windowFactory, IObjectDestroyer objectDestroyer)
        {
            _windowFactory = windowFactory;
            _objectDestroyer = objectDestroyer;
        }
        
        public void Open(WindowTypeId windowTypeId) =>
            OpenAsync(windowTypeId).Forget();

        public UniTask OpenAsync(WindowTypeId windowTypeId)
        {
            BaseWindow openedWindow = _windowFactory.CreateWindow(windowTypeId);
            TryAddToOpened(windowTypeId, openedWindow);
            return openedWindow.Show();
        }

        public void Open<TPayload>(WindowTypeId windowTypeId, TPayload payload) =>
            OpenAsync(windowTypeId, payload).Forget();

        public UniTask OpenAsync<TPayload>(WindowTypeId windowTypeId, TPayload payload)
        {
            BaseWindow openedWindow = _windowFactory.CreateWindow(windowTypeId);
            if(openedWindow is PayloadedWindow<TPayload> windowInstance)
            {
                windowInstance.Set(payload);
                TryAddToOpened(windowTypeId, openedWindow);
                return openedWindow.Show();
            }

            throw new InvalidOperationException($"Window {windowTypeId} is not a PayloadedWindow<{typeof(TPayload)}>");
        }

        public void Refresh<TPayload>(WindowTypeId windowTypeId, TPayload payload)
        {
            bool windowFound = _openedWindows.TryGetValue(windowTypeId, out BaseWindow openedWindow);
            if(!windowFound)
                return;

            if(openedWindow is PayloadedWindow<TPayload> windowInstance)
                windowInstance.Set(payload);
            else
                throw new InvalidOperationException($"Window {windowTypeId} is not a PayloadedWindow<{typeof(TPayload)}>");
        }

        public void Close(WindowTypeId windowTypeId, bool silently = false) =>
            CloseAsync(windowTypeId, silently).Forget();

        public async UniTask CloseAsync(WindowTypeId windowTypeId, bool silently = false)
        {
            CloseRequested?.Invoke(windowTypeId);
            bool windowFound = _openedWindows.TryGetValue(windowTypeId, out BaseWindow openedWindow);
            if(!windowFound && silently)
                return;

            if(!windowFound)
                throw new InvalidOperationException($"There is no opened window with id {windowTypeId.ToString()}.");

            _openedWindows.Remove(windowTypeId);
            await openedWindow.Hide();
            _objectDestroyer.Destroy(openedWindow.gameObject);

            Closed?.Invoke(windowTypeId);
        }

        public void CloseAll() =>
            CloseAllAsync().Forget();

        public UniTask CloseAllAsync()
        {
            List<WindowTypeId> openedWindowsIds = _openedWindows.Keys.ToList();
            return UniTask.WhenAll(openedWindowsIds.Select(x => CloseAsync(x)));
        }
        
        public bool IsOpened(WindowTypeId windowTypeId) =>
            _openedWindows.ContainsKey(windowTypeId);

        private void TryAddToOpened(WindowTypeId windowTypeId, BaseWindow openedWindow)
        {
            bool opened = _openedWindows.TryAdd(windowTypeId, openedWindow);
            if(!opened)
                throw new InvalidOperationException($"Window with id {windowTypeId.ToString()} is already opened.");
        }
    }
}