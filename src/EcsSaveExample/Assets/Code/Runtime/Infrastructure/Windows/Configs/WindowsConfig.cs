using AYellowpaper.SerializedCollections;
using Code.Runtime.Infrastructure.Windows.Api;
using UnityEngine;

namespace Code.Runtime.Infrastructure.Windows.Configs
{
    [CreateAssetMenu(fileName = "WindowsConfig", menuName = "Static data/WindowsConfig")]
    public sealed class WindowsConfig : ScriptableObject
    {
        [SerializedDictionary("Type", "Prefab")]
        public SerializedDictionary<WindowTypeId, BaseWindow> Windows;
    }
}