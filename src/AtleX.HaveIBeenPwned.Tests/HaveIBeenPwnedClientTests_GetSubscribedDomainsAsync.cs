// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AtleX.HaveIBeenPwned.Tests.Mocks;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests;
public class HaveIBeenPwnedClientTests_GetSubscribedDomainsAsync
  : HaveIBeenPwnedClientTestsBase
{
  [Fact]
  public async Task GetSubscribedDomainsAsync_Succeeds()
  {
    using var httpClient = new HttpClient(new MockHttpMessageHandler());

    var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    var result = await c.GetSubscribedDomainsAsync();

    Assert.NotNull(result);
    Assert.NotEmpty(result);
  }

  [Fact]
  public async Task GetSubscribedDomainsAsync_CancellationToken_Succeeds()
  {
    using var httpClient = new HttpClient(new MockHttpMessageHandler());

    var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    var result = await c.GetSubscribedDomainsAsync(CancellationToken.None);

    Assert.NotNull(result);
    Assert.NotEmpty(result);
  }

  [Fact]
  public async Task GetSubscribedDomainsAsync_AfterDispose_Throws()
  {
    using var httpClient = new HttpClient(new MockHttpMessageHandler());

    var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);
    c.Dispose();

    await Assert.ThrowsAsync<ObjectDisposedException>(() => c.GetSubscribedDomainsAsync());
  }

  [Fact]
  public async Task GetSubscribedDomainsAsync_CancellationToken_AfterDispose_Throws()
  {
    using var httpClient = new HttpClient(new MockHttpMessageHandler());

    var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);
    c.Dispose();

    await Assert.ThrowsAsync<ObjectDisposedException>(() => c.GetSubscribedDomainsAsync(CancellationToken.None));
  }

  [Fact]
  public async Task GetSubscribedDomainsAsync_RateLimitExceeded_ThrowsRateLimitExceededException()
  {
    using var cancellationTokenSource = new CancellationTokenSource();
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 429));
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<RateLimitExceededException>(() => c.GetSubscribedDomainsAsync());
  }

  [Fact]
  public async Task GetSubscribedDomainsAsync_WithCancellationToken_RateLimitExceeded_ThrowsRateLimitExceededException()
  {
    using var cancellationTokenSource = new CancellationTokenSource();
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 429));
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<RateLimitExceededException>(() => c.GetSubscribedDomainsAsync(CancellationToken.None));
  }

  [Fact]
  public async Task GetSubscribedDomainsAsyncc_WithInvalidApiKey_Throws()
  {
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 401));

    var settings = this.ClientSettings;
    settings.ApiKey = string.Empty;

    using var c = new HaveIBeenPwnedClient(settings, httpClient);

    await Assert.ThrowsAsync<InvalidApiKeyException>(() => c.GetSubscribedDomainsAsync());
  }

  [Fact]
  public async Task GetSubscribedDomainsAsyncc_WithCancellationToken_WithInvalidApiKey_Throws()
  {
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 401));

    var settings = this.ClientSettings;
    settings.ApiKey = string.Empty;

    using var c = new HaveIBeenPwnedClient(settings, httpClient);

    await Assert.ThrowsAsync<InvalidApiKeyException>(() => c.GetSubscribedDomainsAsync(CancellationToken.None));
  }
}
