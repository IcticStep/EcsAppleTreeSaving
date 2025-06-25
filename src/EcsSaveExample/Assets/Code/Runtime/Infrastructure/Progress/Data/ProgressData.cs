using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace Code.Runtime.Infrastructure.Progress.Data
{
    [Serializable]
    public sealed class ProgressData
    {
        public List<EntitySnapshot> GameSnapshots = new();
    }
}