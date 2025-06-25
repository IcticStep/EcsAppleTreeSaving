using Code.Runtime.UI.Common;
using UnityEngine;

namespace Code.Runtime.UI
{
    [CreateAssetMenu(fileName = "UiConfig", menuName = "Static data/UiConfig")]
    public sealed class UiConfig : ScriptableObject
    {
        public Fader DarkFaderPrefab;
        public RectTransform UiRootPrefab;
        public GameObject EventSystemPrefab;
    }
}