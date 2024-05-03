// Copyright (c) Alex Kamsteeg (https://atlex.nl/)
// License: MIT (See LICENSE file)

using AtleX.HaveIBeenPwned.IntegrationTests.XUnit;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AtleX.HaveIBeenPwned.IntegrationTests;

[Trait(Constants.Tests.Categories.RequiresApiKeyCategory.Name, "true")]
[ApiKeyRequestDelayer]
public class HaveIBeenPwnedClientTests_GetPastesAsync
  : HaveIBeenPwnedClientIntegrationTestsBase
{
  [FactWithApiKey]
  public async Task GetPastesAsync_WithValidInput_ReturnsResults()
  {
    using var httpClient = new HttpClient();
    using var c = new HaveIBeenPwnedClient(CreateSettings(), httpClient);

    var result = await c.GetPastesAsync("account-exists@hibp-integration-tests.com");

    Assert.NotNull(result);
    Assert.NotEmpty(result);
  }

  [FactWithApiKey]
  public async Task GetPastesAsync_WithUnknownEmail_DoesNotThrow()
  {
    using var httpClient = new HttpClient();
    using var c = new HaveIBeenPwnedClient(CreateSettings(), httpClient);

    var result = await c.GetPastesAsync("opt-out@hibp-integration-tests.com");

    Assert.NotNull(result);
    Assert.Empty(result);
  }

  [FactWithApiKey]
  public async Task GetPastesAsync_WithValidInputAndCancellationToken_ReturnsResults()
  {
    using var cancellationTokenSource = new CancellationTokenSource();
    using var httpClient = new HttpClient();
    using var c = new HaveIBeenPwnedClient(CreateSettings(), httpClient);

    var result = await c.GetPastesAsync("account-exists@hibp-integration-tests.com", cancellationTokenSource.Token);

    Assert.NotNull(result);
    Assert.NotEmpty(result);
  }

  [FactWithApiKey]
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

  [FactWithApiKey]
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

  [FactWithApiKey]
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
