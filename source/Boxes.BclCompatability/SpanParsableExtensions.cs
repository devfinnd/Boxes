using System.Globalization;
using Boxes.Preludes;
using Boxes.Types;

namespace Boxes.BclCompatability;

public static class SpanParsableExtensions
{
    public static Maybe<T> TryParse<T>(this ReadOnlySpan<char> input) where T : ISpanParsable<T>
        => T.TryParse(input, NumberFormatInfo.CurrentInfo, out T? result) ? MaybePrelude.Some(result) : MaybePrelude.None<T>();

    public static Maybe<T> TryParse<T>(this ReadOnlySpan<char> input, IFormatProvider? provider) where T : ISpanParsable<T>
        => T.TryParse(input, provider, out T? result) ? MaybePrelude.Some(result) : MaybePrelude.None<T>();
}