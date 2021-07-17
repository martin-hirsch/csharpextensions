namespace Shared.Core.Monad
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using Shared.Core.Validation;

    public class Result<TResult, TRequest>
    {
        public bool HasErrors => ValidierungsErgebnis.HasErrors;

        public bool HasValidationMessages => ValidierungsErgebnis.HasValidationMessages;

        public bool HasValue => Value != null;

        public bool IsOk => !HasValidationMessages;

        public IEnumerable<string> ValidationMessagesTexts => ValidierungsErgebnis.GetMessagesTexts();

        public ValidierungsErgebnis<TRequest> ValidierungsErgebnis { get; }

        public TResult Value { get; }

        private Result(TResult value, ValidierungsErgebnis<TRequest> validierungsErgebnis)
        {
            ValidierungsErgebnis = validierungsErgebnis;
            Value = value;
        }

        public static Result<TResult, TRequest> Empty(TRequest originalObject)
        {
            return new Result<TResult, TRequest>(default, new ValidierungsErgebnis<TRequest>(originalObject, new ValidierungsErgebnis()));
        }

        public static Result<TResult, TRequest> Error(ValidierungsErgebnis<TRequest> validationResult)
        {
            return new Result<TResult, TRequest>(default, validationResult);
        }

        public static Result<TResult, TRequest> FromResult(Result<TResult> result, TRequest originalObject)
        {
            return new Result<TResult, TRequest>(result.Value, new ValidierungsErgebnis<TRequest>(originalObject, new ValidierungsErgebnis()));
        }

        public static Result<TResult, TRequest> Ok(TResult value, TRequest originalObject)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return new Result<TResult, TRequest>(value, new ValidierungsErgebnis<TRequest>(originalObject, new ValidierungsErgebnis()));
        }

        public static Result<TResult, TRequest> Ok(TResult value, ValidierungsErgebnis<TRequest> validationResult)
        {
            return new Result<TResult, TRequest>(value, validationResult);
        }

        public static implicit operator Result<TResult>(Result<TResult, TRequest> me)
        {
            return me.HasErrors ? Result<TResult>.Error(me.ValidierungsErgebnis) : Result<TResult>.Ok(me.Value);
        }

        public void ThrowIfInvalid()
        {
            ValidierungsErgebnis.ThrowIfInvalid();
        }
    }

    [DebuggerDisplay("{DebuggerDisplay(), nq}")]
    public class Result<TResult> : IHaveValidierungsErgebnis
    {
        public bool HasErrors => ValidierungsErgebnis?.HasErrors ?? false;

        public bool HasValidationMessages => ValidierungsErgebnis?.HasValidationMessages ?? false;

        public bool HasValue => Value != null;

        public bool IsOk => !HasValidationMessages;

        public TResult Value { get; }

        private Result(TResult value, ValidierungsErgebnis validierungsErgebnis)
        {
            ValidierungsErgebnis = validierungsErgebnis;
            Value = value;
        }

        public ValidierungsErgebnis ValidierungsErgebnis { get; }

        public static Result<TResult> Error(IHaveValidierungsErgebnis validierungsErgebnis)
        {
            return new Result<TResult>(default, validierungsErgebnis.ValidierungsErgebnis);
        }

        public static Result<TResult> Error(string message)
        {
            var validation = new ValidierungsErgebnis();
            validation.AddError(message);
            return new Result<TResult>(default, validation);
        }

        public static Result<TResult> Error(ValidierungsErgebnis validation)
        {
            if (validation == null)
            {
                throw new ArgumentNullException(nameof(validation));
            }

            return new Result<TResult>(default, validation);
        }

        public static Result<TResult> Ok(TResult value)
        {
            return new Result<TResult>(value, new ValidierungsErgebnis());
        }

        public static implicit operator Result<TResult>(TResult value)
        {
            return Ok(value);
        }

        public static implicit operator Result<TResult>(ValidierungsErgebnis validationResult)
        {
            return Error(validationResult);
        }

        public void ThrowIfInvalid()
        {
            ValidierungsErgebnis.ThrowIfInvalid();
        }

        private string DebuggerDisplay()
        {
            if (HasValidationMessages)
            {
                return $@"Error: {string.Join(", ", ValidierungsErgebnis.GetMessageTexts())}";
            }

            if (HasValue)
            {
                return Value.ToString();
            }

            return ToString();
        }
    }
}