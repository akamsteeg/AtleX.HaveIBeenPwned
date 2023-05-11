using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AtleX.HaveIBeenPwned.IntegrationTests;

[Trait(Constants.Tests.Categories.RequiresApiKeyCategory.Name, "true")]
public class HaveIBeenPwnedClientTests_GetBreachesAsync
  : HaveIBeenPwnedClientIntegrationTestsBase
{
  [Fact]
  public async Task GetBreachesAsync_WithValidInput_DoesNotThrow()
  {
    using var httpClient = new HttpClient();
    using var c = new HaveIBeenPwnedClient(CreateSettings(), httpClient);

    var result = await c.GetBreachesAsync("account-exists@hibp-integration-tests.com");

    Assert.NotNull(result);
  }

  [Fact]
  public async Task GetBreachesAsync_WithValidInputAndExludeUnVerifiedBreaches_DoesNotThrow()
  {
    using var httpClient = new HttpClient();
    using var c = new HaveIBeenPwnedClient(CreateSettings(), httpClient);

    var result = await c.GetBreachesAsync("not-active-breach@hibp-integration-tests.com", BreachMode.ExcludeUnverified);

    Assert.NotNull(result);
  }

  [Fact]
  public async Task GetBreachesAsync_WithValidInputAndCancellationToken_DoesNotThrow()
  {
    using var cancellationTokenSource = new CancellationTokenSource();
    using var httpClient = new HttpClient();
    using var c = new HaveIBeenPwnedClient(CreateSettings(), httpClient);

    var result = await c.GetBreachesAsync("account-exists@hibp-integration-tests.com", cancellationTokenSource.Token);

    Assert.NotNull(result);
  }

  [Fact]
  public async Task GetBreachesAsync_WithValidInputAndExludeUnVerifiedBreachesAndCancellationToken_DoesNotThrow()
  {
    using var cancellationTokenSource = new CancellationTokenSource();
    using var httpClient = new HttpClient();
    using var c = new HaveIBeenPwnedClient(CreateSettings(), httpClient);

    var result = await c.GetBreachesAsync("not-active-breach@hibp-integration-tests.com", BreachMode.ExcludeUnverified, cancellationTokenSource.Token);

    Assert.NotNull(result);
  }
}
