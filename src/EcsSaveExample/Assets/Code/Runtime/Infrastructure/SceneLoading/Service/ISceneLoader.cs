using Cysharp.Threading.Tasks;

namespace Code.Runtime.Infrastructure.SceneLoading.Service
{
    public interface ISceneLoader
    {
        string CurrentSceneName { get; }
        UniTask LoadScene(SceneTypeId scene);
    }
}