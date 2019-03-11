using AtleX.HaveIBeenPwned.Benchmarks.Mocks;
using BenchmarkDotNet.Attributes;

namespace AtleX.HaveIBeenPwned.Benchmarks.Benchmarks
{
  public class HaveIBeenPwnedClientBenchmarks
    : IServiceClientBenchmarks
  {
    [GlobalSetup]
    public void GlobalSetup()
    {
      var mockServiceClient = new IServiceClientMock();

      this._client = new HaveIBeenPwnedClient(ClientSettings.Default, mockServiceClient);
    }
  }
}
