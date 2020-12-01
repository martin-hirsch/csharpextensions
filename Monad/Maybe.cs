 using System;

    public sealed class Maybe<T>
    {
        public bool HasItem { get; }

        private T Item { get; }

        public Maybe()
        {
            HasItem = false;
        }

        private Maybe(T item)
        {
            HasItem = item != null;
            Item = item;
        }

        public T GetValueOrFallback(T fallbackValue)
        {
            if (fallbackValue == null)
            {
                throw new ArgumentNullException(nameof(fallbackValue));
            }

            return HasItem ? Item : fallbackValue;
        }

        public T GetValueUnsafe()
        {
            return HasItem ? Item : throw new ArgumentNullException();
        }

        public static Maybe<T> Just(T value)
        {
            return new Maybe<T>(value);
        }

        public static Maybe<T> None()
        {
            return new Maybe<T>();
        }

        public static implicit operator Maybe<T>(T value)
        {
            return Just(value);
        }

        public Maybe<TResult> Select<TResult>(Func<T, TResult> selector)
        {
            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (HasItem)
            {
                return new Maybe<TResult>(selector(Item));
            }

            return new Maybe<TResult>();
        }
    }
