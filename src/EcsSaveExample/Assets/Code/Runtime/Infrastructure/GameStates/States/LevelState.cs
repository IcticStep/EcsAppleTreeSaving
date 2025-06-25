using Code.Runtime.Gameplay.Level;
using Code.Runtime.Infrastructure.GameStates.Api;
using Code.Runtime.Infrastructure.Systems;
using Code.Runtime.Infrastructure.Windows;
using Code.Runtime.Infrastructure.Windows.Service;
using Code.Runtime.UI.Windows.Dark.Service;
using JetBrains.Annotations;

namespace Code.Runtime.Infrastructure.GameStates.States
{
    [UsedImplicitly]
    internal sealed class LevelState : IEnterableState, IUpdateableState, IEndOfFrameExitableState
    {
        private readonly ISystemFactory _systems;
        private readonly IDarkFader _darkFader;
        private readonly GameContext _streetContext;
        private readonly IWindowService _windowService;
        private LevelFeature _feature;

        public LevelState(
            ISystemFactory systems,
            IDarkFader darkFader,
            GameContext gameContext,
            IWindowService windowService)
        {
            _systems = systems;
            _darkFader = darkFader;
            _streetContext = gameContext;
            _windowService = windowService;
        }
        
        public void Enter()
        {
            _feature = _systems.Create<LevelFeature>(); 
            _feature.Initialize();
            _darkFader.ShowGame();
            _windowService.Open(WindowTypeId.Hud);
        }

        public void Update()
        {
            _feature.Execute();
            _feature.Cleanup();
        }

        public void ExitOnEndOfFrame()
        {
            _windowService.Close(WindowTypeId.Hud);
            
            _feature.DeactivateReactiveSystems();
            _feature.ClearReactiveSystems();

            DestructEntities();
            
            _feature.Cleanup();
            _feature.TearDown();
            _feature = null;
        }
        
        private void DestructEntities()
        {
            foreach(GameEntity entity in _streetContext.GetEntities())
            {
                if(entity.hasView)
                    entity.View.ReleaseEntity();
                
                entity.Destroy();
            }
        }
    }
}