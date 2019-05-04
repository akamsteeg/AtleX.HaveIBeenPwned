using AtleX.HaveIBeenPwned.Benchmarks.Mocks;
using AtleX.HaveIBeenPwned.Communication.Http;
using BenchmarkDotNet.Attributes;
using System.Net.Http;

namespace AtleX.HaveIBeenPwned.Benchmarks.Benchmarks.Communication
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
