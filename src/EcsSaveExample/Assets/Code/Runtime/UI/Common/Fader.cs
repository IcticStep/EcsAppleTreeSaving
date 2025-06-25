using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Code.Runtime.UI.Common
{
    public sealed class Fader : MonoBehaviour
    {
        public CanvasGroup CanvasGroup;
        public float Duration;
        public Ease EaseIn;
        public Ease EaseOut;

        private Tween _tween;

        public bool IsVisible { get; private set; }
        
        public event Action BecameVisible;
        public event Action BecameInvisible;
        public event Action ShowTriggered;
        public event Action HideTriggered;

        public void Show() => 
            ShowAsync().Forget();
        
        public void Hide() => 
            HideAsync().Forget();
        
        public void ShowImmediately()
        {
            _tween?.Kill();
            CanvasGroup.alpha = 1;
            SetVisibleState();
        }

        public void HideImmediately()
        {
            _tween?.Kill();
            CanvasGroup.alpha = 0;
            SetInvisibleState();
        }

        public UniTask ShowAsync()
        {
            ShowTriggered?.Invoke();
            _tween?.Kill();
            _tween = CanvasGroup
                .DOFade(1, Duration)
                .SetEase(EaseIn)
                .SetLink(gameObject)
                .OnComplete(SetVisibleState);
            
            return _tween.ToUniTask();
        }

        public UniTask HideAsync()
        {
            HideTriggered?.Invoke();
            _tween?.Kill();
            _tween = CanvasGroup
                .DOFade(0, Duration)
                .SetEase(EaseOut)
                .SetLink(gameObject)
                .OnComplete(SetInvisibleState);

            return _tween.ToUniTask();
        }

        private void SetVisibleState()
        {
            IsVisible = true;
            BecameVisible?.Invoke();
        }

        private void SetInvisibleState()
        {
            IsVisible = false;
            BecameInvisible?.Invoke();
        }
    }
}