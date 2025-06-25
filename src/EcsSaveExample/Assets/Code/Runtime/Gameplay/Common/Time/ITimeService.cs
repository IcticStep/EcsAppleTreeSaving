using System;

namespace Code.Runtime.Gameplay.Common.Time
{
    public interface ITimeService
    {
        float DeltaTime { get; }
        DateTime UtcNow { get; }
        float SmoothedDeltaTime { get; }
        void StopTime();
        void StartTime();
    }
}