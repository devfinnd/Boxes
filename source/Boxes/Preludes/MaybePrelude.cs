using Boxes.Types;
namespace Boxes.Preludes;

public static class MaybePrelude
{
    public static Some<T> Some<T>(T value) where T : notnull => new(value);
    public static None<T> None<T>() where T : notnull => Types.None<T>.Instance;
}
