using Code.Runtime.Infrastructure.StaticData.Service;
using JetBrains.Annotations;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Runtime.Infrastructure.UIRoot
{
    [UsedImplicitly]
    internal sealed class UIRootProvider : IUIRootProvider
    {
        private readonly IObjectResolver _objectResolver;
        private readonly IStaticDataService _staticDataService;
        public RectTransform UIRoot { get; private set; }

        public UIRootProvider(IObjectResolver objectResolver, IStaticDataService staticDataService)
        {
            _objectResolver = objectResolver;
            _staticDataService = staticDataService;
        }

        public void SetUIRoot(RectTransform uiRoot) =>
            UIRoot = uiRoot;

        public void Initialize() =>
            UIRoot = _objectResolver.Instantiate(_staticDataService.UiConfig.UiRootPrefab.gameObject).GetComponent<RectTransform>();

        public void Cleanup() =>
            UIRoot = null;
    }
}