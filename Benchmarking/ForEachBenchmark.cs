namespace Benchmarking
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Jobs;

    [SimpleJob(RuntimeMoniker.Net48)]
    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [SimpleJob(RuntimeMoniker.Net60)]
    [RPlotExporter]
    public class ForEachBenchmark
    {
        private Zero zero;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var zero = new Zero
            {
                Id = Guid.NewGuid()
            };

            for (var a = 0; a <= 10; a++)
            {
                var one = new One
                {
                    Id = Guid.NewGuid()
                };

                for (var b = 0; b <= 10; b++)
                {
                    var two = new Two
                    {
                        Id = Guid.NewGuid()
                    };

                    for (var c = 0; c <= 10; c++)
                    {
                        var three = new Three
                        {
                            Id = Guid.NewGuid()
                        };

                        for (var d = 0; d <= 10; d++)
                        {
                            three.Strings.Add(Guid.NewGuid(), $"StringValue{a}-{b}-{c}-{d}");
                        }

                        two.Threes.Add(Guid.NewGuid(), three);
                    }

                    one.Twos.Add(Guid.NewGuid(), two);
                }

                zero.Ones.Add(Guid.NewGuid(), one);
            }

            this.zero = zero;
        }

        [Benchmark]
        public int ForMethod()
        {
            var i = 0;

            var ones = this.zero.Ones.Values.ToArray();

            for (var a = 0; a < ones.Length - 1; a++)
            {
                var twos = ones[a].Twos.Values.ToArray();

                for (var b = 0; b < twos.Length - 1; b++)
                {
                    var threes = twos[b].Threes.Values.ToArray();

                    for (var c = 0; c < threes.Length - 1; c++)
                    {
                        var strings = threes[c].Strings.Values.ToArray();

                        for (var d = 0; d < strings.Length - 1; d++)
                        {
                            i++;
                        }
                    }
                }
            }

            return i;
        }

        [Benchmark]
        public int ForEachArrayMethod()
        {
            var i = 0;

            var ones = this.zero.Ones.Values.ToArray();

            foreach (var one in ones)
            {
                var twos = this.zero.Ones.Values.ToArray();

                foreach (var two in twos)
                {
                    var threes = this.zero.Ones.Values.ToArray();

                    foreach (var three in threes)
                    {
                        var strings = this.zero.Ones.Values.ToArray();

                        foreach (var value in strings)
                        {
                            i++;
                        }
                    }
                }
            }

            return i;
        }

        [Benchmark(Baseline = true)]
        public int ForEachMethod()
        {
            var i = 0;

            foreach (var one in this.zero.Ones.Values)
            {
                foreach (var two in one.Twos.Values)
                {
                    foreach (var three in two.Threes.Values)
                    {
                        foreach (var value in three.Strings.Values)
                        {
                            i++;
                        }
                    }
                }
            }

            return i;
        }

        [Benchmark]
        public int ForEachLessLookupsMethod()
        {
            var i = 0;

            var ones = this.zero.Ones.Values;

            foreach (var one in ones)
            {
                var twos = one.Twos.Values;

                foreach (var two in twos)
                {
                    var threes = two.Threes.Values;

                    foreach (var three in threes)
                    {
                        var values = three.Strings.Values;

                        foreach (var value in values)
                        {
                            i++;
                        }
                    }
                }
            }

            return i;
        }
        [Benchmark]
        public int ForEachLessLookupsStaticTypedMethod()
        {
            var i = 0;

            Dictionary<Guid, One>.ValueCollection ones = this.zero.Ones.Values;

            foreach (One one in ones)
            {
                Dictionary<Guid, Two>.ValueCollection twos = one.Twos.Values;

                foreach (Two two in twos)
                {
                    Dictionary<Guid, Three>.ValueCollection threes = two.Threes.Values;

                    foreach (Three three in threes)
                    {
                        Dictionary<Guid, string>.ValueCollection values = three.Strings.Values;

                        foreach (string value in values)
                        {
                            i++;
                        }
                    }
                }
            }

            return i;
        }

        [Benchmark]
        public int StaticTypedForEachMethod()
        {
            var i = 0;

            foreach (One one in this.zero.Ones.Values)
            {
                foreach (Two two in one.Twos.Values)
                {
                    foreach (Three three in two.Threes.Values)
                    {
                        foreach (string value in three.Strings.Values)
                        {
                            i++;
                        }
                    }
                }
            }

            return i;
        }

        //[Benchmark]
        //public int WhileMethod()
        //{
        //    var i = 0;

        //    var a = 0;
        //    var ones = this.zero.Ones.Values.ToArray();

        //    while (a < ones.Length)
        //    {
        //        var b = 0;
        //        var twos = ones[a].Twos.Values.ToArray();

        //        while (b < twos.Length)
        //        {
        //            var c = 0;
        //            var threes = twos[b].Threes.Values.ToArray();

        //            while (c < threes.Length)
        //            {
        //                var d = 0;
        //                var strings = threes[c].Strings.Values.ToArray();

        //                while (d < strings.Length)
        //                {
        //                    d++;
        //                    i++;
        //                }

        //                c++;
        //            }

        //            b++;
        //        }

        //        a++;
        //    }

        //    return i;
        //}

        //[Benchmark]
        //public int ForEachParallelMethod()
        //{
        //    var i = 0;

        //    foreach (var one in this.zero.Ones.Values.AsParallel())
        //    {
        //        foreach (var two in one.Twos.Values.AsParallel())
        //        {
        //            foreach (var three in two.Threes.Values.AsParallel())
        //            {
        //                foreach (var value in three.Strings.Values.AsParallel())
        //                {
        //                    i++;
        //                }
        //            }
        //        }
        //    }

        //    return i;
        ////}

        //[Benchmark]
        //public int LinqMethod()
        //{
        //    var i = 0;

        //    var values = this.zero.Ones.Values.SelectMany(o => o.Twos.Values.SelectMany(t => t.Threes.Values.SelectMany(th => th.Strings.Values)));

        //    foreach (var value in values)
        //    {
        //        i++;
        //    }

        //    return i;
        //}
    }

    public class Zero
    {
        public Guid Id { get; set; }
        public Dictionary<Guid, One> Ones { get; set; } = new Dictionary<Guid, One>();
    }

    public class One
    {
        public Guid Id { get; set; }
        public Dictionary<Guid, Two> Twos { get; set; } = new Dictionary<Guid, Two>();
    }

    public class Two
    {
        public Guid Id { get; set; }
        public Dictionary<Guid, Three> Threes { get; set; } = new Dictionary<Guid, Three>();
    }

    public class Three
    {
        public Guid Id { get; set; }
        public Dictionary<Guid, string> Strings { get; set; } = new Dictionary<Guid, string>();
    }
}
