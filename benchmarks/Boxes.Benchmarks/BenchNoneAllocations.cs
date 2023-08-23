using BenchmarkDotNet.Attributes;
using Boxes.Options;
using static Boxes.MaybePrelude;


namespace Boxes.Benchmarks;

[MemoryDiagnoser]
public class BenchNoneAllocations
{
    private readonly Maybe<int> _some = Some(15);
    private readonly Maybe<int> _none = None<int>();
    private const int _fallback = -17;

    [Benchmark]
    public int MapSome() => _some.Map(Inplace).Reduce(_fallback);

    [Benchmark]
    public int MapSomeTwice() => _some.Map(Inplace).Map(Inplace).Reduce(_fallback);

    [Benchmark]
    public int MapNone() => _none.Map(Inplace).Reduce(_fallback);

    [Benchmark]
    public int MapNoneTwice() => _none.Map(Inplace).Map(Inplace).Reduce(_fallback);

    [Benchmark]
    public int BindSome() => _some.Bind(MaybeInplace).Reduce(_fallback);

    [Benchmark]
    public int BindSomeTwice() => _some.Bind(MaybeInplace).Bind(MaybeInplace).Reduce(_fallback);

    [Benchmark]
    public int BindNone() => _none.Bind(MaybeInplace).Reduce(_fallback);

    [Benchmark]
    public int BindNoneTwice() => _none.Bind(MaybeInplace).Bind(MaybeInplace).Reduce(_fallback);


    private static int Inplace(int input) => input;
    private static Maybe<int> MaybeInplace(int input) => Some(input);
}
