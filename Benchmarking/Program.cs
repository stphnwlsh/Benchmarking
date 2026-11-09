namespace Benchmarking
{
    using BenchmarkDotNet.Running;

    internal static class Program
    {
        /// <summary>
        /// Runs whichever benchmarks you ask for. With no arguments you get a menu.
        /// For example: dotnet run -c Release --project Benchmarking -- --filter '*Lists*'
        /// </summary>
        public static void Main(string[] args)
        {
            _ = BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        }
    }
}
