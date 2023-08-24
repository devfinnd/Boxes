namespace Boxes.Types;

public abstract class Maybe<T>
{
    public abstract T UnwrapOr(T defaultValue);
    public abstract T Unwrap();
}

public sealed class Some<T> : Maybe<T>
{
    private readonly T _value;

    internal Some(T value)
    {
        _value = value;
    }

    public override T Unwrap() => _value;
    public override T UnwrapOr(T defaultValue) => _value;
}

public sealed class None<T> : Maybe<T>
{
    internal static readonly None<T> Instance = new();

    private None() { }

    public override T Unwrap() => throw new InvalidOperationException($"Cannot unwrap None<{typeof(T).Name}>");
    public override T UnwrapOr(T defaultValue) => defaultValue;
}
