```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.7462/25H2/2025Update/HudsonValley2)
12th Gen Intel Core i9-12900KF 3.20GHz, 1 CPU, 24 logical and 16 physical cores
.NET SDK 10.0.101
  [Host]     : .NET 10.0.1 (10.0.1, 10.0.125.57005), X64 RyuJIT x86-64-v3
  Job-LDLMHG : .NET 10.0.1 (10.0.1, 10.0.125.57005), X64 RyuJIT x86-64-v3
  Job-IOAYXE : .NET 9.0.11 (9.0.11, 9.0.1125.51716), X64 RyuJIT x86-64-v3
  Job-GEUVPA : .NET Framework 4.8.1 (4.8.9323.0), X64 RyuJIT VectorSize=256

PowerPlanMode=00000000-0000-0000-0000-000000000000  IterationTime=250ms  MaxIterationCount=20  
MinIterationCount=15  WarmupCount=1  

```
| Method                                       | Runtime            | Count | Mean          | Error         | StdDev        | Median        | Min           | Max           | Ratio | RatioSD | Gen0    | Gen1    | Gen2    | Allocated | Alloc Ratio |
|--------------------------------------------- |------------------- |------ |--------------:|--------------:|--------------:|--------------:|--------------:|--------------:|------:|--------:|--------:|--------:|--------:|----------:|------------:|
| **&#39;Create from string sequence via WriteLines&#39;** | **.NET 10.0**          | **8**     |      **96.47 ns** |     **10.230 ns** |     **11.781 ns** |     **102.20 ns** |      **78.73 ns** |     **113.44 ns** |  **1.02** |    **0.18** |  **0.0336** |       **-** |       **-** |     **528 B** |        **1.00** |
| &#39;Create from int sequence via WriteLines&lt;T&gt;&#39; | .NET 10.0          | 8     |      78.84 ns |      2.944 ns |      3.023 ns |      77.62 ns |      75.04 ns |      84.69 ns |  0.83 |    0.11 |  0.0321 |       - |       - |     504 B |        0.95 |
|                                              |                    |       |               |               |               |               |               |               |       |         |         |         |         |           |             |
| &#39;Create from string sequence via WriteLines&#39; | .NET 9.0           | 8     |      97.60 ns |      2.546 ns |      2.724 ns |      97.07 ns |      93.33 ns |     103.88 ns |  1.00 |    0.04 |  0.0334 |       - |       - |     528 B |        1.00 |
| &#39;Create from int sequence via WriteLines&lt;T&gt;&#39; | .NET 9.0           | 8     |      83.47 ns |      2.513 ns |      2.689 ns |      82.97 ns |      79.37 ns |      88.24 ns |  0.86 |    0.04 |  0.0319 |       - |       - |     504 B |        0.95 |
|                                              |                    |       |               |               |               |               |               |               |       |         |         |         |         |           |             |
| &#39;Create from string sequence via WriteLines&#39; | .NET Framework 4.8 | 8     |     229.94 ns |      3.558 ns |      3.328 ns |     229.26 ns |     224.79 ns |     236.03 ns |  1.00 |    0.02 |  0.0833 |       - |       - |     530 B |        1.00 |
| &#39;Create from int sequence via WriteLines&lt;T&gt;&#39; | .NET Framework 4.8 | 8     |     400.36 ns |      6.012 ns |      5.330 ns |     400.88 ns |     392.75 ns |     409.86 ns |  1.74 |    0.03 |  0.1208 |       - |       - |     762 B |        1.44 |
|                                              |                    |       |               |               |               |               |               |               |       |         |         |         |         |           |             |
| **&#39;Create from string sequence via WriteLines&#39;** | **.NET 10.0**          | **256**   |   **1,237.83 ns** |     **43.074 ns** |     **44.234 ns** |   **1,229.18 ns** |   **1,174.89 ns** |   **1,343.54 ns** |  **1.00** |    **0.05** |  **0.8793** |  **0.0242** |       **-** |   **13792 B** |        **1.00** |
| &#39;Create from int sequence via WriteLines&lt;T&gt;&#39; | .NET 10.0          | 256   |   1,669.19 ns |     40.290 ns |     44.782 ns |   1,676.20 ns |   1,594.85 ns |   1,754.02 ns |  1.35 |    0.06 |  0.8391 |  0.0131 |       - |   13208 B |        0.96 |
|                                              |                    |       |               |               |               |               |               |               |       |         |         |         |         |           |             |
| &#39;Create from string sequence via WriteLines&#39; | .NET 9.0           | 256   |   1,526.52 ns |     61.280 ns |     65.569 ns |   1,510.14 ns |   1,442.55 ns |   1,684.80 ns |  1.00 |    0.06 |  0.8763 |  0.0234 |       - |   13792 B |        1.00 |
| &#39;Create from int sequence via WriteLines&lt;T&gt;&#39; | .NET 9.0           | 256   |   1,739.16 ns |     39.546 ns |     43.955 ns |   1,732.13 ns |   1,675.31 ns |   1,837.88 ns |  1.14 |    0.05 |  0.8371 |  0.0074 |       - |   13208 B |        0.96 |
|                                              |                    |       |               |               |               |               |               |               |       |         |         |         |         |           |             |
| &#39;Create from string sequence via WriteLines&#39; | .NET Framework 4.8 | 256   |   6,797.31 ns |     85.372 ns |     79.857 ns |   6,798.25 ns |   6,643.61 ns |   6,940.70 ns |  1.00 |    0.02 |  2.1749 |  0.0544 |       - |   13843 B |        1.00 |
| &#39;Create from int sequence via WriteLines&lt;T&gt;&#39; | .NET Framework 4.8 | 256   |  19,270.68 ns |  2,943.635 ns |  3,389.895 ns |  21,076.97 ns |  12,874.82 ns |  21,802.23 ns |  2.84 |    0.49 |  3.3662 |  0.0502 |       - |   21471 B |        1.55 |
|                                              |                    |       |               |               |               |               |               |               |       |         |         |         |         |           |             |
| **&#39;Create from string sequence via WriteLines&#39;** | **.NET 10.0**          | **4096**  |  **84,475.85 ns** |  **3,893.786 ns** |  **4,166.308 ns** |  **85,847.66 ns** |  **71,001.28 ns** |  **86,840.24 ns** |  **1.00** |    **0.07** | **27.7008** | **27.7008** | **27.7008** |  **185417 B** |        **1.00** |
| &#39;Create from int sequence via WriteLines&lt;T&gt;&#39; | .NET 10.0          | 4096  |  68,753.09 ns | 10,908.273 ns | 12,561.983 ns |  73,989.85 ns |  38,408.24 ns |  77,868.69 ns |  0.82 |    0.15 | 20.0777 |  3.2383 |       - |  316008 B |        1.70 |
|                                              |                    |       |               |               |               |               |               |               |       |         |         |         |         |           |             |
| &#39;Create from string sequence via WriteLines&#39; | .NET 9.0           | 4096  |  79,696.76 ns | 17,376.810 ns | 20,011.160 ns |  93,059.99 ns |  48,680.45 ns |  95,614.83 ns |  1.08 |    0.43 | 27.6403 | 27.6403 | 27.6403 |  185417 B |        1.00 |
| &#39;Create from int sequence via WriteLines&lt;T&gt;&#39; | .NET 9.0           | 4096  |  66,911.16 ns |  3,750.424 ns |  4,168.587 ns |  68,161.73 ns |  51,049.20 ns |  69,146.69 ns |  0.91 |    0.28 | 20.0000 |  3.2813 |       - |  316008 B |        1.70 |
|                                              |                    |       |               |               |               |               |               |               |       |         |         |         |         |           |             |
| &#39;Create from string sequence via WriteLines&#39; | .NET Framework 4.8 | 4096  | 151,901.64 ns | 31,650.656 ns | 36,448.942 ns | 126,745.90 ns | 120,254.35 ns | 216,739.30 ns |  1.05 |    0.33 | 27.5000 | 27.5000 | 27.5000 |  185727 B |        1.00 |
| &#39;Create from int sequence via WriteLines&lt;T&gt;&#39; | .NET Framework 4.8 | 4096  | 203,468.65 ns |  4,627.091 ns |  5,328.564 ns | 204,516.18 ns | 193,476.90 ns | 209,626.50 ns |  1.41 |    0.29 | 55.3797 |  7.1203 |       - |  351252 B |        1.89 |
