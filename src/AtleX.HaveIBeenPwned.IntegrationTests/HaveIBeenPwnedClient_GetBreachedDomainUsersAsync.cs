using AtleX.HaveIBeenPwned.IntegrationTests.XUnit;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AtleX.HaveIBeenPwned.IntegrationTests;
public class HaveIBeenPwnedClient_GetBreachedDomainUsersAsync
  : HaveIBeenPwnedClientIntegrationTestsBase
{
  [FactWithApiKey]
  public async Task GetBreachedDomainUsersAsync_WithValidInput_ReturnsResults()
  {
    using var httpClient = new HttpClient();
    using var c = new HaveIBeenPwnedClient(CreateSettings(), httpClient);

    var result = await c.GetBreachedDomainUsersAsync("atlex.nl");

    Assert.NotNull(result);
    Assert.NotEmpty(result);
  }

  [FactWithApiKey]
  public async Task GetBreachedDomainUsersAsync_WithUnknownDomain_DoesNotThrow()
  {
    using var httpClient = new HttpClient();
    using var c = new HaveIBeenPwnedClient(CreateSettings(), httpClient);

    var result = await c.GetBreachedDomainUsersAsync(Guid.Empty.ToString());

    Assert.NotNull(result);
    Assert.Empty(result);
  }

  [FactWithApiKey]
  public async Task GetBreachedDomainUsersAsync_WithValidInputAndCancellationToken_ReturnsResults()
  {
    using var cancellationTokenSource = new CancellationTokenSource();
    using var httpClient = new HttpClient();
    using var c = new HaveIBeenPwnedClient(CreateSettings(), httpClient);

    var result = await c.GetBreachedDomainUsersAsync("atlex.nl", cancellationTokenSource.Token);

    Assert.NotNull(result);
    Assert.NotEmpty(result);
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

    await Assert.ThrowsAsync<InvalidApiKeyException>(() => c.GetBreachedDomainUsersAsync("atlex.nl"));
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

    await Assert.ThrowsAsync<InvalidApiKeyException>(() => c.GetBreachedDomainUsersAsync("atlex.nl", cancellationTokenSource.Token));
  }

  [FactWithApiKey]
  public async Task GetBreachedDomainUsersAsync_WithUnknownDomainAndCancellationToken_DoesNotThrow()
  {
    using var cancellationTokenSource = new CancellationTokenSource();
    using var httpClient = new HttpClient();
    using var c = new HaveIBeenPwnedClient(CreateSettings(), httpClient);

    var result = await c.GetBreachedDomainUsersAsync(Guid.Empty.ToString(), cancellationTokenSource.Token);

    Assert.NotNull(result);
    Assert.Empty(result);
  }
}
