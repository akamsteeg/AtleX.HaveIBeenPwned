using BenchmarkDotNet.Attributes;
using System.Threading.Tasks;

namespace AtleX.HaveIBeenPwned.Benchmarks.Benchmarks;
public class IHaveIBeenPwnedDomainClientBenchmarks
  : HaveIBeenPwnedClientBenchmarksBase
{
  [Benchmark]
  public async Task GetBreachedDomainUsersAsync()
  {
    var result = await this._client.GetBreachedDomainUsersAsync("example.com");
  }
}
