namespace Benchmarking
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Diagnosers;

    [RankColumn]
    [MemoryDiagnoser]
    [MarkdownExporterAttribute.Default]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    public class IteratingDictionaries
    {
        [Params(100, 10000)]
        public int N;

        private readonly Dictionary<Guid, string> dictionary = new Dictionary<Guid, string>();

        [GlobalSetup]
        public void GlobalSetup()
        {
            Console.WriteLine("Start GlobalSetup");

            for (var a = 0; a <= this.N; a++)
            {
                this.dictionary.Add(Guid.NewGuid(), $"StringValue{a}");
            }

            Console.WriteLine("Finish GlobalSetup");
        }

        [Benchmark]
        public void ForEachKeyValuePair()
        {
            var benchmarkDictionary = this.dictionary;

            foreach (var entry in benchmarkDictionary)
            {
                if (entry.Key != Guid.Empty && entry.Value == "StringValue")
                {
                    Console.WriteLine(entry.Key + " : " + entry.Value);
                }
            }
        }

        [Benchmark]
        public void ForEachKey()
        {
            var benchmarkDictionary = this.dictionary;

            foreach (var key in benchmarkDictionary.Keys)
            {
                if (key != Guid.Empty && benchmarkDictionary[key] == "StringValue")
                {
                    Console.WriteLine(key + " : " + benchmarkDictionary[key]);
                }
            }
        }

        [Benchmark]
        public void ForEachTuple()
        {
            var benchmarkDictionary = this.dictionary;

            foreach (var (key, value) in benchmarkDictionary)
            {
                if (key != Guid.Empty && value == "StringValue")
                {
                    Console.WriteLine(key + " : " + value);
                }
            }
        }

        [Benchmark]
        public void ForEachKeyValue()
        {
            var benchmarkDictionary = this.dictionary;

            foreach ((var key, var value) in benchmarkDictionary)
            {
                if (key != Guid.Empty && value == "StringValue")
                {
                    Console.WriteLine(key + " : " + value);
                }
            }
        }

        [Benchmark]
        public void ForKeyValuePair()
        {
            var benchmarkDictionary = this.dictionary;
            var length = benchmarkDictionary.Count;

            for (var i = 0; i < length; i++)
            {
                var entry = benchmarkDictionary.ElementAt(i);

                if (entry.Key != Guid.Empty && entry.Value == "StringValue")
                {
                    Console.WriteLine(entry.Key + " : " + entry.Value);
                }
            }
        }

        [Benchmark]
        public void ParallelForKeyValuePair()
        {
            var benchmarkDictionary = this.dictionary;

            benchmarkDictionary.AsParallel()
                .ForAll(entry =>
                {
                    if (entry.Key != Guid.Empty && entry.Value == "StringValue")
                    {
                        Console.WriteLine(entry.Key + " : " + entry.Value);
                    }
                });
        }
    }
}
