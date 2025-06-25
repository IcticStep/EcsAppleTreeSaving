namespace Code.Runtime.Infrastructure.Progress.SaveLoad
{
    public interface ISaveLoadService
    {
        void SaveProgress();
        void LoadProgress();
        bool HasSavedProgress { get; }
        bool ProgressWasLoaded { get; }
    }
}