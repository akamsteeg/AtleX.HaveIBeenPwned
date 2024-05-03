// Copyright (c) Alex Kamsteeg (https://atlex.nl/)
// License: MIT (See LICENSE file)

using AtleX.HaveIBeenPwned.Tests.Mocks;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests;

public class HaveIBeenPwnedClientTests_IsPwnedPassword
  : HaveIBeenPwnedClientTestsBase
{
  [Fact]
  public async Task IsPwnedPasswordAsync_WithNullValueForPassword_Throws()
  {
    using var httpClient = new HttpClient(new MockHttpMessageHandler());
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<ArgumentNullException>(() => c.IsPwnedPasswordAsync(null));
  }

  [Fact]
  public async Task IsPwnedPasswordAsync_CancellationToken_WithNullValueForPassword_Throws()
  {
    using var httpClient = new HttpClient(new MockHttpMessageHandler());
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<ArgumentNullException>(() => c.IsPwnedPasswordAsync(null, CancellationToken.None));
  }

  [Fact]
  public async Task IsPwnedPasswordAsync_AfterDispose_Throws()
  {
    using var httpClient = new HttpClient(new MockHttpMessageHandler());

    var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);
    c.Dispose();

    await Assert.ThrowsAsync<ObjectDisposedException>(() => c.IsPwnedPasswordAsync("DUMMY"));
  }

  [Fact]
  public async Task IsPwnedPasswordAsync_CancellationToken_AfterDispose_Throws()
  {
    using var httpClient = new HttpClient(new MockHttpMessageHandler());

    var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);
    c.Dispose();

    await Assert.ThrowsAsync<ObjectDisposedException>(() => c.IsPwnedPasswordAsync("DUMMY", CancellationToken.None));
  }

  [Fact]
  public async Task IsPwnedPasswordAsync_NotFound_ThrowsHaveIBeenPwnedClientException()
  {
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 404));
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<HaveIBeenPwnedClientException>(() => c.IsPwnedPasswordAsync("UNKNOWN"));
  }

  [Fact]
  public async Task IsPwnedPasswordAsync_CancellationToken_NotFound_ThrowsHaveIBeenPwnedClientException()
  {
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 404));
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<HaveIBeenPwnedClientException>(() => c.IsPwnedPasswordAsync("UNKNOWN", CancellationToken.None));
  }

  [Fact]
  public async Task IsPwnedPasswordAsync_RateLimitExceeded_ThrowsRateLimitExceededException()
  {
    using var cancellationTokenSource = new CancellationTokenSource();
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 429));
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<RateLimitExceededException>(() => c.IsPwnedPasswordAsync("DUMMY"));
  }

  [Fact]
  public async Task IsPwnedPasswordAsync_WithCancellationToken_RateLimitExceeded_ThrowsRateLimitExceededException()
  {
    using var cancellationTokenSource = new CancellationTokenSource();
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 429));
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<RateLimitExceededException>(() => c.IsPwnedPasswordAsync("DUMMY", CancellationToken.None));
  }

  [Fact]
  public async Task IsPwnedPasswordAsync_WithImATeapot_Throws()
  {
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 418));
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<HaveIBeenPwnedClientException>(() => c.IsPwnedPasswordAsync("DUMMY"));
  }

  [Fact]
  public async Task IsPwnedPasswordAsync_CancellationToken_WithImATeapot_Throws()
  {
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 418));
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<HaveIBeenPwnedClientException>(() => c.IsPwnedPasswordAsync("DUMMY", CancellationToken.None));
  }

  [Fact]
  public async Task IsPwnedPasswordAsync_KnownInput_Succeeds()
  {
    using var httpClient = new HttpClient(new MockHttpMessageHandler());
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    var result = await c.IsPwnedPasswordAsync("P@ssw0rd");

    Assert.True(result);
  }

  [Fact]
  public async Task IsPwnedPasswordAsync_CancellationToken_KnownInput_Succeeds()
  {
    using var httpClient = new HttpClient(new MockHttpMessageHandler());
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    var result = await c.IsPwnedPasswordAsync("P@ssw0rd", CancellationToken.None);

    Assert.True(result);
  }

  [Fact]
  public async Task IsPwnedPasswordAsync_CancellationToken_WithCancellationRequested_Throws()
  {
    using var cts = new CancellationTokenSource();

    using var httpClient = new HttpClient(new MockHttpMessageHandler());
    var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    cts.Cancel();

    await Assert.ThrowsAsync<OperationCanceledException>(() => c.IsPwnedPasswordAsync("DUMMY", cts.Token));
  }
}
