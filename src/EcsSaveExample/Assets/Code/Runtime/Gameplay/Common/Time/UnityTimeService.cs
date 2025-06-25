using System;

namespace Code.Runtime.Gameplay.Common.Time
{
    internal sealed class UnityTimeService : ITimeService
    {
        private bool _paused;

        public float DeltaTime => !_paused ? UnityEngine.Time.deltaTime : 0;

        public float SmoothedDeltaTime => _paused ? 0 : UnityEngine.Time.smoothDeltaTime;
        public DateTime UtcNow => DateTime.UtcNow;

        public void StopTime() =>
            _paused = true;

        public void StartTime() =>
            _paused = false;
    }
}