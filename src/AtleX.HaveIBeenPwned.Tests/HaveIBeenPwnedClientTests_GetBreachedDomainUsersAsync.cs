// Copyright (c) Alex Kamsteeg (https://atlex.nl/)
// License: MIT (See LICENSE file)

using AtleX.HaveIBeenPwned.Tests.Mocks;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests;
public class HaveIBeenPwnedClientTests_GetBreachedDomainUsersAsync
  : HaveIBeenPwnedClientTestsBase
{
  [Fact]
  public async Task GetBreachedDomainUsersAsync_WithValidValueForDomain_Succeeds()
  {
    using var httpClient = new HttpClient(new MockHttpMessageHandler());

    var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    var result = await c.GetBreachedDomainUsersAsync("atlex.nl");

    Assert.NotNull(result);
    Assert.NotEmpty(result);
  }

  [Fact]
  public async Task GetBreachedDomainUsersAsync_WithNullValueForDomain_Throws()
  {
    using var httpClient = new HttpClient(new MockHttpMessageHandler());

    var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<ArgumentNullException>(() => c.GetBreachedDomainUsersAsync(null));
  }

  [Fact]
  public async Task GetBreachedDomainUsersAsync_CancellationToken_WithNullValueForDomain_Throws()
  {
    using var httpClient = new HttpClient(new MockHttpMessageHandler());

    var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<ArgumentNullException>(() => c.GetBreachedDomainUsersAsync(null, CancellationToken.None));
  }

  [Fact]
  public async Task GetBreachedDomainUsersAsync_AfterDispose_Throws()
  {
    using var httpClient = new HttpClient(new MockHttpMessageHandler());

    var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);
    c.Dispose();

    await Assert.ThrowsAsync<ObjectDisposedException>(() => c.GetBreachedDomainUsersAsync("DUMMY"));
  }

  [Fact]
  public async Task GetBreachedDomainUsersAsync_CancellationToken_AfterDispose_Throws()
  {
    using var httpClient = new HttpClient(new MockHttpMessageHandler());

    var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);
    c.Dispose();

    await Assert.ThrowsAsync<ObjectDisposedException>(() => c.GetBreachedDomainUsersAsync("DUMMY", CancellationToken.None));
  }

  [Fact]
  public async Task GetBreachedDomainUsersAsync_RateLimitExceeded_ThrowsRateLimitExceededException()
  {
    using var cancellationTokenSource = new CancellationTokenSource();
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 429));
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<RateLimitExceededException>(() => c.GetBreachedDomainUsersAsync("DUMMY"));
  }

  [Fact]
  public async Task GetBreachedDomainUsersAsync_WithCancellationToken_RateLimitExceeded_ThrowsRateLimitExceededException()
  {
    using var cancellationTokenSource = new CancellationTokenSource();
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 429));
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<RateLimitExceededException>(() => c.GetBreachedDomainUsersAsync("DUMMY", CancellationToken.None));
  }

  [Fact]
  public async Task GetBreachedDomainUsersAsync_WithInvalidApiKey_Throws()
  {
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 401));

    var settings = this.ClientSettings;
    settings.ApiKey = string.Empty;

    using var c = new HaveIBeenPwnedClient(settings, httpClient);

    await Assert.ThrowsAsync<InvalidApiKeyException>(() => c.GetBreachedDomainUsersAsync("DUMMY"));
  }

  [Fact]
  public async Task GetBreachedDomainUsersAsync_WithNotOwnedDomain_Throws()
  {
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 403));

    var settings = this.ClientSettings;
    settings.ApiKey = "Valid";

    using var c = new HaveIBeenPwnedClient(settings, httpClient);

    await Assert.ThrowsAsync<DomainForbiddenException>(() => c.GetBreachedDomainUsersAsync("DUMMY"));
  }

  [Fact]
  public async Task GetBreachedDomainUsersAsync_WithCancellationToken_WithNotOwnedDomain_Throws()
  {
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 403));

    var settings = this.ClientSettings;
    settings.ApiKey = "Valid";

    using var c = new HaveIBeenPwnedClient(settings, httpClient);

    await Assert.ThrowsAsync<DomainForbiddenException>(() => c.GetBreachedDomainUsersAsync("DUMMY", CancellationToken.None));
  }
}
