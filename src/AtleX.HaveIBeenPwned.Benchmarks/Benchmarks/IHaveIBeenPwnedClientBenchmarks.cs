using BenchmarkDotNet.Attributes;
using System.Threading.Tasks;

namespace AtleX.HaveIBeenPwned.Benchmarks.Benchmarks
{
  public abstract class IHaveIBeenPwnedClientBenchmarks
  {
    protected IHaveIBeenPwnedClient _client;

    [Benchmark]
    public async Task GetAllBreachesAsync()
    {
      var result = await this._client.GetAllBreachesAsync();
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

    [Benchmark]
    public async Task GetPastesAsync()
    {
      var result = await this._client.GetPastesAsync("benchmark");
    }

    [Benchmark]
    public async Task IsPwnedPasswordAsync()
    {
      var result = await this._client.IsPwnedPasswordAsync("benchmark");
    }
  }
}
