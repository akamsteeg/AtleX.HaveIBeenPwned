﻿using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AtleX.HaveIBeenPwned.IntegrationTests;

[Trait(Constants.Tests.Categories.RequiresApiKeyCategory.Name, "true")]
public class HaveIBeenPwnedClientTests_GetPastesAsync
  : HaveIBeenPwnedClientIntegrationTestsBase
{
  [Fact]
  public async Task GetPastesAsync_WithValidInput_ReturnsResults()
  {
    using var httpClient = new HttpClient();
    using var c = new HaveIBeenPwnedClient(CreateSettings(), httpClient);

    var result = await c.GetPastesAsync("account-exists@hibp-integration-tests.com");

    Assert.NotNull(result);
    Assert.NotEmpty(result);
  }

  [Fact]
  public async Task GetPastesAsync_WithUnknownEmail_DoesNotThrow()
  {
    using var httpClient = new HttpClient();
    using var c = new HaveIBeenPwnedClient(CreateSettings(), httpClient);

    var result = await c.GetPastesAsync("opt-out@hibp-integration-tests.com");

    Assert.NotNull(result);
    Assert.Empty(result);
  }

  [Fact]
  public async Task GetPastesAsync_WithValidInputAndCancellationToken_ReturnsResults()
  {
    using var cancellationTokenSource = new CancellationTokenSource();
    using var httpClient = new HttpClient();
    using var c = new HaveIBeenPwnedClient(CreateSettings(), httpClient);

    var result = await c.GetPastesAsync("account-exists@hibp-integration-tests.com", cancellationTokenSource.Token);

    Assert.NotNull(result);
    Assert.NotEmpty(result);
  }

  [Fact]
  public async Task GetPastesAsync_WithInvalidApiKey_ThrowsInvalidApiKeyException()
  {
    var settings = new HaveIBeenPwnedClientSettings()
    {
      ApiKey = "DUMMYAPIKEY",
      ApplicationName = Constants.Tests.ApplicationName,
    };

    using var cancellationTokenSource = new CancellationTokenSource();
    using var httpClient = new HttpClient();
    using var c = new HaveIBeenPwnedClient(settings, httpClient);

    await Assert.ThrowsAsync<InvalidApiKeyException>(() => c.GetPastesAsync("opt-out@hibp-integration-tests.com"));
  }

  [Fact]
  public async Task GetPastesAsync_WithCancellationTokenAndInvalidApiKey_ThrowsInvalidApiKeyException()
  {
    var settings = new HaveIBeenPwnedClientSettings()
    {
      ApiKey = "DUMMYAPIKEY",
      ApplicationName = Constants.Tests.ApplicationName,
    };

    using var cancellationTokenSource = new CancellationTokenSource();
    using var httpClient = new HttpClient();
    using var c = new HaveIBeenPwnedClient(settings, httpClient);

    await Assert.ThrowsAsync<InvalidApiKeyException>(() => c.GetPastesAsync("account-exists@hibp-integration-tests.com", cancellationTokenSource.Token));
  }

  [Fact]
  public async Task GetPastesAsync_WithUnknownEmailAndCancellationToken_DoesNotThrow()
  {
    using var cancellationTokenSource = new CancellationTokenSource();
    using var httpClient = new HttpClient();
    using var c = new HaveIBeenPwnedClient(CreateSettings(), httpClient);

    var result = await c.GetPastesAsync("opt-out@hibp-integration-tests.com", cancellationTokenSource.Token);

    Assert.NotNull(result);
    Assert.Empty(result);
  }
}
