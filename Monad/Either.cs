using System;

    public class Either<TLeft, TRight> where TLeft : class where TRight : class
    {
        public TLeft Left { get; }

        public TRight Right { get; }

        private Either(TLeft left, TRight right)
        {
            Right = right;
            Left = left;
        }

        public static Either<TLeft, TRight> CreateLeft(TLeft value)
        {
            return new Either<TLeft, TRight>(value, null);
        }

        public static Either<TLeft, TRight> CreateRight(TRight right)
        {
            return new Either<TLeft, TRight>(null, right);
        }

        public bool IsLeft()
        {
            return Left != null;
        }

        public bool IsRight()
        {
            return Right != null;
        }

        public Either<TLeft, TRight> Match(Func<TLeft, Either<TLeft, TRight>> leftFunc, Func<TRight, Either<TLeft, TRight>> rightFunc)
        {
            if (Left != null)
            {
                return leftFunc.Invoke(Left);
            }

            if (Right != null)
            {
                return rightFunc.Invoke(Right);
            }

            throw new NotSupportedException();
        }

        public static implicit operator Either<TLeft, TRight>(TLeft value)
        {
            return CreateLeft(value);
        }

        public static implicit operator Either<TLeft, TRight>(TRight right)
        {
            return CreateRight(right);
        }
    }
