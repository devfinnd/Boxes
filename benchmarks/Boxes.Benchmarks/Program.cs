// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using Boxes.Benchmarks;

BenchmarkRunner.Run<BenchMaybeAllocations>();
