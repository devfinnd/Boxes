namespace Boxes.Options;

public abstract class Maybe<T>
{
    public abstract T Reduce(T fallback);
}

public sealed class Some<T> : Maybe<T>
{
    internal T Value { get; }

    internal Some(T value)
    {
        Value = value;
    }

    public override T Reduce(T fallback) => Value;
}

public sealed class None<T> : Maybe<T>
{
    internal static readonly None<T> Instance = new();

    private None()
    {
    }

    public override T Reduce(T fallback) => fallback;
}
