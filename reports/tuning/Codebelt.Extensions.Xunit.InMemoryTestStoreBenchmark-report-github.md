```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.8737/25H2/2025Update/HudsonValley2)
12th Gen Intel Core i9-12900KF 3.20GHz, 1 CPU, 24 logical and 16 physical cores
.NET SDK 11.0.100-preview.4.26230.115
  [Host]     : .NET 10.0.10 (10.0.10, 10.0.1026.32716), X64 RyuJIT x86-64-v3
  Job-LDLMHG : .NET 10.0.10 (10.0.10, 10.0.1026.32716), X64 RyuJIT x86-64-v3
  Job-IOAYXE : .NET 9.0.18 (9.0.18, 9.0.1826.31522), X64 RyuJIT x86-64-v3
  Job-GEUVPA : .NET Framework 4.8.1 (4.8.9337.0), X64 RyuJIT VectorSize=256

PowerPlanMode=00000000-0000-0000-0000-000000000000  IterationTime=250ms  MaxIterationCount=20  
MinIterationCount=15  WarmupCount=1  

```
| Method                           | Runtime            | Count | Mean           | Error       | StdDev      | Median         | Min            | Max            | Ratio     | RatioSD | Gen0   | Allocated | Alloc Ratio |
|--------------------------------- |------------------- |------ |---------------:|------------:|------------:|---------------:|---------------:|---------------:|----------:|--------:|-------:|----------:|------------:|
| **&#39;Query all items (no predicate)&#39;** | **.NET 10.0**          | **8**     |             **NA** |          **NA** |          **NA** |             **NA** |             **NA** |             **NA** |         **?** |       **?** |     **NA** |        **NA** |           **?** |
| &#39;Query with simple predicate&#39;    | .NET 10.0          | 8     |             NA |          NA |          NA |             NA |             NA |             NA |         ? |       ? |     NA |        NA |           ? |
| &#39;Query with complex predicate&#39;   | .NET 10.0          | 8     |             NA |          NA |          NA |             NA |             NA |             NA |         ? |       ? |     NA |        NA |           ? |
| &#39;QueryFor&lt;T&gt; type filtering&#39;     | .NET 10.0          | 8     |             NA |          NA |          NA |             NA |             NA |             NA |         ? |       ? |     NA |        NA |           ? |
|                                  |                    |       |                |             |             |                |                |                |           |         |        |           |             |
| &#39;Query all items (no predicate)&#39; | .NET 9.0           | 8     |             NA |          NA |          NA |             NA |             NA |             NA |         ? |       ? |     NA |        NA |           ? |
| &#39;Query with simple predicate&#39;    | .NET 9.0           | 8     |             NA |          NA |          NA |             NA |             NA |             NA |         ? |       ? |     NA |        NA |           ? |
| &#39;Query with complex predicate&#39;   | .NET 9.0           | 8     |             NA |          NA |          NA |             NA |             NA |             NA |         ? |       ? |     NA |        NA |           ? |
| &#39;QueryFor&lt;T&gt; type filtering&#39;     | .NET 9.0           | 8     |             NA |          NA |          NA |             NA |             NA |             NA |         ? |       ? |     NA |        NA |           ? |
|                                  |                    |       |                |             |             |                |                |                |           |         |        |           |             |
| &#39;Query all items (no predicate)&#39; | .NET Framework 4.8 | 8     |             NA |          NA |          NA |             NA |             NA |             NA |         ? |       ? |     NA |        NA |           ? |
| &#39;Query with simple predicate&#39;    | .NET Framework 4.8 | 8     |             NA |          NA |          NA |             NA |             NA |             NA |         ? |       ? |     NA |        NA |           ? |
| &#39;Query with complex predicate&#39;   | .NET Framework 4.8 | 8     |             NA |          NA |          NA |             NA |             NA |             NA |         ? |       ? |     NA |        NA |           ? |
| &#39;QueryFor&lt;T&gt; type filtering&#39;     | .NET Framework 4.8 | 8     |             NA |          NA |          NA |             NA |             NA |             NA |         ? |       ? |     NA |        NA |           ? |
|                                  |                    |       |                |             |             |                |                |                |           |         |        |           |             |
| **&#39;Query all items (no predicate)&#39;** | **.NET 10.0**          | **256**   |       **1.627 ns** |   **0.0335 ns** |   **0.0313 ns** |       **1.618 ns** |       **1.579 ns** |       **1.681 ns** |      **1.00** |    **0.03** |      **-** |         **-** |          **NA** |
| &#39;Query with simple predicate&#39;    | .NET 10.0          | 256   |     370.240 ns |   4.6896 ns |   4.3867 ns |     371.476 ns |     361.327 ns |     374.606 ns |    227.69 |    4.97 | 0.0044 |      72 B |          NA |
| &#39;Query with complex predicate&#39;   | .NET 10.0          | 256   |   1,027.494 ns |  11.9711 ns |  11.1978 ns |   1,027.472 ns |   1,010.640 ns |   1,047.387 ns |    631.90 |   13.48 | 0.0041 |      72 B |          NA |
| &#39;QueryFor&lt;T&gt; type filtering&#39;     | .NET 10.0          | 256   |   1,821.002 ns |  34.0068 ns |  34.9225 ns |   1,812.402 ns |   1,780.635 ns |   1,909.142 ns |  1,119.90 |   29.45 | 0.0065 |     128 B |          NA |
|                                  |                    |       |                |             |             |                |                |                |           |         |        |           |             |
| &#39;Query all items (no predicate)&#39; | .NET 9.0           | 256   |       4.254 ns |   0.1418 ns |   0.1517 ns |       4.251 ns |       3.952 ns |       4.534 ns |      1.00 |    0.05 |      - |         - |          NA |
| &#39;Query with simple predicate&#39;    | .NET 9.0           | 256   |     163.440 ns |  16.7370 ns |  19.2743 ns |     170.365 ns |     108.291 ns |     174.026 ns |     38.47 |    4.63 | 0.0044 |      72 B |          NA |
| &#39;Query with complex predicate&#39;   | .NET 9.0           | 256   |   1,953.112 ns | 294.4965 ns | 339.1426 ns |   2,078.053 ns |     965.456 ns |   2,105.814 ns |    459.68 |   79.58 | 0.0038 |      72 B |          NA |
| &#39;QueryFor&lt;T&gt; type filtering&#39;     | .NET 9.0           | 256   |   1,506.509 ns | 254.1709 ns | 292.7036 ns |   1,610.852 ns |     781.795 ns |   1,697.025 ns |    354.57 |   68.40 | 0.0061 |     128 B |          NA |
|                                  |                    |       |                |             |             |                |                |                |           |         |        |           |             |
| &#39;Query all items (no predicate)&#39; | .NET Framework 4.8 | 256   |       6.288 ns |   0.6092 ns |   0.7015 ns |       6.471 ns |       4.809 ns |       7.168 ns |      1.01 |    0.17 |      - |         - |          NA |
| &#39;Query with simple predicate&#39;    | .NET Framework 4.8 | 256   |   1,520.466 ns |  16.8161 ns |  15.7298 ns |   1,513.067 ns |   1,505.083 ns |   1,553.218 ns |    245.16 |   31.23 | 0.0061 |      72 B |          NA |
| &#39;Query with complex predicate&#39;   | .NET Framework 4.8 | 256   |   8,953.345 ns | 110.8394 ns | 103.6792 ns |   8,944.943 ns |   8,799.230 ns |   9,203.066 ns |  1,443.64 |  184.05 |      - |      72 B |          NA |
| &#39;QueryFor&lt;T&gt; type filtering&#39;     | .NET Framework 4.8 | 256   |   3,182.021 ns |  33.6943 ns |  31.5176 ns |   3,171.303 ns |   3,140.645 ns |   3,245.264 ns |    513.07 |   65.34 | 0.0127 |     128 B |          NA |
|                                  |                    |       |                |             |             |                |                |                |           |         |        |           |             |
| **&#39;Query all items (no predicate)&#39;** | **.NET 10.0**          | **4096**  |       **1.661 ns** |   **0.0294 ns** |   **0.0275 ns** |       **1.666 ns** |       **1.618 ns** |       **1.703 ns** |      **1.00** |    **0.02** |      **-** |         **-** |          **NA** |
| &#39;Query with simple predicate&#39;    | .NET 10.0          | 4096  |   5,151.473 ns |  76.0325 ns |  71.1209 ns |   5,156.931 ns |   5,043.972 ns |   5,254.299 ns |  3,101.34 |   64.94 |      - |      72 B |          NA |
| &#39;Query with complex predicate&#39;   | .NET 10.0          | 4096  |  16,348.890 ns | 204.7805 ns | 191.5518 ns |  16,344.335 ns |  16,038.020 ns |  16,761.271 ns |  9,842.51 |  193.99 |      - |      72 B |          NA |
| &#39;QueryFor&lt;T&gt; type filtering&#39;     | .NET 10.0          | 4096  |  12,546.824 ns |  71.5945 ns |  66.9695 ns |  12,548.568 ns |  12,444.043 ns |  12,667.353 ns |  7,553.55 |  127.84 |      - |     128 B |          NA |
|                                  |                    |       |                |             |             |                |                |                |           |         |        |           |             |
| &#39;Query all items (no predicate)&#39; | .NET 9.0           | 4096  |       2.461 ns |   0.0327 ns |   0.0306 ns |       2.468 ns |       2.406 ns |       2.505 ns |      1.00 |    0.02 |      - |         - |          NA |
| &#39;Query with simple predicate&#39;    | .NET 9.0           | 4096  |   1,360.085 ns |  17.3262 ns |  15.3593 ns |   1,363.114 ns |   1,326.263 ns |   1,380.126 ns |    552.77 |    9.00 |      - |      72 B |          NA |
| &#39;Query with complex predicate&#39;   | .NET 9.0           | 4096  |  15,206.929 ns | 106.7522 ns |  99.8561 ns |  15,215.995 ns |  15,020.885 ns |  15,339.858 ns |  6,180.49 |   84.35 |      - |      72 B |          NA |
| &#39;QueryFor&lt;T&gt; type filtering&#39;     | .NET 9.0           | 4096  |  11,929.514 ns |  86.9984 ns |  81.3784 ns |  11,938.298 ns |  11,824.468 ns |  12,061.726 ns |  4,848.46 |   66.74 |      - |     128 B |          NA |
|                                  |                    |       |                |             |             |                |                |                |           |         |        |           |             |
| &#39;Query all items (no predicate)&#39; | .NET Framework 4.8 | 4096  |       4.751 ns |   0.0471 ns |   0.0440 ns |       4.744 ns |       4.686 ns |       4.825 ns |      1.00 |    0.01 |      - |         - |          NA |
| &#39;Query with simple predicate&#39;    | .NET Framework 4.8 | 4096  |  28,430.631 ns | 196.2767 ns | 183.5974 ns |  28,422.051 ns |  28,106.478 ns |  28,690.767 ns |  5,984.79 |   65.32 |      - |      72 B |          NA |
| &#39;Query with complex predicate&#39;   | .NET Framework 4.8 | 4096  | 144,379.325 ns | 965.6221 ns | 903.2435 ns | 144,364.236 ns | 142,714.120 ns | 145,878.646 ns | 30,392.59 |  328.36 |      - |      71 B |          NA |
| &#39;QueryFor&lt;T&gt; type filtering&#39;     | .NET Framework 4.8 | 4096  |  48,041.131 ns | 325.4594 ns | 288.5113 ns |  48,056.412 ns |  47,491.514 ns |  48,542.011 ns | 10,112.91 |  107.84 |      - |     128 B |          NA |

