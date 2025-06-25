using Code.Runtime.Infrastructure.StaticData.Service;
using Code.Runtime.Infrastructure.UIRoot;
using Code.Runtime.UI.Common;
using Cysharp.Threading.Tasks;
using VContainer;
using VContainer.Unity;

namespace Code.Runtime.UI.Windows.Dark.Service
{
    internal sealed class DarkFader : IDarkFader
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IObjectResolver _objectResolver;
        private readonly IUIRootProvider _uiRootProvider;
        private Fader _fader;

        public DarkFader(IStaticDataService staticDataService, IObjectResolver objectResolver, IUIRootProvider uiRootProvider)
        {
            _staticDataService = staticDataService;
            _objectResolver = objectResolver;
            _uiRootProvider = uiRootProvider;
        }

        public void Initialize()
        {
            _fader = _objectResolver.Instantiate(_staticDataService.UiConfig.DarkFaderPrefab, parent: _uiRootProvider.UIRoot);
            HideGameImmediately();
        }

        public void ShowGame() =>
            _fader.Hide();

        public void HideGame() =>
            _fader.Show();

        public void ShowGameImmediately() =>
            _fader.HideImmediately();

        public void HideGameImmediately() =>
            _fader.ShowImmediately();

        public UniTask ShowGameAsync() =>
            _fader.HideAsync();

        public UniTask HideGameAsync() =>
            _fader.ShowAsync();
    }
}