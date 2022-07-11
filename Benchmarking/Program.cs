namespace Benchmarking
{
    using System;
    using BenchmarkDotNet.Running;
    using Serilog;
    using Array = Any.Array;
    using List = Any.List;

    internal class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .MinimumLevel.Verbose()
            .CreateLogger();

            Console.WriteLine("Start Benchmark");

            _ = BenchmarkRunner.Run<Array.SinglePersonMatchAny>();
            _ = BenchmarkRunner.Run<Array.ManyPersonMatchAny>();

            _ = BenchmarkRunner.Run<List.SinglePersonMatchAny>();
            _ = BenchmarkRunner.Run<List.ManyPersonMatchAny>();

            //_ = BenchmarkRunner.Run<IteratingArrayFors>();
            //_ = BenchmarkRunner.Run<IteratingArrays>();
            //_ = BenchmarkRunner.Run<IteratingLists>();
            //_ = BenchmarkRunner.Run<IteratingDictionaries>();
            //_ = BenchmarkRunner.Run<IteratingHttpClientsGet>();
            //_ = BenchmarkRunner.Run<IteratingNestedDictionaries>();

            Console.WriteLine("Finish Benchmark");
        }
    }
}
