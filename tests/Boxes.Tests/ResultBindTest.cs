using Boxes.Operators;
using Boxes.Types;

namespace Boxes.Tests;

public sealed class ResultBindTest
{
    [Fact]
    public void Result_BindSuccess_ShouldWork()
    {
        Result<int> a = Success(15)
            .Bind(static x => Success(x * 2));

        Assert.Equal(30, a.Unwrap());
    }

    [Fact]
    public void Result_BindSuccessToFailure_ShouldThrowOnUnwrap()
    {
        var a = Success(15)
            .Bind(static _ => Failure<string>(new Exception()));

        Assert.Throws<Exception>(() => a.Unwrap());
    }

    [Fact]
    public void Result_Bind_ShouldChangeUnderlyingResultType()
    {
        var a = Success(15)
            .Bind(static _ => Success("Hello World!"));

        Assert.IsAssignableFrom<Result<string>>(a);
        Assert.IsType<Success<string>>(a);
    }

    private static string IntToString(int x) => x.ToString();
}
