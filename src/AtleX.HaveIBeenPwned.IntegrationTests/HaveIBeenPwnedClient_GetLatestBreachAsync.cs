// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AtleX.HaveIBeenPwned.IntegrationTests;
public class HaveIBeenPwnedClient_GetLatestBreachAsync
  : HaveIBeenPwnedClientIntegrationTestsBase
{
  [Fact]
  public async Task GetLatestBreachAsync_DoesNotThrow()
  {
    using var httpClient = new HttpClient();
    using var c = new HaveIBeenPwnedClient(CreateSettings(), httpClient);

    var result = await c.GetLatestBreachAsync();

    Assert.NotNull(result);
    Assert.IsType<SiteBreach>(result);
  }

  [Fact]
  public async Task GetLatestBreachAsync_WithCancellationToken_DoesNotThrow()
  {
    using var cancellationTokenSource = new CancellationTokenSource();
    using var httpClient = new HttpClient();
    using var c = new HaveIBeenPwnedClient(CreateSettings(), httpClient);

    var result = await c.GetLatestBreachAsync(cancellationTokenSource.Token);

    Assert.NotNull(result);
    Assert.IsType<SiteBreach>(result);
  }
}
