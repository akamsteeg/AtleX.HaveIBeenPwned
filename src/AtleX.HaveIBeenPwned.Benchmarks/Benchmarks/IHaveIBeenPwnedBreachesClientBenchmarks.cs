using BenchmarkDotNet.Attributes;
using System.Threading.Tasks;

namespace AtleX.HaveIBeenPwned.Benchmarks.Benchmarks;
public class IHaveIBeenPwnedBreachesClientBenchmarks
  : HaveIBeenPwnedClientBenchmarksBase
{
  [Benchmark]
  public async Task GetAllBreachesAsync()
  {
    var result = await this._client.GetAllBreachesAsync();
  }

  [Benchmark]
  public async Task GetLatestBreachAsync()
  {
    var result = await this._client.GetLatestBreachAsync();
  }

  [Benchmark]
  public async Task GetBreachesAsync()
  {
    var result = await this._client.GetBreachesAsync("benchmark");
  }

  [Benchmark]
  [Arguments(BreachMode.All)]
  [Arguments(BreachMode.ExcludeUnverified)]
  public async Task GetBreachesAsync_BreachMode(BreachMode breachMode)
  {
    var result = await this._client.GetBreachesAsync("benchmark", breachMode);
  }
}
