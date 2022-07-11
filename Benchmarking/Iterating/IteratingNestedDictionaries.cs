namespace Benchmarking.Iterating;

using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

[RankColumn]
[MemoryDiagnoser]
[MarkdownExporterAttribute.Default]
[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
public class IteratingNestedDictionaries
{
    [Params(10, 50)]
    public int N;

    private Zero zero;

    [GlobalSetup]
    public void GlobalSetup()
    {
        Console.WriteLine("Start GlobalSetup");

        var zero = new Zero
        {
            Id = Guid.NewGuid()
        };

        for (var a = 0; a <= this.N; a++)
        {
            var one = new One
            {
                Id = Guid.NewGuid()
            };

            for (var b = 0; b <= this.N; b++)
            {
                var two = new Two
                {
                    Id = Guid.NewGuid()
                };

                for (var c = 0; c <= this.N; c++)
                {
                    var three = new Three
                    {
                        Id = Guid.NewGuid()
                    };

                    for (var d = 0; d <= this.N; d++)
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

        Console.WriteLine("Finish GlobalSetup");
    }

    #region For

    [Benchmark]
    public int ForListMethod()
    {
        var i = 0;

        var ones = this.zero.Ones.Values.ToList();

        for (var a = 0; a < ones.Count - 1; a++)
        {
            var twos = ones[a].Twos.Values.ToList();

            for (var b = 0; b < twos.Count - 1; b++)
            {
                var threes = twos[b].Threes.Values.ToList();

                for (var c = 0; c < threes.Count - 1; c++)
                {
                    var strings = threes[c].Strings.Values.ToList();

                    for (var d = 0; d < strings.Count - 1; d++)
                    {
                        i++;
                    }
                }
            }
        }

        return i;
    }

    [Benchmark]
    public int ForArrayMethod()
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

    #endregion For

    #region ForEach

    [Benchmark(Baseline = true)]
    public int ForEachBaselineMethod()
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
    public int ForEachMethod()
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
                    var strings = three.Strings.Values;

                    foreach (var value in strings)
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
            var twos = one.Twos.Values.ToArray();

            foreach (var two in twos)
            {
                var threes = two.Threes.Values.ToArray();

                foreach (var three in threes)
                {
                    var strings = three.Strings.Values.ToArray();

                    foreach (var value in strings)
                    {
                        i++;
                    }
                }
            }
        }

        return i;
    }

    [Benchmark]
    public int ForEachListMethod()
    {
        var i = 0;

        var ones = this.zero.Ones.Values.ToList();

        foreach (var one in ones)
        {
            var twos = one.Twos.Values.ToList();

            foreach (var two in twos)
            {
                var threes = two.Threes.Values.ToList();

                foreach (var three in threes)
                {
                    var strings = three.Strings.Values.ToList();

                    foreach (var value in strings)
                    {
                        i++;
                    }
                }
            }
        }

        return i;
    }

    #endregion ForEach

    #region Linq

    [Benchmark]
    public int LinqForEachMethod()
    {
        var i = 0;

        this.zero.Ones.Values.ToList().ForEach(one => one.Twos.Values.ToList().ForEach(two => two.Threes.Values.ToList().ForEach(three => three.Strings.Values.ToList().ForEach(value => i++))));

        return i;
    }

    [Benchmark]
    public int LinqSelectManyMethod()
    {
        var i = 0;

        this.zero.Ones.Values.SelectMany(o => o.Twos.Values).SelectMany(t => t.Threes.Values).SelectMany(th => th.Strings.Values).ToList().ForEach(value => i++);

        return i;
    }

    #endregion Linq
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