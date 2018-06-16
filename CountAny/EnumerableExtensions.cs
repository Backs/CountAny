using System;
using System.Collections;
using System.Collections.Generic;

namespace CountAny
{
    public static class EnumerableExtensions
    {
        public static bool CustomAny<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (source is TSource[] array)
            {
                return array.Length != 0;
            }

            if (source is ICollection<TSource> collection)
            {
                return collection.Count != 0;
            }

            if (source is ICollection readOnlyCollection)
            {
                return readOnlyCollection.Count != 0;
            }

            using (var enumerator = source.GetEnumerator())
            {
                if (enumerator.MoveNext())
                    return true;
            }

            return false;
        }
    }
}
