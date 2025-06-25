using Code.Runtime.Infrastructure.Progress.Data;
using JetBrains.Annotations;

namespace Code.Runtime.Infrastructure.Progress.Provider
{
    [UsedImplicitly]
    internal sealed class ProgressProvider : IProgressProvider
    {
        public ProgressData ProgressData { get; private set; } = new();

        public void SetProgressData(ProgressData data) =>
            ProgressData = data;
    }
}