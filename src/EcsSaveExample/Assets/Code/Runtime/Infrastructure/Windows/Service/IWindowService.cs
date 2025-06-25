using System;
using Code.Runtime.Infrastructure.Windows.Api;
using Cysharp.Threading.Tasks;

namespace Code.Runtime.Infrastructure.Windows.Service
{
    public interface IWindowService
    {
        event Action<WindowTypeId> Closed;
        event Action<WindowTypeId> CloseRequested;
        void Open(WindowTypeId windowTypeId);
        UniTask OpenAsync(WindowTypeId windowTypeId);
        void Open<TPayload>(WindowTypeId windowTypeId, TPayload payload);
        UniTask OpenAsync<TPayload>(WindowTypeId windowTypeId, TPayload payload);
        void Refresh<TPayload>(WindowTypeId windowTypeId, TPayload payload);
        void Close(WindowTypeId windowTypeId, bool silently = false);
        UniTask CloseAsync(WindowTypeId windowTypeId, bool silently = false);
        void CloseAll();
        UniTask CloseAllAsync();
        bool IsOpened(WindowTypeId windowTypeId);
    }
}