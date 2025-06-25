using System;
using System.Collections.Generic;
using System.Linq;

namespace Code.Runtime.Common.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable) =>
            enumerable switch
            {
                null => true,
                ICollection<T> collection => collection.Count == 0,
                _ => !enumerable.Any(),
            };

        public static bool IsNullOrEmpty(this string str) =>
            string.IsNullOrWhiteSpace(str);

        public static IEnumerable<T> Except<T>(this IEnumerable<T> enumerable, T toExcept) =>
            enumerable.Except(new[] { toExcept });

        public static T ElementAtOrFirst<T>(this T[] array, int index) =>
            index < array.Length ? array[index] : array[0];

        public static IEnumerable<T> OrEmpty<T>(this IEnumerable<T> self) =>
            self ?? Enumerable.Empty<T>();

        public static IEnumerable<T> NoNulls<T>(this IEnumerable<T> self) =>
            self.Where(element => element != null);

        public static IEnumerable<T> ForEachInReverse<T>(this List<T> list, Action<T> action)
        {
            for(int i = list.Count - 1; i >= 0; i--)
            {
                T item = list[i];
                action(item);
            }

            return list;
        }

        public static IList<T> Shuffle<T>(this IList<T> list)
        {
            var rng = new System.Random();
            int n = list.Count;
            while(n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }

            return list;
        }

        public static T FindMin<T, TComp>(this IEnumerable<T> enumerable, Func<T, TComp> selector)
            where TComp : IComparable<TComp> =>
            Find(enumerable, selector, true);

        public static T FindMax<T, TComp>(this IEnumerable<T> enumerable, Func<T, TComp> selector)
            where TComp : IComparable<TComp> =>
            Find(enumerable, selector, false);

        private static T Find<T, TComp>(IEnumerable<T> enumerable, Func<T, TComp> selector, bool selectMin)
            where TComp : IComparable<TComp>
        {
            if(enumerable == null)
                return default(T);

            bool first = true;
            T selected = default;
            TComp selectedComp = default;

            foreach(T current in enumerable)
            {
                TComp comp = selector(current);
                if(first)
                {
                    first = false;
                    selected = current;
                    selectedComp = comp;
                    continue;
                }

                int res = selectMin
                    ? comp.CompareTo(selectedComp)
                    : selectedComp.CompareTo(comp);

                if(res < 0)
                {
                    selected = current;
                    selectedComp = comp;
                }
            }

            return selected;
        }

        public static float? SumOrNull(this IEnumerable<float> numbers)
        {
            float? sum = null;
            foreach(float f in numbers)
                sum = f + (sum ?? 0);

            return sum;
        }
    }
}