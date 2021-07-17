using System.Collections.Generic;

namespace Shared.Core.Extensions
{
    public static class EnumerableExtension
    {
        public static IEnumerable<T> AddRange<T>(this IEnumerable<T> me, IEnumerable<T> other) where T : class
        {
            foreach (var element in me)
            {
                yield return element;
            }

            if (other == null)
            {
                yield break;
            }

            foreach (var element in other)
            {
                yield return element;
            }
        }
    }
}