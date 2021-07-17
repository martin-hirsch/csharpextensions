using System;

namespace Shared.Core.Misc
{
    public static class ThrowIfNullHelper
    {
        public static T ThrowIfNull<T>(this T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value;
        }
    }
}