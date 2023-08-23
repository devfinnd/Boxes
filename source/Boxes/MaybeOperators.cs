using Boxes.Types;
using static Boxes.MaybePrelude;

namespace Boxes;

public static class MaybeOperators
{
    public static Maybe<U> Map<T, U>(this Maybe<T> maybe, Func<T, U> mapper) where T : notnull where U : notnull => maybe switch
    {
        Some<T> some => Some(mapper(some.Value)),
        None<T> => None<U>(),
        _ => throw new InvalidOperationException()
    };

    public static Maybe<U> Bind<T, U>(this Maybe<T> maybe, Func<T, Maybe<U>> binder) where T : notnull where U : notnull => maybe switch
    {
        Some<T> some => binder(some.Value),
        None<T> => None<U>(),
        _ => throw new InvalidOperationException()
    };
}
