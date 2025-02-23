// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace AtleX.HaveIBeenPwned.Benchmarks.Benchmarks;
public class IHaveIBeenPwnedPastesClientBenchmarks
  : HaveIBeenPwnedClientBenchmarksBase
{
  [Benchmark]
  public async Task GetPastesAsync()
  {
    var result = await this._client.GetPastesAsync("benchmark");
  }
}
