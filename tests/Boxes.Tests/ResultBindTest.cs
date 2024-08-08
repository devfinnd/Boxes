using Boxes.Operators;
using Boxes.Types;

namespace Boxes.Tests;

public sealed class ResultBindTest
{
    private static Exception _allocatedException;
    private const string _expectedFailureMessage = "Expected Failure";
    private const string _expectedSuccessMessage = "Expected Success";

    public ResultBindTest()
    {
        _allocatedException = new Exception();
    }

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
            .Bind(static _ => Failure<string>(_expectedFailureMessage, _allocatedException));

        Assert.Throws<Exception>(() => a.Unwrap());
    }

    [Fact]
    public void Result_Bind_ShouldChangeUnderlyingResultType()
    {
        var a = Success(15)
            .Bind(static _ => Success(_expectedSuccessMessage));

        Assert.IsAssignableFrom<Result<string>>(a);
        Assert.IsType<Success<string>>(a);
    }
}
