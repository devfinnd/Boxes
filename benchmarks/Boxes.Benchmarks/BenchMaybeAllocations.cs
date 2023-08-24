using BenchmarkDotNet.Attributes;
using Boxes.Operators;
using Boxes.Types;
using static Boxes.Preludes.MaybePrelude;


namespace Boxes.Benchmarks;

[MemoryDiagnoser]
public class BenchMaybeAllocations
{
    private readonly Maybe<int> _some = Some(15);
    private readonly Maybe<int> _none = None<int>();
    private const int _fallback = 0;

    [Benchmark]
    public int MapSome() => _some.Map(Inplace).Unwrap();

    [Benchmark]
    public int MapSomeTwice() => _some.Map(Inplace).Map(Inplace).Unwrap();

    [Benchmark]
    public int MapNone() => _none.Map(Inplace).UnwrapOr(_fallback);

    [Benchmark]
    public int MapNoneTwice() => _none.Map(Inplace).Map(Inplace).UnwrapOr(_fallback);

    [Benchmark]
    public int BindSome() => _some.Bind(MaybeInplace).Unwrap();

    [Benchmark]
    public int BindSomeTwice() => _some.Bind(MaybeInplace).Bind(MaybeInplace).Unwrap();

    [Benchmark]
    public int BindNone() => _none.Bind(MaybeInplace).UnwrapOr(_fallback);

    [Benchmark]
    public int BindNoneTwice() => _none.Bind(MaybeInplace).Bind(MaybeInplace).UnwrapOr(_fallback);


    private static int Inplace(int input) => input;
    private static Maybe<int> MaybeInplace(int input) => Some(input);
}
