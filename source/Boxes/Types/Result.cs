using System.Diagnostics.CodeAnalysis;

namespace Boxes.Types;

public abstract class Result<T>
{
    public abstract T Unwrap();
    public abstract T UnwrapOrRecover(Func<Exception, T> recover);
}

public sealed class Success<T> : Result<T>
{
    private readonly T _value;

    public Success(T value) => _value = value;

    public override T Unwrap() => _value;
    public override T UnwrapOrRecover(Func<Exception, T> recover) => _value;

    public static implicit operator Success<T>(T value) => new(value);
}

public sealed class Failure<T> : Result<T>
{
    private readonly Exception _exception;

    public Failure(Exception exception) => _exception = exception;

    [DoesNotReturn]
    public override T Unwrap() => throw _exception;
    public override T UnwrapOrRecover(Func<Exception, T> recover) => recover(_exception);
    public Failure<U> Forward<U>() => new(_exception);

    public static implicit operator Failure<T>(Exception exception) => new(exception);
}
