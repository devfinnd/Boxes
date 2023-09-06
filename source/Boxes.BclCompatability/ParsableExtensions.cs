using System.Globalization;
using System.Numerics;
using Boxes.Types;
using static Boxes.Preludes.MaybePrelude;

namespace Boxes.BclCompatability;

public static class ParsableExtensions
{
    public static Maybe<T> TryParse<T>(this string input) where T : INumber<T>
        => T.TryParse(input, NumberFormatInfo.CurrentInfo, out T? result) ? Some(result) : None<T>();

    public static Maybe<T> TryParse<T>(this string input, IFormatProvider? provider) where T : IParsable<T>
        => T.TryParse(input, provider, out T? result) ? Some(result) : None<T>();
}
