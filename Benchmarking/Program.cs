namespace Benchmarking
{
    using System;
    using BenchmarkDotNet.Running;
    using Benchmarking.Any.Array;
    using Benchmarking.Any.List;
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

            _ = BenchmarkRunner.Run<FivePercentArray>();
            _ = BenchmarkRunner.Run<TenPercentArray>();
            _ = BenchmarkRunner.Run<TwentyFivePercentArray>();
            _ = BenchmarkRunner.Run<FiftyPercentArray>();
            _ = BenchmarkRunner.Run<SeventyFivePercentArray>();
            _ = BenchmarkRunner.Run<NinetyFivePercentArray>();
            _ = BenchmarkRunner.Run<NinetyFivePercentArray>();

            // _ = BenchmarkRunner.Run<FivePercentList>();
            // _ = BenchmarkRunner.Run<TenPercentList>();
            // _ = BenchmarkRunner.Run<TwentyFivePercentList>();
            // _ = BenchmarkRunner.Run<FiftyPercentList>();
            // _ = BenchmarkRunner.Run<SeventyFivePercentList>();
            // _ = BenchmarkRunner.Run<NinetyFivePercentList>();
            // _ = BenchmarkRunner.Run<NinetyFivePercentList>();

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
