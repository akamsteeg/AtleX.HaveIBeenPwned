using BenchmarkDotNet.Attributes;
using System.Threading.Tasks;

namespace AtleX.HaveIBeenPwned.Benchmarks.Benchmarks
{
  public abstract class IServiceClientBenchmarks
  {
    protected IHaveIBeenPwnedClient _client;

    [Benchmark]
    public async Task GetBreachesAsync()
    {
      var result = await this._client.GetBreachesAsync("benchmark");
    }

    [Benchmark]
    public async Task GetBreachesAsync_BreachMode()
    {
      var result = await this._client.GetBreachesAsync("benchmark", BreachMode.All);
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
