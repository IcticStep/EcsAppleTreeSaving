using UnityEngine;

namespace Code.Runtime.Gameplay.Apples.Behaviours.Animator
{
    internal sealed class AppleAnimator : MonoBehaviour, IAppleAnimator
    {
        public Transform Transform;
        public SpriteRenderer SpriteRenderer;
        
        public float MinimalSize = 0.5f;
        public float MaxSize = 1f;
        public float MinimalOpacity = 0.5f;
        public float MaxOpacity = 1f;

        private void OnValidate()
        {
            Transform ??= GetComponent<Transform>();
            SpriteRenderer ??= GetComponent<SpriteRenderer>();
        }

        public void SetGrowProgress(float progress)
        {
            SetSizeByProgress(progress);
            SetOpacityByProgress(progress);
        }

        private void SetSizeByProgress(float clampedProgress)
        {
            float size = Mathf.Lerp(MinimalSize, MaxSize, clampedProgress);
            Transform.localScale = Vector3.one * size;
        }

        private void SetOpacityByProgress(float clampedProgress)
        {
            float opacity = Mathf.Lerp(MinimalOpacity, MaxOpacity, clampedProgress);
            Color color = SpriteRenderer.color;
            color.a = opacity;
            SpriteRenderer.color = color;
        }
    }
}