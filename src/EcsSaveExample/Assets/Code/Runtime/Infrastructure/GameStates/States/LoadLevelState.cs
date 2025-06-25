using Code.Runtime.Gameplay.InitializeLevel;
using Code.Runtime.Infrastructure.GameStates.Api;
using Code.Runtime.Infrastructure.GameStates.Machine;
using Code.Runtime.Infrastructure.Progress.SaveLoad;
using Code.Runtime.Infrastructure.SceneLoading;
using Code.Runtime.Infrastructure.SceneLoading.Service;
using Code.Runtime.Infrastructure.Systems;
using Cysharp.Threading.Tasks;
using Entitas;

namespace Code.Runtime.Infrastructure.GameStates.States
{
    internal sealed class LoadLevelState : IEnterableState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISaveLoadService _saveLoadService;
        private readonly ISystemFactory _systems;

        public LoadLevelState(
            ISceneLoader sceneLoader,
            IGameStateMachine gameStateMachine,
            ISaveLoadService saveLoadService,
            ISystemFactory systems)
        {
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
            _saveLoadService = saveLoadService;
            _systems = systems;
        }
        
        public void Enter() =>
            EnterAsync()
                .Forget();

        private async UniTaskVoid EnterAsync()
        {
            await _sceneLoader.LoadScene(SceneTypeId.Level);

            if(_saveLoadService.HasSavedProgress)
                _saveLoadService.LoadProgress();
            else
                InitializeLevel();

            await _gameStateMachine.EnterAsync<LevelState>();
        }

        private void InitializeLevel()
        {
            InitializeLevelFeature feature = _systems.Create<InitializeLevelFeature>();
            feature.Initialize();
            
            feature.Execute();
            feature.Cleanup();
            
            feature.DeactivateReactiveSystems();
            feature.ClearReactiveSystems();
            feature.Cleanup();
            feature.TearDown();
        }
    }
}