// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AtleX.HaveIBeenPwned.IntegrationTests.XUnit;
using Xunit;

namespace AtleX.HaveIBeenPwned.IntegrationTests;

[ApiKeyRequestDelayer]
public class HaveIBeenPwnedClient_GetBreachedDomainUsersAsync
  : HaveIBeenPwnedClientIntegrationTestsBase
{
  private const string UnknownDomain = "dba9f72.com";

  [FactWithApiKey]
  public async Task GetBreachedDomainUsersAsync_WithValidInput_ReturnsResults()
  {
    using var httpClient = new HttpClient();
    using var c = new HaveIBeenPwnedClient(CreateSettings(), httpClient);

    var result = await c.GetBreachedDomainUsersAsync(PrivateSettings.OwnedDomain);

    Assert.NotNull(result);
    Assert.NotEmpty(result);
  }

  [FactWithApiKey]
  public async Task GetBreachedDomainUsersAsync_WithValidInputAndCancellationToken_ReturnsResults()
  {
    using var cancellationTokenSource = new CancellationTokenSource();
    using var httpClient = new HttpClient();
    using var c = new HaveIBeenPwnedClient(CreateSettings(), httpClient);

    var result = await c.GetBreachedDomainUsersAsync(PrivateSettings.OwnedDomain, cancellationTokenSource.Token);

    Assert.NotNull(result);
    Assert.NotEmpty(result);
  }

  [FactWithApiKey]
  public async Task GetBreachedDomainUsersAsync_WithUnknownDomain_ThrowsDomainForbiddenException()
  {
    using var httpClient = new HttpClient();
    using var c = new HaveIBeenPwnedClient(CreateSettings(), httpClient);

    await Assert.ThrowsAsync<DomainForbiddenException>(() => c.GetBreachedDomainUsersAsync(UnknownDomain));
  }

  [FactWithApiKey]
  public async Task GetBreachedDomainUsersAsync_WithUnknownDomainAndCancellationToken_DoesNotThrow()
  {
    using var cancellationTokenSource = new CancellationTokenSource();
    using var httpClient = new HttpClient();
    using var c = new HaveIBeenPwnedClient(CreateSettings(), httpClient);

    await Assert.ThrowsAsync<DomainForbiddenException>(() => c.GetBreachedDomainUsersAsync(UnknownDomain, cancellationTokenSource.Token));
  }

  [FactWithApiKey]
  public async Task GetBreachedDomainUsersAsync_WithInvalidApiKey_ThrowsInvalidApiKeyException()
  {
    var settings = new HaveIBeenPwnedClientSettings()
    {
      ApiKey = "DUMMYAPIKEY",
      ApplicationName = Constants.Tests.ApplicationName,
    };

    using var cancellationTokenSource = new CancellationTokenSource();
    using var httpClient = new HttpClient();
    using var c = new HaveIBeenPwnedClient(settings, httpClient);

    await Assert.ThrowsAsync<InvalidApiKeyException>(() => c.GetBreachedDomainUsersAsync(PrivateSettings.OwnedDomain));
  }

  [FactWithApiKey]
  public async Task GetBreachedDomainUsersAsync_WithCancellationTokenAndInvalidApiKey_ThrowsInvalidApiKeyException()
  {
    var settings = new HaveIBeenPwnedClientSettings()
    {
      ApiKey = "DUMMYAPIKEY",
      ApplicationName = Constants.Tests.ApplicationName,
    };

    using var cancellationTokenSource = new CancellationTokenSource();
    using var httpClient = new HttpClient();
    using var c = new HaveIBeenPwnedClient(settings, httpClient);

    await Assert.ThrowsAsync<InvalidApiKeyException>(() => c.GetBreachedDomainUsersAsync(PrivateSettings.OwnedDomain, cancellationTokenSource.Token));
  }
}
