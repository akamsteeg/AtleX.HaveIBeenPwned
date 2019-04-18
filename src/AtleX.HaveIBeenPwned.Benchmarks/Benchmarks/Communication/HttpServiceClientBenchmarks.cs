using AtleX.HaveIBeenPwned.Benchmarks.Mocks;
using AtleX.HaveIBeenPwned.Communication.Http;
using BenchmarkDotNet.Attributes;
using System.Net.Http;

namespace AtleX.HaveIBeenPwned.Benchmarks.Benchmarks.Communication
{
  public class HttpServiceClientBenchmarks
    : IServiceClientBenchmarks
  {
    [GlobalSetup]
    public void GlobalSetup()
    {
      var mockMessageHandler = new MockHttpMessageHandler();

      var testHttpClient = new HttpClient(mockMessageHandler);

      var httpServiceClient = new HttpServiceClient(HttpClientSettings.Default, testHttpClient);

      this._client = new HaveIBeenPwnedClient(httpServiceClient);
    }
  }
}
