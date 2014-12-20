using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

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

        public static IEnumerable<T> Repeat<T>(this IEnumerable<T> xs, int repeat = int.MaxValue)
        {
            return Enumerable.Range(1,repeat).SelectMany(_=>xs);
        }

        public static void ForEach<T>(this IEnumerable<T> xs, Action<T> action)
        {
            foreach (var x in xs)
                action(x);
        }

        public static IEnumerable<T> Next<T>(this IEnumerable<T> xs, T x)
        {
            return xs.Concat(x.ListWrap());
        }

        public static IEnumerable<T> Next<T>(this T x, T y)
        {
            return x.ListWrap().Next(y);
        }

        public static IEnumerable<T> TakeWhileIncluding<T>(this IEnumerable<T> xs, Func<T, bool> p)
        {
            foreach (var x in xs)
            {
                yield return x;
                if (!p(x))
                    yield break;
            }
        }

        public static IEnumerable<int> To(this int number, int to)
        {
            return Enumerable.Range(number, to - number + 1);
        }
    }
}