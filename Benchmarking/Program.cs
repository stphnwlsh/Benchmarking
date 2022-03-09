namespace Benchmarking
{
    using System;
    using BenchmarkDotNet.Running;

    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Start Benchmark");

            //_ = BenchmarkRunner.Run<DictionaryForEach>();
            _ = BenchmarkRunner.Run<NestedDictionaryForEach>();

            Console.WriteLine("Finish Benchmark");
        }
    }
}
