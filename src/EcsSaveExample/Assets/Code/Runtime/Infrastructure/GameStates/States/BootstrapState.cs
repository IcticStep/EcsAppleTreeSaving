using Code.Runtime.Infrastructure.GameStates.Api;
using Code.Runtime.Infrastructure.GameStates.Machine;
using Code.Runtime.Infrastructure.Input.Service;
using Code.Runtime.Infrastructure.SceneLoading;
using Code.Runtime.Infrastructure.SceneLoading.Service;
using Code.Runtime.Infrastructure.StaticData.Service;
using Code.Runtime.Infrastructure.UIRoot;
using Code.Runtime.UI.Windows.Dark.Service;
using Cysharp.Threading.Tasks;
using VContainer;
using VContainer.Unity;

namespace Code.Runtime.Infrastructure.GameStates.States
{
    internal sealed class BootstrapState : IEnterableState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IStaticDataService _staticDataService;
        private readonly IUIRootProvider _uiRootProvider;
        private readonly IDarkFader _darkFader;
        private readonly IObjectResolver _objectResolver;
        private readonly ISceneLoader _sceneLoader;
        private readonly IInputService _inputService;

        public BootstrapState(
            IGameStateMachine stateMachine,
            IStaticDataService staticDataService,
            IUIRootProvider uiRootProvider,
            IDarkFader darkFader,
            IObjectResolver objectResolver,
            ISceneLoader sceneLoader,
            IInputService inputService)
        {
            _stateMachine = stateMachine;
            _staticDataService = staticDataService;
            _uiRootProvider = uiRootProvider;
            _darkFader = darkFader;
            _objectResolver = objectResolver;
            _sceneLoader = sceneLoader;
            _inputService = inputService;
        }

        public void Enter()
        {
            _staticDataService.LoadScenesConfig();
            _sceneLoader.LoadScene(SceneTypeId.Bootstrap);

            UniTaskScheduler.PropagateOperationCanceledException = true;
            _staticDataService.LoadAll();
            _uiRootProvider.Initialize();
            _darkFader.Initialize();
            _inputService.Enable();
            _objectResolver.Instantiate(_staticDataService.UiConfig.EventSystemPrefab);
            
            _stateMachine.Enter<LoadLevelState>();
        }
    }
}