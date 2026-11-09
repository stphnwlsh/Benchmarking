# Benchmarking

[![Build](https://github.com/stphnwlsh/Benchmarking/actions/workflows/build.yml/badge.svg)](https://github.com/stphnwlsh/Benchmarking/actions/workflows/build.yml)
[![License: MIT](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE.md)

Settling "which way is faster?" arguments in C# with [BenchmarkDotNet](https://benchmarkdotnet.org), so the next code review can be about something more interesting.

Every suite answers one question with real measurements on .NET 10, instead of a hunch, a blog post from 2014 or whoever argues the loudest.

## The suites

| Suite | The question |
| --- | --- |
| `IteratingArrays` | `for`, `foreach`, `Array.ForEach`, an enumerator or a span: which is the quickest way through an array? |
| `IteratingLists` | The same question for `List<T>`, where `List.ForEach` and `CollectionsMarshal.AsSpan` join the party. |
| `IteratingDictionaries` | Is it worth copying a dictionary's values to an array before looping, from 500 to 500,000 entries? |
| `IteratingNestedDictionaries` | Four levels of dictionaries deep: nested loops, arrays, lists, `ForEach` chains or LINQ `SelectMany`? |
| `GeneratingRandomNumbers` | Three thread-safe ways to get random numbers: `Random.Shared`, a lock or one `Random` per thread. |
| `HttpClientsGet` | `HttpClient`, Refit, Flurl and RestEase calling a real API. It's tagged `Network`, because it measures the internet as much as the code. |

## Running them

You need the [.NET 10 SDK](https://dotnet.microsoft.com/download). Benchmarks always run in Release, because timing a Debug build is like timing a sprinter in thongs.

```bash
# Pick from a menu
dotnet run -c Release --project Benchmarking

# Run one suite
dotnet run -c Release --project Benchmarking -- --filter '*Lists*'

# Run everything except the network one
dotnet run -c Release --project Benchmarking -- --filter '*Iterating*' '*Random*'

# Check they all work, quickly, without trusting the numbers
dotnet run -c Release --project Benchmarking -- --job Dry --filter '*'
```

Results land in `BenchmarkDotNet.Artifacts/results` as Markdown, HTML and CSV. Your numbers will differ from anyone else's, which is the point of running them yourself.

Close the browser and your music before you start. BenchmarkDotNet is thorough, but it can't make a laptop ignore 40 Chrome tabs.

## Running them on GitHub

The **Benchmark** workflow runs any filter you give it on a GitHub runner, adds the results table to the run summary and uploads the full reports. Open [Actions](https://github.com/stphnwlsh/Benchmarking/actions/workflows/benchmark.yml), choose **Run workflow**, and type a filter.

Shared runners are noisy neighbours, so compare results from the same run, not across runs.

The **Build** workflow builds everything and does a dry run of every suite on each pull request, so a broken benchmark never makes it to `main`.

## Adding a benchmark

1. Add a class to the `Benchmarking` folder, with a `[Benchmark]` method for each way of doing the thing.
1. Copy the attributes from an existing suite (`[MemoryDiagnoser]`, `[RankColumn]` and friends), so every report looks the same.
1. Return or discard (`_ =`) whatever each method works out, so the JIT can't skip the work and make it look free.
1. Put a line in the table above, as a question.

## Contributing

Bug reports, ideas and new suites are all welcome. [CONTRIBUTING.md](CONTRIBUTING.md) explains how, and the [code of conduct](CODE_OF_CONDUCT.md) explains how to be excellent to each other. Security problems go through the [security policy](SECURITY.md), not an issue.

If this settles an argument in your favour, you can [sponsor the work](https://github.com/sponsors/stphnwlsh). It also funds [Lean Mean Clean Apps](https://leanmeancleanapps.github.io), and the coffee.

## Licence

[MIT](LICENSE.md).
