public readonly struct ID<T>
    {
        public static implicit operator Guid(ID<T> me)
        {
            return me.Value;
        }

        public Guid Value { get; }

        public Id(Guid value)
        {
            Value = value;
        }

        public override string ToString()
        {
	        return Value.ToString();
        }

        public static Id<T> NewID() => new Id<T>(Guid.NewGuid());
    }
