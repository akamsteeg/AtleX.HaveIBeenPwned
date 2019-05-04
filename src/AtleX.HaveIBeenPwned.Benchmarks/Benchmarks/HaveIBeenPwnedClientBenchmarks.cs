using AtleX.HaveIBeenPwned.Benchmarks.Mocks;
using BenchmarkDotNet.Attributes;

namespace AtleX.HaveIBeenPwned.Benchmarks.Benchmarks
{
  public class HaveIBeenPwnedClientBenchmarks
    : IHaveIBeenPwnedClientBenchmarks
  {
    [GlobalSetup]
    public void GlobalSetup()
    {
      var mockServiceClient = new IHaveIBeenPwnedClientMock();

      this._client = new HaveIBeenPwnedClient(mockServiceClient);
    }
  }
}
