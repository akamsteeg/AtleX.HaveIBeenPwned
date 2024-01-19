using BenchmarkDotNet.Attributes;
using System.Threading.Tasks;

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
