namespace Benchmarking
{
    using System;
    using BenchmarkDotNet.Running;

    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Start Benchmark");

            _ = BenchmarkRunner.Run<ForEachBenchmark>();

            Console.WriteLine("Finish Benchmark");
        }
    }
}
