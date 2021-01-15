using System.Collections.Generic;

    public static class HashHelper
    {
        public static int GetSequenceHashCode<TItem>(this IEnumerable<TItem> other)
        {
            const int B = 378551;
            var a = 63689;
            var hash = 0;

            if (other == null)
            {
                return hash;
            }
            
            unchecked // If it overflows then just wrap around
            {
                foreach (var obj in other)
                {
                    if (obj == null)
                    {
                        continue;
                    }

                    hash = hash * a + obj.GetHashCode();
                    a *= B;
                }
            }

            return hash;
        }
    }
