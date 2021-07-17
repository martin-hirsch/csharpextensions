namespace Shared.Core.Misc
{
    public class ValueClass<T>
    {
        public T Value { get; private set; }

        /// <summary>
        ///     Prevents Base from being constructed.
        /// </summary>
        private ValueClass()
        {
        }

        protected ValueClass(T value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
