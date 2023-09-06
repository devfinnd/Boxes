using System.Diagnostics;

namespace Boxes.Types;

[DebuggerStepThrough]
public abstract class Maybe<T> : IEquatable<Maybe<T>> where T : notnull
{
    public abstract T UnwrapOr(T defaultValue);
    public abstract T Unwrap();

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

    public override T Unwrap() => _value;
    public override T UnwrapOr(T defaultValue) => _value;

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

    public override T Unwrap() => throw new InvalidOperationException($"Cannot unwrap None<{typeof(T).Name}>");
    public override T UnwrapOr(T defaultValue) => defaultValue;

    #region Equality

    public bool Equals(None<T>? other) => other is not null;
    public override bool Equals(object? obj) => ReferenceEquals(this, obj) || obj is None<T> other && Equals(other);
    public override int GetHashCode() => GetType().GetHashCode();

    #endregion
}
