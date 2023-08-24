using Boxes.Operators;
using Boxes.Types;

namespace Boxes.Tests;

public sealed class ResultMapTest
{
    [Fact]
    public void Result_MapSuccess_ShouldWork()
    {
        Result<int> a = Success(15)
            .Map(static x => x * 2);

        Assert.Equal(30, a.Unwrap());
    }

    [Fact]
    public void Result_MapFailure_ShouldWorkShouldThrowOnUnwrap()
    {
        Result<int> a = Failure<int>(new Exception())
            .Map(static x => x * 2);

        Assert.Throws<Exception>(() => a.Unwrap());
    }

    [Fact]
    public void Result_Map_ShouldChangeUnderlyingResultType()
    {
        var a = Success(15)
            .Map(static _ => "Hello World!");

        Assert.IsAssignableFrom<Result<string>>(a);
        Assert.IsType<Success<string>>(a);
    }
}
