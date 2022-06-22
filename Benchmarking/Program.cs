namespace Benchmarking
{
    using System;
    using BenchmarkDotNet.Running;
    using Serilog;

    internal class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .MinimumLevel.Verbose()
            .CreateLogger();

            Console.WriteLine("Start Benchmark");

            //_ = BenchmarkRunner.Run<IteratingArrayFors>();
            //_ = BenchmarkRunner.Run<IteratingArrays>();
            //_ = BenchmarkRunner.Run<IteratingLists>();
            _ = BenchmarkRunner.Run<IteratingDictionaries>();
            //_ = BenchmarkRunner.Run<IteratingHttpClientsGet>();
            //_ = BenchmarkRunner.Run<IteratingNestedDictionaries>();

            Console.WriteLine("Finish Benchmark");
        }
    }
}
