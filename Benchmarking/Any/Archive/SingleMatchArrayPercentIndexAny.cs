namespace Benchmarking.Any.Archive;

using System;
using System.Linq;
using BenchmarkDotNet.Attributes;

[RankColumn]
[MemoryDiagnoser]
[MarkdownExporterAttribute.Default]
[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
public class SingleMatchArrayPercentIndexAny
{
    [Params(10000)]
    public int N;

    private int[] onePercentArray;
    private int[] twoPercentArray;
    private int[] threePercentArray;
    private int[] fourPercentArray;
    private int[] fivePercentArray;
    private int[] sixPercentArray;
    private int[] sevenPercentArray;
    private int[] eightPercentArray;
    private int[] ninePercentArray;
    private int[] tenPercentArray;

    [GlobalSetup]
    public void Setup()
    {
        // Initialise Array
        this.onePercentArray = new int[this.N];
        this.twoPercentArray = new int[this.N];
        this.threePercentArray = new int[this.N];
        this.fourPercentArray = new int[this.N];
        this.fivePercentArray = new int[this.N];
        this.sixPercentArray = new int[this.N];
        this.sevenPercentArray = new int[this.N];
        this.eightPercentArray = new int[this.N];
        this.ninePercentArray = new int[this.N];
        this.tenPercentArray = new int[this.N];

        var random = new Random();

        // Fill Array with Random Numbers (0-99)
        for (var i = 0; i < this.N; i++)
        {
            this.onePercentArray[i] = random.Next(0, 99);
            this.twoPercentArray[i] = random.Next(0, 99);
            this.threePercentArray[i] = random.Next(0, 99);
            this.fourPercentArray[i] = random.Next(0, 99);
            this.fivePercentArray[i] = random.Next(0, 99);
            this.sixPercentArray[i] = random.Next(0, 99);
            this.sevenPercentArray[i] = random.Next(0, 99);
            this.eightPercentArray[i] = random.Next(0, 99);
            this.ninePercentArray[i] = random.Next(0, 99);
            this.tenPercentArray[i] = random.Next(0, 99);
        }

        // Set Single Match
        this.onePercentArray[this.N / 100 * 1] = 100;
        this.twoPercentArray[this.N / 100 * 2] = 100;
        this.threePercentArray[this.N / 100 * 3] = 100;
        this.fourPercentArray[this.N / 100 * 4] = 100;
        this.fivePercentArray[this.N / 100 * 5] = 100;
        this.sixPercentArray[this.N / 100 * 6] = 100;
        this.sevenPercentArray[this.N / 100 * 7] = 100;
        this.eightPercentArray[this.N / 100 * 8] = 100;
        this.ninePercentArray[this.N / 100 * 9] = 100;
        this.tenPercentArray[this.N / 100 * 10] = 100;
    }

    #region One Percent

    [Benchmark]
    public void WhereAny_OnePercent()
    {
        var benchmarkArray = this.onePercentArray;

        _ = benchmarkArray.Where(item => item > 99).Any();
    }

    [Benchmark]
    public void Any_OnePercent()
    {
        var benchmarkArray = this.onePercentArray;

        _ = benchmarkArray.Any(item => item > 99);
    }

    #endregion One Percent

    #region Two Percent

    [Benchmark]
    public void WhereAny_TwoPercent()
    {
        var benchmarkArray = this.twoPercentArray;

        _ = benchmarkArray.Where(item => item > 99).Any();
    }

    [Benchmark]
    public void Any_TwoPercent()
    {
        var benchmarkArray = this.twoPercentArray;

        _ = benchmarkArray.Any(item => item > 99);
    }

    #endregion Two Percent

    #region Three Percent

    [Benchmark]
    public void WhereAny_ThreePercent()
    {
        var benchmarkArray = this.threePercentArray;

        _ = benchmarkArray.Where(item => item > 99).Any();
    }

    [Benchmark]
    public void Any_ThreePercent()
    {
        var benchmarkArray = this.threePercentArray;

        _ = benchmarkArray.Any(item => item > 99);
    }

    #endregion Three Percent

    #region Four Percent

    [Benchmark]
    public void WhereAny_FourPercent()
    {
        var benchmarkArray = this.fourPercentArray;

        _ = benchmarkArray.Where(item => item > 99).Any();
    }

    [Benchmark]
    public void Any_FourPercent()
    {
        var benchmarkArray = this.fourPercentArray;

        _ = benchmarkArray.Any(item => item > 99);
    }

    #endregion Four Percent

    #region Five Percent

    [Benchmark]
    public void WhereAny_FivePercent()
    {
        var benchmarkArray = this.fivePercentArray;

        _ = benchmarkArray.Where(item => item > 99).Any();
    }

    [Benchmark]
    public void Any_FivePercent()
    {
        var benchmarkArray = this.fivePercentArray;

        _ = benchmarkArray.Any(item => item > 99);
    }

    #endregion Five Percent

    #region Six Percent

    [Benchmark]
    public void WhereAny_SixPercent()
    {
        var benchmarkArray = this.sixPercentArray;

        _ = benchmarkArray.Where(item => item > 99).Any();
    }

    [Benchmark]
    public void Any_SixPercent()
    {
        var benchmarkArray = this.sixPercentArray;

        _ = benchmarkArray.Any(item => item > 99);
    }

    #endregion Six Percent

    #region Seven Percent

    [Benchmark]
    public void WhereAny_SevenPercent()
    {
        var benchmarkArray = this.sevenPercentArray;

        _ = benchmarkArray.Where(item => item > 99).Any();
    }

    [Benchmark]
    public void Any_SevenPercent()
    {
        var benchmarkArray = this.sevenPercentArray;

        _ = benchmarkArray.Any(item => item > 99);
    }

    #endregion Seven Percent

    #region Eight Percent

    [Benchmark]
    public void WhereAny_EightPercent()
    {
        var benchmarkArray = this.eightPercentArray;

        _ = benchmarkArray.Where(item => item > 99).Any();
    }

    [Benchmark]
    public void Any_EightPercent()
    {
        var benchmarkArray = this.eightPercentArray;

        _ = benchmarkArray.Any(item => item > 99);
    }

    #endregion Eight Percent

    #region Nine Percent

    [Benchmark]
    public void WhereAny_NinePercent()
    {
        var benchmarkArray = this.ninePercentArray;

        _ = benchmarkArray.Where(item => item > 99).Any();
    }

    [Benchmark]
    public void Any_NinePercent()
    {
        var benchmarkArray = this.ninePercentArray;

        _ = benchmarkArray.Any(item => item > 99);
    }

    #endregion Nine Percent

    #region Ten Percent

    [Benchmark]
    public void WhereAny_TenPercent()
    {
        var benchmarkArray = this.tenPercentArray;

        _ = benchmarkArray.Where(item => item > 99).Any();
    }

    [Benchmark]
    public void Any_TenPercent()
    {
        var benchmarkArray = this.tenPercentArray;

        _ = benchmarkArray.Any(item => item > 99);
    }

    #endregion Ten Percent
}