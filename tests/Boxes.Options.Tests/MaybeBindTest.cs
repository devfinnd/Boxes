namespace Boxes.Options.Tests;

public sealed class MaybeBindTest
{
    [Theory]
    [InlineData(15, 30)]
    public void Maybe_Bind_ShouldWork(int input, int outcome)
    {
        Maybe<int> a = Some(input)
            .Bind(static x => Some(x * 2));

        Assert.Equal(outcome, a.Reduce(-17));
    }
}
