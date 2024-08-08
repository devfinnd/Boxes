using BenchmarkDotNet.Attributes;
using Boxes.Operators;
using static Boxes.Preludes.MaybePrelude;


namespace Boxes.Benchmarks;

[MemoryDiagnoser]
public class InliningBenchmark
{
    private const string _value = "Hello, World!";

    [Benchmark]
    public string BenchSome() => Some(_value).Unwrap();

    [Benchmark]
    public string BenchNone() => None<string>().UnwrapOr(_value);

    [Benchmark]
    public string BenchSomeMap() => Some(_value).Map(Inplace).Unwrap();

    [Benchmark]
    public string BenchSomeMapInlined() => Some(_value).Map(Inplace).Unwrap();

    [Benchmark]
    public string BenchNoneMap() => None<string>().Map(Inplace).UnwrapOr(_value);

    [Benchmark]
    public string BenchNoneMapInlined() => None<string>().Map(Inplace).UnwrapOr(_value);

    private static T Inplace<T>(T input) => input;
}
