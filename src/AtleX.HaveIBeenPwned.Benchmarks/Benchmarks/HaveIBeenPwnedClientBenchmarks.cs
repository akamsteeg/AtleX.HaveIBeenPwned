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

      var settings = new HaveIBeenPwnedClientSettings()
      {
        ApiKey = "DUMMYKEY",
        ApplicationName = "Unit.Tests",
      };

      this._client = new HaveIBeenPwnedClient(settings, testHttpClient);
    }
  }
}
