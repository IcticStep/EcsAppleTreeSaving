using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using RangeInt = Code.Runtime.Common.Data.RangeInt;

namespace Code.Runtime.Gameplay.Common.Random
{
    [UsedImplicitly]
    internal sealed class RandomService : IRandomService
    {
        public int GetInRange(RangeInt rangeExclusive) =>
            GetInRange(rangeExclusive.Min, rangeExclusive.Max);

        public int GetInRangeInclusive(RangeInt rangeExclusive) =>
            GetInRange(rangeExclusive.Min, rangeExclusive.Max + 1);

        public float GetInRange(float minInclusive, float maxInclusive) =>
            UnityEngine.Random.Range(minInclusive, maxInclusive);

        public int GetInRange(int minInclusive, int maxExclusive) =>
            UnityEngine.Random.Range(minInclusive, maxExclusive);
        
        public bool Roll(float chance) => 
            GetInRange(0f, 1f) <= chance;
        
        public int Roll(int dice) => 
            GetInRange(1, dice + 1);

        public TElement GetRandomElementFromList<TElement>(List<TElement> elements) =>
            elements.ElementAt(GetInRange(0, elements.Count));

        public TElement GetRandomElement<TElement>(IEnumerable<TElement> elements) =>
            elements.ElementAt(GetInRange(0, elements.Count()));
    }
}