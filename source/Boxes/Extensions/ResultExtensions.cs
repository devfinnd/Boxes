using System.Diagnostics;
using Boxes.Types;

namespace Boxes.Extensions;

[DebuggerStepThrough]
public static class ResultExtensions
{
    /// <summary>
    /// Unwraps the result by either returning the inner Maybe or returning None.
    /// </summary>
    /// <param name="result"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static Maybe<T> Flatten<T>(this Result<Maybe<T>> result) where T : notnull
        => result.UnwrapOrRecover(None<T>.Instance);
}
