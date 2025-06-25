using Code.Runtime.Common.Extensions;
using Code.Runtime.Infrastructure.Windows;
using Code.Runtime.Infrastructure.Windows.Api;
using UnityEngine.UI;
using VContainer;

namespace Code.Runtime.UI.Windows.Hud
{
    internal sealed class HudWindow : BaseWindow
    {
        public Button LoadButton;
        public Button SaveButton;
        
        private GameContext _gameContext;

        [Inject]
        private void Construct(GameContext gameContext) =>
            _gameContext = gameContext;

        protected override void OnAwake() =>
            Id = WindowTypeId.Hud;

        protected override void SubscribeNavigation()
        {
            LoadButton.onClick.AddListener(OnLoadButtonClicked);
            SaveButton.onClick.AddListener(OnSaveButtonClicked);
        }
        
        protected override void UnsubscribeNavigation()
        {
            LoadButton.onClick.RemoveListener(OnLoadButtonClicked);
            SaveButton.onClick.RemoveListener(OnSaveButtonClicked);
        }

        private void OnSaveButtonClicked() =>
            _gameContext
                .CreateEntity()
                .With(x => x.isSaveRequest = true)
                .With(x => x.isNonSaveEntity = true);

        private void OnLoadButtonClicked() =>
            _gameContext
                .CreateEntity()
                .With(x => x.isLoadRequest = true)
                .With(x => x.isNonSaveEntity = true);
    }
}