using System.Diagnostics;
using System.Runtime.CompilerServices;
using Boxes.Types;

namespace Boxes.Preludes;

[DebuggerStepThrough]
public static class MaybePrelude
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Maybe<T> Lift<T>(T? value) where T : notnull
        => value is not null ? Some(value) : None<T>();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Maybe<T> Some<T>(T value) where T : notnull
        => new Some<T>(value);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Maybe<T> None<T>() where T : notnull
        => Types.None<T>.Instance;
}
