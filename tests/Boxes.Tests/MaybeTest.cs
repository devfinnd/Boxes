using FluentAssertions;

namespace Boxes.Tests;

public sealed class MaybeTest
{
    [Fact]
    public void None_Should_BeSingleton()
    {
        None<int>().Should().BeSameAs(None<int>());
        None<string>().Should().BeSameAs(None<string>());
        None<float>().Should().BeSameAs(None<float>());
    }

    [Fact]
    public void None_ForDifferentTypes_Should_NotBeSame()
    {
        None<int>().Should().NotBeSameAs(None<string>());
        None<string>().Should().NotBeSameAs(None<float>());
        None<float>().Should().NotBeSameAs(None<int>());
    }

    [Fact]
    public void Some_WithSameValue_Should_Equal()
    {
        Some(15).Should().BeEquivalentTo(Some(15));
    }

    [Fact]
    public void Some_WithDifferentValues_Should_NotEqual()
    {
        Some(18).Should().NotBeEquivalentTo(Some(15));
    }

    [Fact]
    public void None_Shoud_NotEqual_Some()
    {
        None<int>().Should().NotBeEquivalentTo(Some(15));
    }

    [Fact]
    public void None_Shoud_Equal_None()
    {
        None<int>().Should().BeEquivalentTo(None<int>());
    }

    [Fact]
    public void Some_WithSameValue_Should_HaveSameHashcode()
    {
        Some(15).GetHashCode().Should().Be(Some(15).GetHashCode());
    }

    [Fact]
    public void Some_WithDifferentValues_Should_HaveDifferentHashcodes()
    {
        Some(15).GetHashCode().Should().NotBe(Some(18).GetHashCode());
    }

    [Fact]
    public void None_ForSameType_Should_HaveSameHashCode()
    {
        None<int>().GetHashCode().Should().Be(None<int>().GetHashCode());
    }

    [Fact]
    public void Some_ForDifferentTypes_ShouldNot_HaveSameHashcode()
    {
        Some(15).GetHashCode().Should().NotBe(Some("15").GetHashCode());
    }

    [Fact]
    public void None_ForDifferentTypes_ShouldNot_HaveSameHashcode()
    {
        None<int>().GetHashCode().Should().NotBe(None<string>().GetHashCode());
    }
}
