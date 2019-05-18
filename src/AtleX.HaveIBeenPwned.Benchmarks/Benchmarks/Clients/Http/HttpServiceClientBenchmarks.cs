using AtleX.HaveIBeenPwned.Benchmarks.Mocks;
using AtleX.HaveIBeenPwned.Clients.Http;
using BenchmarkDotNet.Attributes;
using System.Net.Http;

namespace AtleX.HaveIBeenPwned.Benchmarks.Benchmarks.Clients.Http
{
  public class HttpHaveIBeenPwnedClientBenchmarks
    : IHaveIBeenPwnedClientBenchmarks
  {
    [GlobalSetup]
    public void GlobalSetup()
    {
      var mockMessageHandler = new MockHttpMessageHandler();

      var testHttpClient = new HttpClient(mockMessageHandler);

      var HttpHaveIBeenPwnedClient = new HttpHaveIBeenPwnedClient(HttpHaveIBeenPwnedClientSettings.Default, testHttpClient);

      this._client = new HaveIBeenPwnedClient(HttpHaveIBeenPwnedClient);
    }
  }
}