Benchmarks with issues:
  InMemoryTestStoreBenchmark.'Query all items (no predicate)': Job-LDLMHG(PowerPlanMode=00000000-0000-0000-0000-000000000000, Runtime=.NET 10.0, IterationTime=250ms, MaxIterationCount=20, MinIterationCount=15, WarmupCount=1) [Count=8]
  InMemoryTestStoreBenchmark.'Query with simple predicate': Job-LDLMHG(PowerPlanMode=00000000-0000-0000-0000-000000000000, Runtime=.NET 10.0, IterationTime=250ms, MaxIterationCount=20, MinIterationCount=15, WarmupCount=1) [Count=8]
  InMemoryTestStoreBenchmark.'Query with complex predicate': Job-LDLMHG(PowerPlanMode=00000000-0000-0000-0000-000000000000, Runtime=.NET 10.0, IterationTime=250ms, MaxIterationCount=20, MinIterationCount=15, WarmupCount=1) [Count=8]
  InMemoryTestStoreBenchmark.'QueryFor<T> type filtering': Job-LDLMHG(PowerPlanMode=00000000-0000-0000-0000-000000000000, Runtime=.NET 10.0, IterationTime=250ms, MaxIterationCount=20, MinIterationCount=15, WarmupCount=1) [Count=8]
  InMemoryTestStoreBenchmark.'Query all items (no predicate)': Job-IOAYXE(PowerPlanMode=00000000-0000-0000-0000-000000000000, Runtime=.NET 9.0, IterationTime=250ms, MaxIterationCount=20, MinIterationCount=15, WarmupCount=1) [Count=8]
  InMemoryTestStoreBenchmark.'Query with simple predicate': Job-IOAYXE(PowerPlanMode=00000000-0000-0000-0000-000000000000, Runtime=.NET 9.0, IterationTime=250ms, MaxIterationCount=20, MinIterationCount=15, WarmupCount=1) [Count=8]
  InMemoryTestStoreBenchmark.'Query with complex predicate': Job-IOAYXE(PowerPlanMode=00000000-0000-0000-0000-000000000000, Runtime=.NET 9.0, IterationTime=250ms, MaxIterationCount=20, MinIterationCount=15, WarmupCount=1) [Count=8]
  InMemoryTestStoreBenchmark.'QueryFor<T> type filtering': Job-IOAYXE(PowerPlanMode=00000000-0000-0000-0000-000000000000, Runtime=.NET 9.0, IterationTime=250ms, MaxIterationCount=20, MinIterationCount=15, WarmupCount=1) [Count=8]
  InMemoryTestStoreBenchmark.'Query all items (no predicate)': Job-GEUVPA(PowerPlanMode=00000000-0000-0000-0000-000000000000, Runtime=.NET Framework 4.8, IterationTime=250ms, MaxIterationCount=20, MinIterationCount=15, WarmupCount=1) [Count=8]
  InMemoryTestStoreBenchmark.'Query with simple predicate': Job-GEUVPA(PowerPlanMode=00000000-0000-0000-0000-000000000000, Runtime=.NET Framework 4.8, IterationTime=250ms, MaxIterationCount=20, MinIterationCount=15, WarmupCount=1) [Count=8]
  InMemoryTestStoreBenchmark.'Query with complex predicate': Job-GEUVPA(PowerPlanMode=00000000-0000-0000-0000-000000000000, Runtime=.NET Framework 4.8, IterationTime=250ms, MaxIterationCount=20, MinIterationCount=15, WarmupCount=1) [Count=8]
  InMemoryTestStoreBenchmark.'QueryFor<T> type filtering': Job-GEUVPA(PowerPlanMode=00000000-0000-0000-0000-000000000000, Runtime=.NET Framework 4.8, IterationTime=250ms, MaxIterationCount=20, MinIterationCount=15, WarmupCount=1) [Count=8]
