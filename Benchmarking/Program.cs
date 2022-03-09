namespace Benchmarking
{
    using System;
    using System.Threading.Tasks;
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

            _ = BenchmarkRunner.Run<HttpGetRequests>();
            //_ = BenchmarkRunner.Run<DictionaryForEach>();
            //_ = BenchmarkRunner.Run<NestedDictionaryForEach>();

            Console.WriteLine("Finish Benchmark");
        }
    }
}
