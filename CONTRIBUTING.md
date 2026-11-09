# Contributing

Thanks for stopping by. Bug reports, ideas, new suites and pull requests are all welcome, in roughly that order of how quickly they get a reply.

This is looked after in spare time by one person with a day job and a coffee habit, so replies are best effort. The fastest way to get something changed is to make it easy to say yes to.

## Before you start

- Be kind. The [code of conduct](CODE_OF_CONDUCT.md) says how.
- Search the [issues](https://github.com/stphnwlsh/Benchmarking/issues) first. Someone may have beaten you to it.
- For a new suite, open an issue with the question it answers first. Good questions make good benchmarks.
- Found a security problem? Don't open an issue. Follow the [security policy](SECURITY.md).

## Making a change

1. Fork the repository and branch from `main`. Name the branch `feature/`, `fix/` or `chore/` followed by a few words, like `feature/string-concatenation`.
1. Follow "Adding a benchmark" in the [README](README.md), and keep each suite to one question.
1. Build it and do a dry run, which is exactly what CI does:

    ```bash
    dotnet build Benchmarking.slnx -c Release
    dotnet run -c Release --no-build --project Benchmarking -- --job Dry --filter '*Iterating*' '*Random*'
    ```

1. Open the pull request against `main`, fill in the template, and paste your results table if you have one. Numbers make a much better argument than adjectives.

Every pull request is reviewed by the maintainer before it goes in. If a review takes a while, a polite nudge is fine.

## Saying thanks

If this settles an argument in your favour, you can [sponsor the work](https://github.com/sponsors/stphnwlsh).
