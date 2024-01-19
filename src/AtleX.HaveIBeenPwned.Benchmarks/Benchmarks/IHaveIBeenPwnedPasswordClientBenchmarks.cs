using BenchmarkDotNet.Attributes;
using System.Threading.Tasks;

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
