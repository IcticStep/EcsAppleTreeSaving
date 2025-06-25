using Code.Runtime.Gameplay.InitializeLevel;
using Code.Runtime.Infrastructure.GameStates.Api;
using Code.Runtime.Infrastructure.GameStates.Machine;
using Code.Runtime.Infrastructure.Systems;
using Code.Runtime.UI.Windows.Dark.Service;
using Cysharp.Threading.Tasks;

namespace Code.Runtime.Infrastructure.GameStates.States
{
    internal sealed class ReloadLevelState : IEnterableState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly GameContext _gameContext;
        private readonly IDarkFader _darkFader;
        private readonly ISystemFactory _systems;

        public ReloadLevelState(IGameStateMachine gameStateMachine, GameContext gameContext, IDarkFader darkFader, ISystemFactory systems)
        {
            _gameStateMachine = gameStateMachine;
            _gameContext = gameContext;
            _darkFader = darkFader;
            _systems = systems;
        }
        
        public void Enter() =>
            EnterAsync()
                .Forget();

        private async UniTaskVoid EnterAsync()
        {
            await _darkFader.HideGameAsync();
            
            MarkAllDestructed();
            CleanupDestructed();

            await _gameStateMachine.EnterAsync<LoadLevelState>();
        }

        private void MarkAllDestructed()
        {
            foreach(GameEntity gameEntity in _gameContext.GetEntities())
                gameEntity.isDestructed = true;
        }

        private void CleanupDestructed()
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