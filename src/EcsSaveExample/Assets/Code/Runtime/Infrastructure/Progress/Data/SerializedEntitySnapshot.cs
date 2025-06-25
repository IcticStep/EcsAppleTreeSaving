using System;
using System.Collections.Generic;

namespace Code.Runtime.Infrastructure.Progress.Data
{
    [Serializable]
    public struct SerializedEntitySnapshot
    {
        public Dictionary<string, string> ComponentsByType;
    }
}