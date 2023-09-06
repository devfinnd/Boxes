using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Boxes.Types;

[DebuggerStepThrough]
public abstract class Result<T> : IEquatable<Result<T>>
{
    public abstract T Unwrap();
    public abstract T UnwrapOrRecover(Func<Exception, T> recover);
    public abstract T UnwrapOrRecover(Func<T> recover);
    public abstract T UnwrapOrRecover(T recover);

    #region Equality

    public bool Equals(Result<T>? other)
    {
        if (this is Success<T> success && other is Success<T>)
            return success.Equals(other);

        if (this is Failure<T> failure && other is Failure<T>)
            return failure.Equals(other);

        throw new InvalidOperationException($"{GetType().Name} is not a valid Result subtype");
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj is Result<T> result && Equals(result);
    }

    public override int GetHashCode() => this switch
    {
        Success<T> success => success.GetHashCode(),
        Failure<T> failure => failure.GetHashCode(),
        _ => throw new InvalidOperationException($"{GetType().Name} is not a valid Result subtype")
    };

    #endregion
}

[DebuggerStepThrough]
public sealed class Success<T> : Result<T>, IEquatable<Success<T>>
{
    private readonly T _value;

    public Success(T value) => _value = value;

    public override T Unwrap() => _value;
    public override T UnwrapOrRecover(Func<Exception, T> recover) => _value;
    public override T UnwrapOrRecover(Func<T> recover) => _value;
    public override T UnwrapOrRecover(T recover) => _value;

    public static implicit operator Success<T>(T value) => new(value);

    #region Equality

    public bool Equals(Success<T>? other) => other is not null && EqualityComparer<T>.Default.Equals(_value, other._value);

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return ReferenceEquals(this, obj) || obj is Success<T> other && Equals(other);
    }

    public override int GetHashCode() => HashCode.Combine(GetType(), _value);

    #endregion
}

[DebuggerStepThrough]
public sealed class Failure<T> : Result<T>, IEquatable<Failure<T>>
{
    private readonly Exception _exception;

    public Failure(Exception exception) => _exception = exception;

    [DoesNotReturn]
    public override T Unwrap() => throw _exception;

    public override T UnwrapOrRecover(Func<Exception, T> recover) => recover(_exception);
    public override T UnwrapOrRecover(Func<T> recover) => recover();
    public override T UnwrapOrRecover(T recover) => recover;
    public Failure<U> Forward<U>() => new(_exception);

    public static implicit operator Failure<T>(Exception exception) => new(exception);

    #region Equality

    public bool Equals(Failure<T>? other) => other is not null && _exception.Equals(other._exception);

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return ReferenceEquals(this, obj) || obj is Failure<T> other && Equals(other);
    }

    public override int GetHashCode() => HashCode.Combine(GetType(), _exception);

    #endregion
}
