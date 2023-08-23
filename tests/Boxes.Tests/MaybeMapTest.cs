using Boxes.Options;

namespace Boxes.Tests;

public class MaybeMapTest
{
    [Theory]
    [InlineData(15, 30)]
    [InlineData(20, 40)]
    public void Maybe_MultiplyByTwo_ShouldWork(int input, int outcome)
    {
        Maybe<int> a = Some(input)
            .Map(static x => x * 2);

        Assert.Equal(outcome, a.Reduce(-17));
    }

    [Theory]
    [MemberData(nameof(ObjectData))]
    public void Maybe_TransformObject_ShouldWork(object input, object outcome)
    {
        Maybe<object> a = Some(input)
            .Map(_ => outcome);

        Assert.Equal(outcome, a.Reduce(-17));
    }

    public static IEnumerable<object[]> ObjectData()
    {
        yield return new object[]{ "SomeString", 15 };
        yield return new object[]{ DateTime.Now, DateTimeOffset.UtcNow };
    }
}
