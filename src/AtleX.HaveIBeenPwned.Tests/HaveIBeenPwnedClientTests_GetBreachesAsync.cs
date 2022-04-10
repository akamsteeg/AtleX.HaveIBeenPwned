using AtleX.HaveIBeenPwned.Tests.Mocks;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests;

public class HaveIBeenPwnedClientTests_GetBreachesAsync
  : HaveIBeenPwnedClientTestsBase
{
  [Fact]
  public async Task GetBreachesAsync_WithNullValueForAccount_Throws()
  {
    using var httpClient = new HttpClient(new MockHttpMessageHandler());

    var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<ArgumentNullException>(() => c.GetBreachesAsync(null));
  }

  [Fact]
  public async Task GetBreachesAsync_CancellationToken_WithNullValueForAccount_Throws()
  {
    using var httpClient = new HttpClient(new MockHttpMessageHandler());

    var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<ArgumentNullException>(() => c.GetBreachesAsync(null, CancellationToken.None));
  }

  [Fact]
  public async Task GetBreachesAsync_AfterDispose_Throws()
  {
    using var httpClient = new HttpClient(new MockHttpMessageHandler());

    var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);
    c.Dispose();

    await Assert.ThrowsAsync<ObjectDisposedException>(() => c.GetBreachesAsync("DUMMY"));
  }

  [Fact]
  public async Task GetBreachesAsync_CancellationToken_AfterDispose_Throws()
  {
    using var httpClient = new HttpClient(new MockHttpMessageHandler());

    var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);
    c.Dispose();

    await Assert.ThrowsAsync<ObjectDisposedException>(() => c.GetBreachesAsync("DUMMY", CancellationToken.None));
  }

  [Fact]
  public async Task GetBreachesAsync_RateLimitExceeded_ThrowsRateLimitExceededException()
  {
    using var cancellationTokenSource = new CancellationTokenSource();
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 429));
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<RateLimitExceededException>(() => c.GetBreachesAsync("DUMMY"));
  }

  [Fact]
  public async Task GetBreachesAsync_WithCancellationToken_RateLimitExceeded_ThrowsRateLimitExceededException()
  {
    using var cancellationTokenSource = new CancellationTokenSource();
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 429));
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<RateLimitExceededException>(() => c.GetBreachesAsync("DUMMY", CancellationToken.None));
  }

  [Fact]
  public async Task GetBreachesAsync_WithInvalidApiKey_Throws()
  {
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 401));

    var settings = this.ClientSettings;
    settings.ApiKey = string.Empty;

    using var c = new HaveIBeenPwnedClient(settings, httpClient);

    await Assert.ThrowsAsync<InvalidApiKeyException>(() => c.GetBreachesAsync("DUMMY"));
  }

  [Theory]
  [InlineData(BreachMode.All)]
  [InlineData(BreachMode.ExcludeUnverified)]
  public async Task GetBreachesAsync_BreachMode_WithInvalidApiKey_Throws(BreachMode breachMode)
  {
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 401));
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<InvalidApiKeyException>(() => c.GetBreachesAsync("DUMMY", breachMode));
  }

  [Fact]
  public async Task GetBreachesAsync_CancellationToken_WithoutApiKey_Throws()
  {
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 401));
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<InvalidApiKeyException>(() => c.GetBreachesAsync("DUMMY", CancellationToken.None));
  }

  [Fact]
  public async Task GetBreachesAsync_CancellationToken_WithInvalidApiKey_Throws()
  {
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 401));

    var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<InvalidApiKeyException>(() => c.GetBreachesAsync("DUMMY", CancellationToken.None));
  }

  [Theory]
  [InlineData(BreachMode.All)]
  [InlineData(BreachMode.ExcludeUnverified)]
  public async Task GetBreachesAsync_BreachModeCancellationToken_WithInvalidApiKey_Throws(BreachMode breachMode)
  {
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 401));

    var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<InvalidApiKeyException>(() => c.GetBreachesAsync("DUMMY", breachMode, CancellationToken.None));
  }

  [Fact]
  public async Task GetBreachesAsync_WithoutApiKey_Throws()
  {
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 401));

    var settings = this.ClientSettings;
    settings.ApiKey = string.Empty;

    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<InvalidApiKeyException>(() => c.GetBreachesAsync("DUMMY"));
  }

  [Theory]
  [InlineData(BreachMode.All)]
  [InlineData(BreachMode.ExcludeUnverified)]
  public async Task GetBreachesAsync_BreachMode_WithoutApiKey_Throws(BreachMode breachMode)
  {
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 401));

    var settings = this.ClientSettings;
    settings.ApiKey = string.Empty;

    using var c = new HaveIBeenPwnedClient(settings, httpClient);

    await Assert.ThrowsAsync<InvalidApiKeyException>(() => c.GetBreachesAsync("DUMMY", breachMode));
  }

  [Theory]
  [InlineData(BreachMode.All)]
  [InlineData(BreachMode.ExcludeUnverified)]
  public async Task GetBreachesAsync_BreachModeCancellationToken_WithoutApiKey_Throws(BreachMode breachMode)
  {
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 401));
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<InvalidApiKeyException>(() => c.GetBreachesAsync("DUMMY", breachMode, CancellationToken.None));
  }

  [Fact]
  public async Task GetBreachesAsync_WithImATeapot_Throws()
  {
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 418));
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<HaveIBeenPwnedClientException>(() => c.GetBreachesAsync("DUMMY"));
  }

  [Fact]
  public async Task GetBreachesAsync_CancellationToken_WithImATeapot_Throws()
  {
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 418));
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<HaveIBeenPwnedClientException>(() => c.GetBreachesAsync("DUMMY", CancellationToken.None));
  }

  [Fact]
  public async Task GetBreachesAsync_BreachModeCancellationToken_WithImATeapot_Throws()
  {
    using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 418));
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    await Assert.ThrowsAsync<HaveIBeenPwnedClientException>(() => c.GetBreachesAsync("DUMMY", BreachMode.Default, CancellationToken.None));
  }

  [Fact]
  public async Task GetBreachesAsync_WithValidInput_Succeeds()
  {
    using var httpClient = new HttpClient(new MockHttpMessageHandler());
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    var result = await c.GetBreachesAsync("test@example.com");

    Assert.NotNull(result);
    Assert.NotEmpty(result);
  }

  [Theory]
  [InlineData(BreachMode.All)]
  [InlineData(BreachMode.ExcludeUnverified)]
  public async Task GetBreachesAsync_BreachMode_WithValidInput_Succeeds(BreachMode breachMode)
  {
    using var httpClient = new HttpClient(new MockHttpMessageHandler());
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    var result = await c.GetBreachesAsync("test@example.com", breachMode);

    Assert.NotNull(result);
    Assert.NotEmpty(result);
  }

  [Fact]
  public async Task GetBreachesAsync_CancellationToken_WithValidInput_Succeeds()
  {
    using var httpClient = new HttpClient(new MockHttpMessageHandler());
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    var result = await c.GetBreachesAsync("test@example.com", CancellationToken.None);

    Assert.NotNull(result);
    Assert.NotEmpty(result);
  }

  [Theory]
  [InlineData(BreachMode.All)]
  [InlineData(BreachMode.ExcludeUnverified)]
  public async Task GetBreachesAsync_BreachModeCancellationToken_WithValidInput_Succeeds(BreachMode breachMode)
  {
    using var httpClient = new HttpClient(new MockHttpMessageHandler());
    using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

    var result = await c.GetBreachesAsync("test@example.com", breachMode, CancellationToken.None);

    Assert.NotNull(result);
    Assert.NotEmpty(result);
  }
}
