using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AtleX.HaveIBeenPwned.IntegrationTests;

public class HaveIBeenPwnedClientTests_GetAllBreachesAsync
  : HaveIBeenPwnedClientIntegrationTestsBase
{
  public override void Dispose()
  {
    // NOP We don't need to wait between requests since this type does not have a rate limit
  }

  [Fact]
  public async Task GetAllBreachesAsync_DoesNotThrow()
  {
    using var httpClient = new HttpClient();
    using var c = new HaveIBeenPwnedClient(CreateSettings(), httpClient);

    var result = await c.GetAllBreachesAsync();

    Assert.NotNull(result);
  }

  [Fact]
  public async Task GetAllBreachesAsync_WithCancellationToken_DoesNotThrow()
  {
    using var cancellationTokenSource = new CancellationTokenSource();
    using var httpClient = new HttpClient();
    using var c = new HaveIBeenPwnedClient(CreateSettings(), httpClient);

    var result = await c.GetAllBreachesAsync(cancellationTokenSource.Token);

    Assert.NotNull(result);

    await Task.Delay(Constants.Tests.DelayBetweenTests);
  }
}
