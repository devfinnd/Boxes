using Boxes.Types;
using static Boxes.Preludes.ResultPrelude;

namespace Boxes.Operators;

public static class ResultOperators
{
    public static Result<U> Map<T, U>(this Result<T> result, Func<T, U> func) => result switch
    {
        Success<T> success => Success(func(success.Unwrap())),
        Failure<T> fail => fail.Forward<U>(),
        _ => throw new InvalidOperationException($"{result.GetType().Name} is not a valid result subtype")
    };

    public static Result<U> Bind<T, U>(this Result<T> result, Func<T, Result<U>> func) => result switch
    {
        Success<T> success => func(success.Unwrap()),
        Failure<T> fail => fail.Forward<U>(),
        _ => throw new InvalidOperationException($"{result.GetType().Name} is not a valid result subtype")
    };
}
