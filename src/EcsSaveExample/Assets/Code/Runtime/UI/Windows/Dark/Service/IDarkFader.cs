using Cysharp.Threading.Tasks;

namespace Code.Runtime.UI.Windows.Dark.Service
{
    internal interface IDarkFader
    {
        void Initialize();
        void ShowGame();
        void HideGame();
        void ShowGameImmediately();
        void HideGameImmediately();
        UniTask ShowGameAsync();
        UniTask HideGameAsync();
    }
}