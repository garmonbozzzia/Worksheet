using System;
using System.Collections.Generic;
using System.Linq;

namespace HsEmulator
{
    public static class Extensions
    {
        public static IEnumerable<T> Cons<T>(this T x, IEnumerable<T> xs)
        {
            return x.ListWrap().Concat(xs);
        }

        public static IEnumerable<T> Concat<T>(this IEnumerable<IEnumerable<T>> xs)
        {
            return xs.SelectMany(x => x);
        }

        public static IEnumerable<T> ListWrap<T> (this T x)
        {
            yield return x;
        }

        public static IEnumerable<T> UnwrapList<T>(this IEnumerable<IEnumerable<T>> xss )
        {
            return xss.SelectMany(xs => xs);
        }

        public static IEnumerable<T> Repeat<T>(this IEnumerable<T> xs)
        {
            return Enumerable.Range(1,int.MaxValue).SelectMany(_=>xs);
        }

        public static void ForEach<T>(this IEnumerable<T> xs, Action<T> action)
        {
            foreach (var x in xs)
                action(x);
        }
    }
}