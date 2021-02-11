``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.17763.1697 (1809/October2018Update/Redstone5)
AMD Ryzen 9 3900X, 1 CPU, 24 logical and 12 physical cores
.NET Core SDK=5.0.102
  [Host]      : .NET Core 3.1.11 (CoreCLR 4.700.20.56602, CoreFX 4.700.20.56604), X64 RyuJIT
  ServerForce : .NET Core 3.1.11 (CoreCLR 4.700.20.56602, CoreFX 4.700.20.56604), X64 RyuJIT
  RyuJitX64   : .NET Core 3.1.11 (CoreCLR 4.700.20.56602, CoreFX 4.700.20.56604), X64 RyuJIT
  Server      : .NET Core 3.1.11 (CoreCLR 4.700.20.56602, CoreFX 4.700.20.56604), X64 RyuJIT

Jit=RyuJit  Platform=X64  

```
|              Method |         Job | Force | Server | IterationCount | LaunchCount | WarmupCount |      Mean |    Error |   StdDev |    Median |      Gen 0 | Gen 1 | Gen 2 |   Allocated |
|-------------------- |------------ |------ |------- |--------------- |------------ |------------ |----------:|---------:|---------:|----------:|-----------:|------:|------:|------------:|
|  DeterministicArray | ServerForce |  True |   True |             15 |           2 |          10 |  13.97 ms | 0.035 ms | 0.051 ms |  13.96 ms |          - |     - |     - |           - |
| DeterministicStatic |   RyuJitX64 |  True |  False |        Default |     Default |     Default |  14.55 ms | 0.017 ms | 0.015 ms |  14.55 ms |          - |     - |     - |         2 B |
| DeterministicStatic | ServerForce |  True |   True |             15 |           2 |          10 |  15.77 ms | 0.038 ms | 0.056 ms |  15.74 ms |          - |     - |     - |       387 B |
|  DeterministicArray |   RyuJitX64 |  True |  False |        Default |     Default |     Default |  15.84 ms | 0.118 ms | 0.105 ms |  15.83 ms |          - |     - |     - |        18 B |
| DeterministicStatic |      Server | False |   True |             15 |           2 |          10 |  16.48 ms | 0.056 ms | 0.082 ms |  16.46 ms |          - |     - |     - |       394 B |
|        NativeStatic | ServerForce |  True |   True |             15 |           2 |          10 |  16.70 ms | 0.032 ms | 0.047 ms |  16.70 ms |          - |     - |     - |           - |
|         NativeArray | ServerForce |  True |   True |             15 |           2 |          10 |  17.07 ms | 0.043 ms | 0.063 ms |  17.07 ms |          - |     - |     - |           - |
|        NativeStatic |      Server | False |   True |             15 |           2 |          10 |  17.46 ms | 0.022 ms | 0.034 ms |  17.46 ms |          - |     - |     - |       395 B |
|        NativeStatic |   RyuJitX64 |  True |  False |        Default |     Default |     Default |  17.60 ms | 0.121 ms | 0.095 ms |  17.63 ms |          - |     - |     - |         3 B |
|         NativeArray |   RyuJitX64 |  True |  False |        Default |     Default |     Default |  17.93 ms | 0.102 ms | 0.095 ms |  17.93 ms |          - |     - |     - |        18 B |
|  DeterministicArray |      Server | False |   True |             15 |           2 |          10 |  29.65 ms | 0.048 ms | 0.070 ms |  29.63 ms |          - |     - |     - |       394 B |
|         NativeArray |      Server | False |   True |             15 |           2 |          10 |  31.81 ms | 0.268 ms | 0.384 ms |  31.74 ms |          - |     - |     - |       162 B |
|           MD5Static | ServerForce |  True |   True |             15 |           2 |          10 | 127.51 ms | 0.516 ms | 0.773 ms | 127.51 ms |   500.0000 |     - |     - | 117602188 B |
|           MD5Static |   RyuJitX64 |  True |  False |        Default |     Default |     Default | 129.41 ms | 2.507 ms | 2.345 ms | 129.13 ms | 14000.0000 |     - |     - | 117600310 B |
|            MD5Array | ServerForce |  True |   True |             15 |           2 |          10 | 134.52 ms | 0.332 ms | 0.486 ms | 134.40 ms |   500.0000 |     - |     - | 117600232 B |
|           MD5Static |      Server | False |   True |             15 |           2 |          10 | 134.61 ms | 0.545 ms | 0.798 ms | 134.54 ms |   500.0000 |     - |     - | 117602210 B |
|            MD5Array |      Server | False |   True |             15 |           2 |          10 | 141.42 ms | 0.289 ms | 0.433 ms | 141.48 ms |   500.0000 |     - |     - | 117602210 B |
|            MD5Array |   RyuJitX64 |  True |  False |        Default |     Default |     Default | 141.99 ms | 1.206 ms | 1.128 ms | 141.91 ms | 14000.0000 |     - |     - | 117600000 B |
