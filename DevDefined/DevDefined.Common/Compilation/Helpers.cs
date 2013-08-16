using System;
using System.Collections.Generic;
using System.Linq;

namespace DevDefined.Common.Compilation
{
    public static class Helpers
    {
        public static T[] ToOrderedArray<T, TKey>(this IEnumerable<T> seq, Func<T, TKey> keySelector)
        {
            return seq.OrderBy(keySelector).ToArray();
        }

        public static IEnumerable<T> TakePage<T>(this IEnumerable<T> seq, int first, int last)
        {
            return seq.Skip(first).Take(last - first + 1);
        }

        public static int FirstDifferentDown<T, TV>(this T[] arr, int searchFrom, Func<T, TV> selector)
            where TV : IComparable<TV>
        {
            TV tvfrom = selector(arr[searchFrom]);
            for (int i = searchFrom - 1; i >= 0; --i)
                if (tvfrom.CompareTo(selector(arr[i])) != 0)
                    return i;

            // TODO: review just what exception should be thrown here.
            throw new Exception("No match");
        }
    }
}