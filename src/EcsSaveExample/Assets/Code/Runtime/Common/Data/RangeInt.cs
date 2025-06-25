using System;

namespace Code.Runtime.Common.Data
{
    [Serializable]
    public struct RangeInt
    {
        public int Min;
        public int Max;

        public RangeInt(int min, int max)
        {
            Min = min;
            Max = max;
        }
    }
}