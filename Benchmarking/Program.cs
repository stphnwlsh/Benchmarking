namespace Benchmarking
{
    using System;
    using BenchmarkDotNet.Running;
    using Benchmarking.Any.Array;
    using Benchmarking.Any.List;
    using Benchmarking.Any.ListObject;
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

            _ = BenchmarkRunner.Run<RandomMatchArrayAny>();
            _ = BenchmarkRunner.Run<AllMatchArrayAny>();
            _ = BenchmarkRunner.Run<NoMatchArrayAny>();
            _ = BenchmarkRunner.Run<SingleMatchArrayEarlyIndexAny>();
            _ = BenchmarkRunner.Run<SingleMatchArrayLateIndexAny>();

            _ = BenchmarkRunner.Run<RandomMatchListAny>();
            _ = BenchmarkRunner.Run<AllMatchListAny>();
            _ = BenchmarkRunner.Run<NoMatchListAny>();
            _ = BenchmarkRunner.Run<SingleMatchListEarlyIndexAny>();
            _ = BenchmarkRunner.Run<SingleMatchListLateIndexAny>();

            _ = BenchmarkRunner.Run<RandomMatchListObjectAny>();
            _ = BenchmarkRunner.Run<AllMatchListObjectAny>();
            _ = BenchmarkRunner.Run<NoMatchListObjectAny>();
            _ = BenchmarkRunner.Run<SingleMatchListObjectEarlyIndexAny>();
            _ = BenchmarkRunner.Run<SingleMatchListObjectLateIndexAny>();

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
