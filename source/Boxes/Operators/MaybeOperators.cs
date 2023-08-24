using Boxes.Types;
using static Boxes.Preludes.MaybePrelude;

namespace Boxes.Operators;

public static class MaybeOperators
{
    public static Maybe<U> Map<T, U>(this Maybe<T> maybe, Func<T, U> mapper) where T : notnull where U : notnull => maybe switch
    {
        Some<T> some => Some(mapper(some.Unwrap())),
        None<T> => None<U>(),
        _ => throw new InvalidOperationException($"{maybe.GetType().Name} is not a valid maybe subtype")
    };

    public static Maybe<U> Bind<T, U>(this Maybe<T> maybe, Func<T, Maybe<U>> binder) where T : notnull where U : notnull => maybe switch
    {
        Some<T> some => binder(some.Unwrap()),
        None<T> => None<U>(),
        _ => throw new InvalidOperationException($"{maybe.GetType().Name} is not a valid maybe subtype")
    };
}
