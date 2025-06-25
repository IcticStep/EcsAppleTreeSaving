using System.Collections.Generic;
using RangeInt = Code.Runtime.Common.Data.RangeInt;

namespace Code.Runtime.Gameplay.Common.Random
{
    public interface IRandomService
    {
        int GetInRange(RangeInt rangeExclusive);
        int GetInRangeInclusive(RangeInt rangeExclusive);
        float GetInRange(float minInclusive, float maxInclusive);
        int GetInRange(int minInclusive, int maxExclusive);
        bool Roll(float chance);
        int Roll(int dice);
        TElement GetRandomElementFromList<TElement>(List<TElement> elements);
        TElement GetRandomElement<TElement>(IEnumerable<TElement> elements);
    }
}