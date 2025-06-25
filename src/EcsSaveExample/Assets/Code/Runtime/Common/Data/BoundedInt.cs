using System;

namespace Code.Runtime.Common.Data
{
    [Serializable]
    public struct BoundedInt
    {
        public int CurrentValue;
        public int MaxValue;

        public BoundedInt(int currentValue, int maxValue)
        {
            CurrentValue = currentValue;
            MaxValue = maxValue;
        }
    }
}