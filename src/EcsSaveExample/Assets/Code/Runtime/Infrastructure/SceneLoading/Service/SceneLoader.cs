using Code.Runtime.Infrastructure.StaticData.Service;
using Cysharp.Threading.Tasks;
using Eflatun.SceneReference;
using UnityEngine.SceneManagement;

namespace Code.Runtime.Infrastructure.SceneLoading.Service
{
    internal sealed class SceneLoader : ISceneLoader
    {
        private readonly IStaticDataService _staticDataService;
        public string CurrentSceneName => SceneManager.GetActiveScene().name;

        public SceneLoader(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public UniTask LoadScene(SceneTypeId scene)
        {
            SceneReference sceneReference = _staticDataService.GetSceneReference(scene);
            return SceneManager
                .LoadSceneAsync(sceneReference.Name)
                .ToUniTask();
        }
    }
}