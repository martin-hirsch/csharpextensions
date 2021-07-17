namespace Shared.Core.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Shared.Core.Monad;
    using Shared.Core.Validation;

    public static class ResultExtension
    {
        public static Result<T, TRequest> AddOriginalObject<T, TRequest>(this Result<T> me, TRequest originalObject)
        {
            return Result<T, TRequest>.FromResult(me, originalObject);
        }

        public static IEnumerable<ValidierungsErgebnis<TRequest>> GetErrors<TResult, TRequest>(this IEnumerable<Result<TResult, TRequest>> me)
        {
            return me.Where(x => x.HasValue == false).Select(y => y.ValidierungsErgebnis);
        }

        public static IEnumerable<TResult> GetValues<TResult, TRequest>(this IEnumerable<Result<TResult, TRequest>> me)
        {
            return me.Where(x => x?.HasValue == true).Select(y => y.Value);
        }

        public static bool HasErrors(this IEnumerable<IHaveValidierungsErgebnis> me)
        {
            return me.Any(x => x.ValidierungsErgebnis.HasErrors);
        }

        public static Result<T> InvokeIfValid<TResult, T>(this Result<TResult> result, Func<TResult, T> mappingFunc) where T : class where TResult : class
        {
            return result.HasValue ? Result<T>.Ok(mappingFunc(result.Value)) : Result<T>.Error(result.ValidierungsErgebnis);
        }

        public static Result<T> InvokeIfValid<TResult, T>(this Result<TResult> result, Func<TResult, Result<T>> mappingFunc)
            where T : class
            where TResult : class
        {
            return result.HasValue ? mappingFunc(result.Value) : Result<T>.Error(result.ValidierungsErgebnis);
        }

        public static Result<T, TRequest> InvokeIfValid<TResult, TRequest, T>(this Result<TResult, TRequest> me, Func<TResult, T> mappingFunc)
        {
            return me.HasValue ? Result<T, TRequest>.Ok(mappingFunc(me.Value), me.ValidierungsErgebnis.ValidatedObject) : Result<T, TRequest>.Error(me.ValidierungsErgebnis);
        }

        public static Result<TResult> Merge<TResult>(this Result<TResult> result, Func<TResult, ValidierungsErgebnis> validationFunc)
            where TResult : class

        {
            var validationResult = new ValidierungsErgebnis();

            if (result.HasValue)
            {
                validationResult = validationFunc(result.Value);
            }

            return result.Merge(validationResult);
        }

        public static Result<TResult> Merge<TResult>(this Result<TResult> result, ValidierungsErgebnis validationResult)
        {
            var newValidationResult = new ValidierungsErgebnis();

            newValidationResult.Merge(result.ValidierungsErgebnis);
            newValidationResult.Merge(validationResult);

            if (newValidationResult.HasErrors)
            {
                return Result<TResult>.Error(newValidationResult);
            }

            return result;
        }

        public static Result<TResult, TRequest> Merge<TResult, TRequest>(this Result<TResult, TRequest> other, ValidierungsErgebnis validationResult)
        {
            var newValidationResult = new ValidierungsErgebnis();

            newValidationResult.Merge(other.ValidierungsErgebnis);
            newValidationResult.Merge(validationResult);

            if (newValidationResult.HasErrors)
            {
                return Result<TResult, TRequest>.Error(new ValidierungsErgebnis<TRequest>(other.ValidierungsErgebnis.ValidatedObject, newValidationResult));
            }

            return other;
        }

        public static ValidierungsErgebnis MergeValidierungsErgebnisse(this IEnumerable<IHaveValidierungsErgebnis> me)
        {
            var result = new ValidierungsErgebnis();

            foreach (var ergebnis in me)
            {
                result = result.Merge(ergebnis.ValidierungsErgebnis);
            }

            return result;
        }

        public static IEnumerable<Result<T, TRequest>> ValuesOfType<TResult, TRequest, T>(this IEnumerable<Result<TResult, TRequest>> me)
            where T : class, TResult
        {
            return me.Where(w => w.HasValue)
                .Where(x => x.InvokeIfValid(y => y is T).Value)
                .Select(z => z.InvokeIfValid(i => i as T));
        }

        public static IEnumerable<Result<T>> WithErrors<T>(this IEnumerable<Result<T>> me)
        {
            return me.Where(x => x != null).Where(x => x.HasErrors);
        }

        public static IEnumerable<Result<T, TRequest>> WithErrors<T, TRequest>(this IEnumerable<Result<T, TRequest>> me)
        {
            return me.Where(x => x != null).Where(x => x.HasErrors);
        }

        public static IEnumerable<Result<T>> WithoutErrors<T>(this IEnumerable<Result<T>> me)
        {
            return me.Where(x => x != null).Where(x => !x.HasErrors);
        }

        public static IEnumerable<Result<T, TRequest>> WithoutErrors<T, TRequest>(this IEnumerable<Result<T, TRequest>> me)
        {
            return me.Where(x => x != null).Where(x => !x.HasErrors);
        }
    }
}