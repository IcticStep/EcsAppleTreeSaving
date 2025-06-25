using UnityEngine;

namespace Code.Runtime.Infrastructure.UIRoot
{
    internal interface IUIRootProvider
    {
        RectTransform UIRoot { get; }
        void SetUIRoot(RectTransform uiRoot);
        void Cleanup();
        void Initialize();
    }
}