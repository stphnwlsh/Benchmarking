|        Method |     N |         Mean |     StdDev | Rank |
|-------------- |------ |-------------:|-----------:|-----:|
|   SpanForEach |   100 |     66.38 ns |   1.064 ns |    1 |
|           For |   100 |     71.83 ns |   1.820 ns |    2 |
|       SpanFor |   100 |     80.74 ns |   3.200 ns |    3 |
| GetEnumerator |   100 |    115.47 ns |   2.386 ns |    4 |
|       ForEach |   100 |    116.48 ns |   2.829 ns |    4 |
|   ForEachLinq |   100 |    350.63 ns |   8.242 ns |    5 |
|   SpanForEach | 10000 |  5,598.75 ns | 156.974 ns |    6 |
|           For | 10000 |  5,705.86 ns |  79.050 ns |    6 |
|       SpanFor | 10000 |  6,710.47 ns | 123.148 ns |    7 |
|       ForEach | 10000 |  9,706.92 ns | 286.127 ns |    8 |
| GetEnumerator | 10000 |  9,743.42 ns | 214.322 ns |    8 |
|   ForEachLinq | 10000 | 35,479.62 ns | 908.056 ns |    9 |

|        Method |     N |         Mean |     StdDev | Rank |
|-------------- |------ |-------------:|-----------:|-----:|
|   SpanForEach |   100 |     49.05 ns |   0.933 ns |    1 |
|       SpanFor |   100 |     56.13 ns |   1.630 ns |    2 |
|           For |   100 |     76.01 ns |   2.835 ns |    3 |
|       ForEach |   100 |    114.61 ns |   3.451 ns |    4 |
| GetEnumerator |   100 |    116.83 ns |   2.207 ns |    4 |
|   ForEachLinq |   100 |    342.05 ns |  10.390 ns |    5 |
|       SpanFor | 10000 |  3,729.07 ns |  95.737 ns |    6 |
|   SpanForEach | 10000 |  3,823.33 ns | 133.230 ns |    6 |
|           For | 10000 |  5,695.28 ns | 168.587 ns |    7 |
|       ForEach | 10000 |  9,486.91 ns | 229.360 ns |    8 |
| GetEnumerator | 10000 |  9,632.17 ns | 224.108 ns |    8 |
|   ForEachLinq | 10000 | 36,448.07 ns | 589.032 ns |    9 |
