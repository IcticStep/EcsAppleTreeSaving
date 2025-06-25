using Code.Runtime.Infrastructure.Di;
using Code.Runtime.Infrastructure.Di.Api;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Runtime.Gameplay.Common
{
    public class SwitchToEntryScene : MonoBehaviour
    {
#if UNITY_EDITOR
        private const int EntrySceneIndex = 0;

        private void Awake()
        {
            if (RootScope.HasInstance) 
                return;
      
            foreach (GameObject root in gameObject.scene.GetRootGameObjects()) 
                root.SetActive(false);
      
            SceneManager.LoadScene(EntrySceneIndex);
        }
#endif
    }
}