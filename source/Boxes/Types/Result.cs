using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Boxes.Exceptions;
using JetBrains.Annotations;

namespace Boxes.Types;

[DebuggerStepThrough]
public abstract class Result<T> : IEquatable<Result<T>>
{
    /// <summary>
    /// Unwraps the value from the Result. If the Result is a Failure, an exception is thrown.
    /// </summary>
    /// <exception cref="FailureUnwrapedException"></exception>
    /// <returns>The unwrapped value</returns>
    public abstract T Unwrap();

    /// <summary>
    /// Unwraps the value from the Result. If the Result is a Failure, the provided function is called with the error.
    /// </summary>
    /// <param name="recover">The function to call with the error</param>
    /// <returns>The unwrapped value</returns>
    public abstract T UnwrapOrRecover(Func<Error, T> recover);

    /// <summary>
    /// Unwraps the value from the Result. If the Result is a Failure, the provided function is called.
    /// </summary>
    /// <param name="recover">The function to call</param>
    /// <returns>The unwrapped value</returns>
    public abstract T UnwrapOrRecover(Func<T> recover);

    /// <summary>
    /// Unwraps the value from the Result. If the Result is a Failure, the provided value is returned.
    /// </summary>
    /// <param name="recover">The value to return</param>
    /// <returns>The unwrapped value</returns>
    public abstract T UnwrapOrRecover(T recover);

    #region Equality

    public bool Equals(Result<T>? other) => (this, other) switch
    {
        (Success<T> selfSuccess, Success<T> otherSuccess) => selfSuccess.Equals(otherSuccess),
        (Failure<T> selfFailure, Failure<T> otherFailure) => selfFailure.Equals(otherFailure),
        _ => false
    };

    public override bool Equals(object? other) => (this, other) switch
    {
        (null, _) => false,
        (_, null) => false,
        _ => Equals(other as Result<T>)
    };

    public override int GetHashCode() => this switch
    {
        Success<T> success => success.GetHashCode(),
        Failure<T> failure => failure.GetHashCode(),
        _ => throw new InvalidOperationException($"{GetType().Name} is not a valid Result subtype")
    };

    #endregion
}

[DebuggerStepThrough]
public sealed class Success<T>(T value) : Result<T>, IEquatable<Success<T>>
{
    private readonly T _value = value;

    public override T Unwrap() => _value;
    public override T UnwrapOrRecover(Func<Error, T> recover) => _value;
    public override T UnwrapOrRecover(Func<T> recover) => _value;
    public override T UnwrapOrRecover(T recover) => _value;

    public static implicit operator Success<T>(T value) => new(value);

    #region Equality

    public bool Equals(Success<T>? other) => other switch
    {
        null => false,
        _ => EqualityComparer<T>.Default.Equals(_value, other._value)
    };

    public override bool Equals(object? obj) => obj switch
    {
        Success<T> other => Equals(other),
        _ => false
    };

    public override int GetHashCode() => HashCode.Combine(GetType(), _value);

    #endregion
}

[DebuggerStepThrough]
public sealed class Failure<T>(Error error) : Result<T>, IEquatable<Failure<T>>
{
    private readonly Error _error = error;

    [DoesNotReturn]
    [ContractAnnotation("=> halt")]
    public override T Unwrap() => _error switch
    {
        SimpleError simple  => throw new FailureUnwrapedException(simple.Message),
        DetailedError detailed => throw new FailureUnwrapedException(detailed.Message, detailed.Data),
        ExceptionalError exceptional => throw new FailureUnwrapedException(exceptional.Message, exceptional.Exception),
        _ => throw new InvalidOperationException(_error.Message)
    };

    public override T UnwrapOrRecover(Func<Error, T> recover) => recover(_error);
    public override T UnwrapOrRecover(Func<T> recover) => recover();
    public override T UnwrapOrRecover(T recover) => recover;
    public Failure<TOther> Forward<TOther>() => new(_error);

    #region Equality

    public bool Equals(Failure<T>? other) => other is not null && ReferenceEquals(_error, other._error);

    public override bool Equals(object? obj) => obj switch
    {
        Failure<T> other => Equals(other),
        _ => false
    };

    public override int GetHashCode() => HashCode.Combine(GetType(), _error);

    #endregion
}
