// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AtleX.HaveIBeenPwned.IntegrationTests.XUnit;
using Xunit;

namespace AtleX.HaveIBeenPwned.IntegrationTests;

[ApiKeyRequestDelayer]
public class HaveIBeenPwnedClient_GetSubscribedDomainsAsync
  : HaveIBeenPwnedClientIntegrationTestsBase
{
  [FactWithApiKey]
  public async Task GetSubscribedDomainsAsync_DoesNotThrow()
  {
    using var httpClient = new HttpClient();
    using var c = new HaveIBeenPwnedClient(CreateSettings(), httpClient);

    var result = await c.GetSubscribedDomainsAsync();

    Assert.NotNull(result);
  }

  [FactWithApiKey]
  public async Task GetSubscribedDomainsAsync_WithCancellationToken_DoesNotThrow()
  {
    using var cancellationTokenSource = new CancellationTokenSource();
    using var httpClient = new HttpClient();
    using var c = new HaveIBeenPwnedClient(CreateSettings(), httpClient);

    var result = await c.GetSubscribedDomainsAsync(cancellationTokenSource.Token);

    Assert.NotNull(result);
  }
}
