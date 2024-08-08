using System.Diagnostics;
using System.Runtime.CompilerServices;
using Boxes.Types;

namespace Boxes.Preludes;

[DebuggerStepThrough]
public static class ResultPrelude
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<T> Success<T>(T value) => new Success<T>(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<T> Failure<T>(Error error) => new Failure<T>(error);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<T> Failure<T>(string message) => new Failure<T>(new SimpleError(message));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<T> Failure<T>(string message, Exception exception) => new Failure<T>(new ExceptionalError(message, exception));
}
