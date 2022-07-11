using System;
using BenchmarkDotNet.Running;
using Array = Benchmarking.Any.Array;
using List = Benchmarking.Any.List;

Console.WriteLine("Start Benchmark");

_ = BenchmarkRunner.Run<Array.SinglePersonMatchAny>();
//_ = BenchmarkRunner.Run<Array.ManyPersonMatchAny>();

_ = BenchmarkRunner.Run<List.SinglePersonMatchAny>();
//_ = BenchmarkRunner.Run<List.ManyPersonMatchAny>();

Console.WriteLine("Finish Benchmark");
