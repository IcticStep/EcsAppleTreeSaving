using System;

namespace Code.Runtime.Common.Extensions
{
    internal static class FunctionalExtensions
    {
        public static T With<T>(this T self, Action<T> set)
        {
            set.Invoke(self);
            return self;
        }

        public static T With<T>(this T self, Action<T> apply, bool when)
        {
            if(when)
                apply?.Invoke(self);

            return self;
        }

        public static T WithIf<T>(this T self, bool condition, Action<T> apply)
        {
            if(condition)
                apply?.Invoke(self);

            return self;
        }
        
        public static T WithIf<T>(this T self, Predicate<T> condition, Action<T> apply)
        {
            if(condition.Invoke(self))
                apply?.Invoke(self);

            return self;
        }

        public static T With<T>(this T self, Action<T> apply, Predicate<T> when)
        {
            if(when.Invoke(self))
                apply?.Invoke(self);

            return self;
        }
    }
}