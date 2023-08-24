using Boxes.Types;

namespace Boxes.Preludes;

public static class ResultPrelude
{
    public static Success<T> Success<T>(T value) => new(value);
    public static Failure<T> Failure<T>(Exception exception) => new(exception);
    public static Func<T, Result<U>> Lift<T, U>(Func<T, U> func) => x => Try(() => func(x));
    public static Result<T> Try<T>(Func<T> func)
    {
        try
        {
            return Success(func());
        }
        catch (Exception exception)
        {
            return Failure<T>(exception);
        }
    }
}
