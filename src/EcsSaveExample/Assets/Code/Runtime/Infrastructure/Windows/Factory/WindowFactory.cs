using Code.Runtime.Infrastructure.StaticData.Service;
using Code.Runtime.Infrastructure.UIRoot;
using Code.Runtime.Infrastructure.Windows.Api;
using JetBrains.Annotations;
using VContainer;
using VContainer.Unity;

namespace Code.Runtime.Infrastructure.Windows.Factory
{
    [UsedImplicitly]
    internal sealed class WindowFactory : IWindowFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IObjectResolver _objectResolver;
        private readonly IUIRootProvider _rootProvider;

        public WindowFactory(
            IStaticDataService staticDataService,
            IObjectResolver objectResolver,
            IUIRootProvider rootProvider)
        {
            _staticDataService = staticDataService;
            _objectResolver = objectResolver;
            _rootProvider = rootProvider;
        }

        public BaseWindow CreateWindow(WindowTypeId windowTypeId) =>
            _objectResolver.Instantiate(PrefabFor(windowTypeId), _rootProvider.UIRoot);
        
        private BaseWindow PrefabFor(WindowTypeId typeId) =>
            _staticDataService.GetWindow(typeId);
    }
}