// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AtleX.HaveIBeenPwned.Tests.Mocks;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests;
public class HaveIBeenPwnedClientTests_GetLatestBreachAsync
  : HaveIBeenPwnedClientTestsBase
{
  [Fact]
  public async Task GetLatestBreachAsync__Succeeds()
  {
    using var httpClient = new HttpClient(new MockHttpMessageHandler());
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    var result = await c.GetLatestBreachAsync();

    Assert.NotNull(result);
    Assert.IsType<SiteBreach>(result);
  }

  [Fact]
  public async Task GetLatestBreachAsync_CancellationToken_Succeeds()
  {
    using var cancellationTokenSource = new CancellationTokenSource();
    using var httpClient = new HttpClient(new MockHttpMessageHandler());
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    var result = await c.GetLatestBreachAsync(cancellationTokenSource.Token);

    Assert.NotNull(result);
    Assert.IsType<SiteBreach>(result);
  }

  [Fact]
  public async Task GetLatestBreachAsync_AfterDispose_Throws()
  {
    using var httpClient = new HttpClient(new MockHttpMessageHandler());

    var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);
    c.Dispose();

    await Assert.ThrowsAsync<ObjectDisposedException>(() => c.GetLatestBreachAsync());
  }

  [Fact]
  public async Task GetLatestBreachAsync_CancellationToken_AfterDispose_Throws()
  {
    using var httpClient = new HttpClient(new MockHttpMessageHandler());

    var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);
    c.Dispose();

    await Assert.ThrowsAsync<ObjectDisposedException>(() => c.GetLatestBreachAsync(CancellationToken.None));
  }

  [Fact]
  public async Task GetLatestBreachAsync_NotFound_ThrowsHaveIBeenPwnedClientException()
  {
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 404));
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<HaveIBeenPwnedClientException>(() => c.GetLatestBreachAsync());
  }

  [Fact]
  public async Task GetLatestBreachAsync_CancellationToken_NotFound_ThrowsHaveIBeenPwnedClientException()
  {
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 404));
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<HaveIBeenPwnedClientException>(() => c.GetLatestBreachAsync(CancellationToken.None));
  }

  [Fact]
  public async Task GetLatestBreachAsync_RateLimitExceeded_ThrowsRateLimitExceededException()
  {
    using var cancellationTokenSource = new CancellationTokenSource();
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 429));
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<RateLimitExceededException>(() => c.GetLatestBreachAsync());
  }

  [Fact]
  public async Task GetLatestBreachAsync_CancellationToken_RateLimitExceeded_ThrowsRateLimitExceededException()
  {
    using var cancellationTokenSource = new CancellationTokenSource();
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 429));
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<RateLimitExceededException>(() => c.GetLatestBreachAsync(CancellationToken.None));
  }

  [Fact]
  public async Task GetLatestBreachAsync_WithImATeapot_Throws()
  {
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 418));
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<HaveIBeenPwnedClientException>(() => c.GetLatestBreachAsync());
  }

  [Fact]
  public async Task GetLatestBreachAsync_CancellationToken_WithImATeapot_Throws()
  {
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 418));
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<HaveIBeenPwnedClientException>(() => c.GetLatestBreachAsync(CancellationToken.None));
  }

  [Fact]
  public async Task GetLatestBreachAsync_CancellationToken_WithCancellationRequested_Throws()
  {
    using var cts = new CancellationTokenSource();

    using var httpClient = new HttpClient(new MockHttpMessageHandler());
    var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    cts.Cancel();

    await Assert.ThrowsAsync<OperationCanceledException>(() => c.GetLatestBreachAsync(cts.Token));
  }
}
