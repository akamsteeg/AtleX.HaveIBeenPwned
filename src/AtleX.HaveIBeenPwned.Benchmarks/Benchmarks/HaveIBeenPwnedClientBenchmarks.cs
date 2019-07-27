using AtleX.HaveIBeenPwned.Benchmarks.Mocks;
using BenchmarkDotNet.Attributes;
using System.Net.Http;

namespace AtleX.HaveIBeenPwned.Benchmarks.Benchmarks
{
  public class HaveIBeenPwnedClientBenchmarks
    : IHaveIBeenPwnedClientBenchmarks
  {
    [GlobalSetup]
    public void GlobalSetup()
    {
      var mockMessageHandler = new MockHttpMessageHandler();

      var testHttpClient = new HttpClient(mockMessageHandler);

      this._client = new HaveIBeenPwnedClient(HaveIBeenPwnedClientSettings.Default, testHttpClient);
    }
  }
}
