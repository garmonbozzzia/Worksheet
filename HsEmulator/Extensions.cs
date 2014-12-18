using System.Collections.Generic;
using System.Linq;

namespace HsEmulator
{
    public static class Extensions
    {
        public static IEnumerable<T> Cons<T>(this T x, IEnumerable<T> xs)
        {
            return Enumerable.Repeat(x,1).Concat(xs);
        }

        public static IEnumerable<T> Concat<T>(this IEnumerable<IEnumerable<T>> xs)
        {
            return xs.SelectMany(x => x);
        }
    }
}