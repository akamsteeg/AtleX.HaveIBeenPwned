using AtleX.HaveIBeenPwned.Benchmarks.Mocks;
using BenchmarkDotNet.Attributes;
using System.Net.Http;

namespace AtleX.HaveIBeenPwned.Benchmarks.Benchmarks;

public abstract class HaveIBeenPwnedClientBenchmarksBase
{
  protected IHaveIBeenPwnedClient _client;

  [GlobalSetup]
  public void GlobalSetup()
  {
    var mockMessageHandler = new MockHttpMessageHandler();

    var testHttpClient = new HttpClient(mockMessageHandler);

    var settings = new HaveIBeenPwnedClientSettings()
    {
      ApiKey = "DUMMYKEY",
      ApplicationName = "Benchmarks",
    };

    this._client = new HaveIBeenPwnedClient(settings, testHttpClient);
  }
}
