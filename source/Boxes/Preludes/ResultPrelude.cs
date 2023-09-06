using System.Diagnostics;
using Boxes.Types;

namespace Boxes.Preludes;

[DebuggerStepThrough]
public static class ResultPrelude
{
    public static Result<T> Success<T>(T value) => new Success<T>(value);
    public static Result<T> Failure<T>(Exception exception) => new Failure<T>(exception);
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
