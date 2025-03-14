// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

using System;
using System.Collections;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AtleX.HaveIBeenPwned.Tests.Mocks;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests;

public class HaveIBeenPwnedClientTests_GetAllBreachesAsync
  : HaveIBeenPwnedClientTestsBase
{
  [Fact]
  public async Task GetAllBreachesAsync_Succeeds()
  {
    using var httpClient = new HttpClient(new MockHttpMessageHandler());
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    var result = await c.GetAllBreachesAsync();

    Assert.NotNull(result);
    Assert.NotEmpty(result);
  }

  [Fact]
  public async Task GetAllBreachesAsync_CancellationToken_Succeeds()
  {
    using var cancellationTokenSource = new CancellationTokenSource();
    using var httpClient = new HttpClient(new MockHttpMessageHandler());
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    var result = await c.GetAllBreachesAsync(cancellationTokenSource.Token);

    Assert.NotNull(result);
    Assert.NotEmpty(result);
  }

  [Fact]
  public async Task GetAllBreachesAsync_AfterDispose_Throws()
  {
    using var httpClient = new HttpClient(new MockHttpMessageHandler());

    var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);
    c.Dispose();

    await Assert.ThrowsAsync<ObjectDisposedException>(() => c.GetAllBreachesAsync());
  }

  [Fact]
  public async Task GetAllBreachesAsync_CancellationToken_AfterDispose_Throws()
  {
    using var httpClient = new HttpClient(new MockHttpMessageHandler());

    var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);
    c.Dispose();

    await Assert.ThrowsAsync<ObjectDisposedException>(() => c.GetAllBreachesAsync(CancellationToken.None));
  }

  [Fact]
  public async Task GetAllBreachesAsync_NotFound_ThrowsHaveIBeenPwnedClientException()
  {
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 404));
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<HaveIBeenPwnedClientException>(() => c.GetAllBreachesAsync());
  }

  [Fact]
  public async Task GetAllBreachesAsync_CancellationToken_NotFound_ThrowsHaveIBeenPwnedClientException()
  {
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 404));
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<HaveIBeenPwnedClientException>(() => c.GetAllBreachesAsync(CancellationToken.None));
  }

  [Fact]
  public async Task GetAllBreachesAsync_RateLimitExceeded_ThrowsRateLimitExceededException()
  {
    using var cancellationTokenSource = new CancellationTokenSource();
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 429));
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<RateLimitExceededException>(() => c.GetAllBreachesAsync());
  }

  [Fact]
  public async Task GetAllBreachesAsync_CancellationToken_RateLimitExceeded_ThrowsRateLimitExceededException()
  {
    using var cancellationTokenSource = new CancellationTokenSource();
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 429));
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<RateLimitExceededException>(() => c.GetAllBreachesAsync(CancellationToken.None));
  }

  [Fact]
  public async Task GetAllBreachesAsync_WithImATeapot_Throws()
  {
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 418));
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<HaveIBeenPwnedClientException>(() => c.GetAllBreachesAsync());
  }

  [Fact]
  public async Task GetAllBreachesAsync_CancellationToken_WithImATeapot_Throws()
  {
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 418));
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<HaveIBeenPwnedClientException>(() => c.GetAllBreachesAsync(CancellationToken.None));
  }

  [Fact]
  public async Task GetAllBreachesAsync_CancellationToken_WithCancellationRequested_Throws()
  {
    using var cts = new CancellationTokenSource();

    using var httpClient = new HttpClient(new MockHttpMessageHandler());
    var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    cts.Cancel();

    await Assert.ThrowsAsync<OperationCanceledException>(() => c.GetAllBreachesAsync(cts.Token));
  }
}
