using System.Diagnostics;
using Boxes.Types;

namespace Boxes.Preludes;

[DebuggerStepThrough]
public static class MaybePrelude
{
    public static Maybe<T> Some<T>(T value) where T : notnull
        => new Some<T>(value);
    public static Maybe<T> None<T>() where T : notnull
        => Types.None<T>.Instance;
}
