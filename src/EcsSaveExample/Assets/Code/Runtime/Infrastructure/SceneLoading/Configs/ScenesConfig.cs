using AYellowpaper.SerializedCollections;
using Eflatun.SceneReference;
using UnityEngine;

namespace Code.Runtime.Infrastructure.SceneLoading.Configs
{
    [CreateAssetMenu(fileName = "ScenesConfig", menuName = "Static data/Scenes Config")]
    public sealed class ScenesConfig : ScriptableObject
    {
        [SerializedDictionary("Type", "Reference")]
        public SerializedDictionary<SceneTypeId, SceneReference> Scenes;
    }
}