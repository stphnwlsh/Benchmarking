namespace Benchmarking
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BenchmarkDotNet.Attributes;

    [RankColumn]
    [MemoryDiagnoser]
    [MarkdownExporterAttribute.Default]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    public class IteratingDictionaries
    {
        [Params(100, 10000)]
        public int N;

        private readonly Dictionary<Guid, string> items = new Dictionary<Guid, string>();

        [GlobalSetup]
        public void GlobalSetup()
        {
            Console.WriteLine("Start GlobalSetup");

            for (var a = 0; a <= this.N; a++)
            {
                this.items.Add(Guid.NewGuid(), $"StringValue{a}");
            }

            Console.WriteLine("Finish GlobalSetup");
        }

        [Benchmark]
        public void ForEachKeyValuePair()
        {
            foreach (var entry in this.items)
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
            foreach (var key in this.items.Keys)
            {
                if (key != Guid.Empty && this.items[key] == "StringValue")
                {
                    Console.WriteLine(key + " : " + this.items[key]);
                }
            }
        }

        [Benchmark]
        public void ForEachTuple()
        {
            foreach (var (key, value) in this.items)
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
            foreach ((var key, var value) in this.items)
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
            for (var i = 0; i < this.items.Count; i++)
            {
                var entry = this.items.ElementAt(i);

                if (entry.Key != Guid.Empty && entry.Value == "StringValue")
                {
                    Console.WriteLine(entry.Key + " : " + entry.Value);
                }
            }
        }

        [Benchmark]
        public void ParallelForKeyValuePair()
        {
            this.items.AsParallel()
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
