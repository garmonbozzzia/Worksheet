using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Utils
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

        public static IEnumerable<T> ListWrap<T>(this T x)
        {
            yield return x;
        }

        public static IEnumerable<T> UnwrapList<T>(this IEnumerable<IEnumerable<T>> xss)
        {
            return xss.SelectMany(xs => xs);
        }

        public static IEnumerable<T> Repeat<T>(this IEnumerable<T> xs, int repeat = int.MaxValue)
        {
            return Enumerable.Range(1, repeat).SelectMany(_ => xs);
        }

        public static void ForEach<T>(this IEnumerable<T> xs, Action<T> action)
        {
            foreach (var x in xs)
                action(x);
        }

        public static IEnumerable<TResult> Scanl<T, TResult>(
            this IEnumerable<T> source,
            TResult first,
            Func<TResult, T, TResult> combine)
        {
            using (var data = source.GetEnumerator())
            {
                yield return first;

                while (data.MoveNext())
                {
                    first = combine(first, data.Current);
                    yield return first;
                }
            }
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

        public static Func<T, R> Memoize<T, R>(this Func<T, R> f)
        {
            var d = new Dictionary<T, R>();
            return a =>
            {
                R r;
                if (!d.TryGetValue(a, out r))
                {
                    r = f(a);
                    d.Add(a, r);
                }
                return r;
            };
        } 

        public static IEnumerable<int> To(this int number, int to)
        {
            return to >= number
                ? Enumerable.Range(number, to - number + 1)
                : Enumerable.Range(to, number - to + 1).Select(x => number + to - x);
        }

        public static IEnumerable<int> To(this int number)
        {
            return Enumerable.Range(number, int.MaxValue);
        }
    }
}
