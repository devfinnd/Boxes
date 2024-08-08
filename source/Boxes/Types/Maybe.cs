using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Boxes.Types;

[DebuggerStepThrough]
public abstract class Maybe<T> : IEquatable<Maybe<T>> where T : notnull
{
    /// <summary>
    /// Unwraps the value from the <see cref="Maybe{T}"/>.
    /// If the <see cref="Maybe{T}"/> is a <see cref="None{T}"/>, an exception is thrown.
    /// </summary>
    /// <returns>The unwrapped value</returns>
    /// <exception cref="InvalidOperationException">Thrown when trying to unwrap a <see cref="None{T}"/></exception>
    public abstract T Unwrap();

    /// <summary>
    /// Unwraps the value from the <see cref="Maybe{T}"/>.
    /// If the <see cref="Maybe{T}"/> is a <see cref="None{T}"/>, the provided value is returned.
    /// </summary>
    /// <param name="defaultValue">The value to return if the Maybe is a <see cref="None{T}"/></param>
    /// <returns>The unwrapped value</returns>
    public abstract T UnwrapOr(T defaultValue);

    /// <summary>
    /// Unwraps the value from the <see cref="Maybe{T}"/>.
    /// If the <see cref="Maybe{T}"/> is a <see cref="None{T}"/> the provided function is called.
    /// </summary>
    /// <param name="recover">The function to call</param>
    /// <returns>The unwrapped value</returns>
    public abstract T UnwrapOrRecover(Func<T> recover);

    #region Equality

    public bool Equals(Maybe<T>? other)
    {
        if (this is None<T> none && other is None<T>)
            return none.Equals(other);

        if (this is Some<T> some && other is Some<T>)
            return some.Equals(other);

        throw new InvalidOperationException($"{GetType().Name} is not a valid Maybe subtype");
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj is Maybe<T> maybe && Equals(maybe);
    }

    public override int GetHashCode() => this switch
    {
        Some<T> some => some.GetHashCode(),
        None<T> none => none.GetHashCode(),
        _ => throw new InvalidOperationException($"{GetType().Name} is not a valid Maybe subtype")
    };

    #endregion
}

[DebuggerStepThrough]
public sealed class Some<T> : Maybe<T>, IEquatable<Some<T>> where T : notnull
{
    private readonly T _value;

    internal Some(T value)
    {
        _value = value;
    }

    /// <summary>
    /// Unwraps the value from the <see cref="Some{T}"/>.
    /// </summary>
    /// <returns>The unwrapped value</returns>
    public override T Unwrap() => _value;

    /// <summary>
    /// Unwraps the value from the <see cref="Some{T}"/>.
    /// </summary>
    /// <param name="defaultValue">This will never be used for a <see cref="Some{T}"/></param>
    public override T UnwrapOr(T defaultValue) => _value;

    /// <summary>
    /// Unwraps the value from the <see cref="Some{T}"/>.
    /// </summary>
    /// <param name="recover">This will never be used for a <see cref="Some{T}"/></param>
    /// <returns>The unwrapped value</returns>
    public override T UnwrapOrRecover(Func<T> recover) => _value;

    #region Equality

    public bool Equals(Some<T>? other) => other is not null && Equals(_value, other._value);

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return ReferenceEquals(this, obj) || obj is Some<T> other && Equals(other);
    }

    public override int GetHashCode() => HashCode.Combine(GetType(), _value);

    #endregion
}

[DebuggerStepThrough]
public sealed class None<T> : Maybe<T>, IEquatable<None<T>> where T : notnull
{
    internal static readonly None<T> Instance = new();

    private None()
    {
    }

    /// <summary>
    /// Unwraps the value from the <see cref="None{T}"/>.
    /// </summary>
    /// <remarks>
    /// This method will always throw an <see cref="InvalidOperationException"/> as the <see cref="None{T}"/> has no value to return.
    /// </remarks>
    [DoesNotReturn]
    [ContractAnnotation("=> halt")]
    public override T Unwrap() => throw new InvalidOperationException($"Cannot unwrap None<{typeof(T).Name}>");

    /// <summary>
    /// Unwraps the value from the <see cref="None{T}"/>.
    /// </summary>
    /// <param name="defaultValue">The value to return</param>
    /// <remarks>
    /// This method will always return the <paramref name="defaultValue"/> as the <see cref="None{T}"/> has no value to return.
    /// </remarks>
    public override T UnwrapOr(T defaultValue) => defaultValue;

    /// <summary>
    /// Unwraps the value from the <see cref="None{T}"/>.
    /// </summary>
    /// <param name="recover">The function to call</param>
    /// <remarks>
    /// This method will always call the <paramref name="recover"/> function as the <see cref="None{T}"/> has no value to return.
    /// </remarks>
    public override T UnwrapOrRecover(Func<T> recover) => recover();

    #region Equality

    public bool Equals(None<T>? other) => other is not null;
    public override bool Equals(object? obj) => ReferenceEquals(this, obj) || obj is None<T> other && Equals(other);
    public override int GetHashCode() => GetType().GetHashCode();

    #endregion
}
