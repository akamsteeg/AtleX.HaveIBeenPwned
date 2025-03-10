// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

using System.Net.Http;
using AtleX.HaveIBeenPwned.Benchmarks.Mocks;
using BenchmarkDotNet.Attributes;

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
