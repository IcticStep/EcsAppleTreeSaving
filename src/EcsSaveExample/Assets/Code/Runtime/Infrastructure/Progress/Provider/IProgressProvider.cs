using Code.Runtime.Infrastructure.Progress.Data;

namespace Code.Runtime.Infrastructure.Progress.Provider
{
    public interface IProgressProvider
    {
        ProgressData ProgressData { get; }
        void SetProgressData(ProgressData data);
    }
}