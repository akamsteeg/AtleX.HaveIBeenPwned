// Copyright (c) Alex Kamsteeg (https://atlex.nl/)
// License: MIT (See LICENSE file)

using AtleX.HaveIBeenPwned.Tests.Mocks;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests;

public class HaveIBeenPwnedClientTests_GetPastesAsync
  : HaveIBeenPwnedClientTestsBase
{
  [Fact]
  public async Task GetPastesAsync_WithNullValueForEmailAddress_Throws()
  {
    using var httpClient = new HttpClient(new MockHttpMessageHandler());
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<ArgumentNullException>(() => c.GetPastesAsync(null));
  }

  [Fact]
  public async Task GetPastesAsync_CancellationToken_WithNullValueForEmailAddress_Throws()
  {
    using var httpClient = new HttpClient(new MockHttpMessageHandler());
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<ArgumentNullException>(() => c.GetPastesAsync(null, CancellationToken.None));
  }

  [Fact]
  public async Task GetPastesAsync_WithInvalidApiKey_Throws()
  {
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 401));
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<InvalidApiKeyException>(() => c.GetPastesAsync("DUMMY"));
  }

  [Fact]
  public async Task GetPastesAsync_WithhoutApiKey_Throws()
  {
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 401));

    var settings = this.ClientSettings;
    settings.ApiKey = string.Empty;

    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<InvalidApiKeyException>(() => c.GetPastesAsync("DUMMY"));
  }

  [Fact]
  public async Task GetPastesAsync_CancellationToken_WithInvalidApiKey_Throws()
  {
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 401));
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<InvalidApiKeyException>(() => c.GetPastesAsync("DUMMY", CancellationToken.None));
  }

  [Fact]
  public async Task GetPastesAsync_AfterDispose_Throws()
  {
    using var httpClient = new HttpClient(new MockHttpMessageHandler());

    var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);
    c.Dispose();

    await Assert.ThrowsAsync<ObjectDisposedException>(() => c.GetPastesAsync("DUMMY"));
  }

  [Fact]
  public async Task GetPastesAsync_CancellationToken_AfterDispose_Throws()
  {
    using var httpClient = new HttpClient(new MockHttpMessageHandler());

    var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);
    c.Dispose();

    await Assert.ThrowsAsync<ObjectDisposedException>(() => c.GetPastesAsync("DUMMY", CancellationToken.None));
  }

  [Fact]
  public async Task GetPastesAsync_NotFound_ReturnsEmptyEnumerable()
  {
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 404));
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    var pastes = await c.GetPastesAsync("UNKNOWN");
    Assert.Empty(pastes);
  }

  [Fact]
  public async Task GetPastesAsync_CancellationToken_NotFound_ReturnsEmptyEnumerable()
  {
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 404));
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    var pastes = await c.GetPastesAsync("UNKNOWN", CancellationToken.None);
    Assert.Empty(pastes);
  }

  [Fact]
  public async Task GetPastesAsync_RateLimitExceeded_ThrowsRateLimitExceededException()
  {
    using var cancellationTokenSource = new CancellationTokenSource();
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 429));
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<RateLimitExceededException>(() => c.GetPastesAsync("DUMMY"));
  }

  [Fact]
  public async Task GetPastesAsync_WithCancellationToken_RateLimitExceeded_ThrowsRateLimitExceededException()
  {
    using var cancellationTokenSource = new CancellationTokenSource();
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 429));
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<RateLimitExceededException>(() => c.GetPastesAsync("DUMMY", CancellationToken.None));
  }

  [Fact]
  public async Task GetPastesAsync_WithImATeapot_Throws()
  {
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 418));
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<HaveIBeenPwnedClientException>(() => c.GetPastesAsync("DUMMY"));
  }

  [Fact]
  public async Task GetPastesAsync_CancellationToken_WithImATeapot_Throws()
  {
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 418));
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<HaveIBeenPwnedClientException>(() => c.GetPastesAsync("DUMMY", CancellationToken.None));
  }

  [Fact]
  public async Task GetPastesAsync_WithValidInput_Succeeds()
  {
    using var httpClient = new HttpClient(new MockHttpMessageHandler());
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    var result = await c.GetPastesAsync("test@example.com");

    Assert.NotNull(result);
    Assert.NotEmpty(result);
  }

  [Fact]
  public async Task GetPastesAsync_CancellationToken_WithValidInput_Succeeds()
  {
    using var httpClient = new HttpClient(new MockHttpMessageHandler());
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    var result = await c.GetPastesAsync("test@example.com", CancellationToken.None);

    Assert.NotNull(result);
    Assert.NotEmpty(result);
  }

  [Fact]
  public async Task GetPastesAsync_CancellationToken_WithCancellationRequested_Throws()
  {
    using var cts = new CancellationTokenSource();

    using var httpClient = new HttpClient(new MockHttpMessageHandler());
    var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    cts.Cancel();

    await Assert.ThrowsAsync<OperationCanceledException>(() => c.GetPastesAsync("test@example.com", cts.Token));
  }
}
