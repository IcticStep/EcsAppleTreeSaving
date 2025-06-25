using Code.Runtime.Gameplay.Apples.Factory;
using Code.Runtime.Gameplay.Common.Random;
using Code.Runtime.Gameplay.Common.Time;
using Code.Runtime.Infrastructure.Destroyer;
using Code.Runtime.Infrastructure.Di.Api;
using Code.Runtime.Infrastructure.GameStates.Factory;
using Code.Runtime.Infrastructure.GameStates.Machine;
using Code.Runtime.Infrastructure.GameStates.States;
using Code.Runtime.Infrastructure.Identifiers;
using Code.Runtime.Infrastructure.Input.Service;
using Code.Runtime.Infrastructure.Progress.Provider;
using Code.Runtime.Infrastructure.Progress.Recreator;
using Code.Runtime.Infrastructure.Progress.SaveLoad;
using Code.Runtime.Infrastructure.SceneLoading.Service;
using Code.Runtime.Infrastructure.StaticData.Service;
using Code.Runtime.Infrastructure.Systems;
using Code.Runtime.Infrastructure.UIRoot;
using Code.Runtime.Infrastructure.View.Factory;
using Code.Runtime.Infrastructure.Windows.Factory;
using Code.Runtime.Infrastructure.Windows.Service;
using Code.Runtime.UI.Windows.Dark.Service;
using VContainer;
using VContainer.Unity;

namespace Code.Runtime.Infrastructure.Di.Installers
{
    internal sealed class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings(IContainerBuilder builder)
        {
            BindContexts(builder);
            BindInfrastructureServices(builder);
            BindCommonServices(builder);
            BindGameplayFactories(builder);
            BindGameStateMachine(builder);
            BindGameStates(builder);
            builder.RegisterEntryPoint<EntryPoint>();
        }

        private void BindContexts(IContainerBuilder builder)
        {
            Contexts.sharedInstance = new Contexts();
            builder.RegisterInstance(Contexts.sharedInstance.game);
        }

        private void BindInfrastructureServices(IContainerBuilder builder)
        {
            builder.Register<IIdentifierService, IdentifierService>(Lifetime.Singleton);
            builder.Register<ISceneLoader, SceneLoader>(Lifetime.Singleton);
            builder.Register<ISystemFactory, SystemFactory>(Lifetime.Singleton);
            builder.Register<IEntityViewFactory, EntityViewFactory>(Lifetime.Singleton);
            builder.Register<IStaticDataService, StaticDataService>(Lifetime.Singleton);
            builder.Register<IObjectDestroyer, ObjectDestroyer>(Lifetime.Singleton);
            builder.Register<IWindowFactory, WindowFactory>(Lifetime.Singleton);
            builder.Register<IUIRootProvider, UIRootProvider>(Lifetime.Singleton);
            builder.Register<IWindowService, WindowService>(Lifetime.Singleton);
            builder.Register<IDarkFader, DarkFader>(Lifetime.Singleton);
            builder.Register<IProgressProvider, ProgressProvider>(Lifetime.Singleton);
            builder.Register<ISaveLoadService, SaveLoadService>(Lifetime.Singleton);
            builder.Register<IRecreatorService, RecreatorService>(Lifetime.Singleton);
        }

        private void BindCommonServices(IContainerBuilder builder)
        {
            builder.Register<ITimeService, UnityTimeService>(Lifetime.Singleton);
            builder.Register<IRandomService, RandomService>(Lifetime.Singleton);
            builder.Register<InputService>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private void BindGameplayFactories(IContainerBuilder builder) =>
            builder.Register<IApplesFactory, ApplesFactory>(Lifetime.Singleton);
        
        private void BindGameStateMachine(IContainerBuilder builder)
        {
            builder.Register<IGameStateMachine, ITickable, GameStateMachine>(Lifetime.Singleton);
            builder.Register<IStateFactory, StateFactory>(Lifetime.Singleton);
        }

        private void BindGameStates(IContainerBuilder builder)
        {
            builder.Register<BootstrapState>(Lifetime.Singleton);
            
            builder.Register<LoadLevelState>(Lifetime.Singleton);
            builder.Register<LevelState>(Lifetime.Singleton);
            
            builder.Register<ReloadLevelState>(Lifetime.Singleton);
        }
    }
}