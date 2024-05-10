```

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3447/23H2/2023Update/SunValley3)
12th Gen Intel Core i5-12500, 1 CPU, 12 logical and 6 physical cores
.NET SDK 8.0.204
  [Host]     : .NET 6.0.29 (6.0.2924.17105), X64 RyuJIT AVX2
  DefaultJob : .NET 6.0.29 (6.0.2924.17105), X64 RyuJIT AVX2


```
| Method                       | Mean       | Error    | StdDev   | Min        | Max        | Rank | Gen0    | Gen1    | Allocated |
|----------------------------- |-----------:|---------:|---------:|-----------:|-----------:|-----:|--------:|--------:|----------:|
| FindMinNetPrice_linqCast     | 1,100.9 μs | 19.53 μs | 16.31 μs | 1,080.1 μs | 1,135.8 μs |    2 | 58.5938 |       - |  546.6 KB |
| FindMinNetPrice_rowCondition |   772.0 μs |  3.35 μs |  2.97 μs |   767.1 μs |   776.9 μs |    1 | 43.9453 | 15.6250 | 404.64 KB |
