using System.Diagnostics;
using Boxes.Types;
using static Boxes.Preludes.ResultPrelude;

namespace Boxes.Operators;

[DebuggerStepThrough]
public static class ResultOperators
{
    public static Result<R> Map<L, R>(this Result<L> result, Func<L, R> func) => result switch
    {
        Success<L> success => Success(func(success.Unwrap())),
        Failure<L> fail => fail.Forward<R>(),
        _ => throw new InvalidOperationException($"{result.GetType().Name} is not a valid result subtype")
    };

    public static Result<R> Bind<L, R>(this Result<L> result, Func<L, Result<R>> func) => result switch
    {
        Success<L> success => func(success.Unwrap()),
        Failure<L> fail => fail.Forward<R>(),
        _ => throw new InvalidOperationException($"{result.GetType().Name} is not a valid result subtype")
    };

    public static Maybe<R> Bind<L, R>(this Result<L> result, Func<L, Maybe<R>> func) where R : notnull => result switch
    {
        Success<L> success => func(success.Unwrap()),
        Failure<L> => None<R>.Instance,
        _ => throw new InvalidOperationException($"{result.GetType().Name} is not a valid result subtype")
    };
}
