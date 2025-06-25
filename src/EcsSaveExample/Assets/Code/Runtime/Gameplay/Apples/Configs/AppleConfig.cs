using UnityEngine;

namespace Code.Runtime.Gameplay.Apples.Configs
{
    [CreateAssetMenu(fileName = "AppleConfig", menuName = "Static data/AppleConfig", order = 1)]
    public sealed class AppleConfig : ScriptableObject
    {
        public float SpawnInterval = 1.5f;
        public float DistanceCheckAccuracy = 0.01f;
        public float TimeToGrow = 3f;
        public float DestroyBeyondY = -10f;
    }
}