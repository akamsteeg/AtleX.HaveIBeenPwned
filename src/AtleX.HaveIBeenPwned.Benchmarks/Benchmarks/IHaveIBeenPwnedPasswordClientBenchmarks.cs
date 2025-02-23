// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace AtleX.HaveIBeenPwned.Benchmarks.Benchmarks;
public class IHaveIBeenPwnedPasswordClientBenchmarks
  : HaveIBeenPwnedClientBenchmarksBase
{
  [Benchmark]
  public async Task IsPwnedPasswordAsync()
  {
    var result = await this._client.IsPwnedPasswordAsync("benchmark");
  }
}
