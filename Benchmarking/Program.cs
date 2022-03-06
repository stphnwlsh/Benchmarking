using BenchmarkDotNet.Running;
using Benchmarking;

Console.WriteLine("Hello, World!");

BenchmarkRunner.Run<ForEachBenchmark>();