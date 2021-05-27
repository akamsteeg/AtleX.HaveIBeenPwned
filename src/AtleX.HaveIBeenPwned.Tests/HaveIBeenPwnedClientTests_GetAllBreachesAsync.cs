using AtleX.HaveIBeenPwned.Tests.Mocks;
using System;
using System.Collections;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests
{
  public class HaveIBeenPwnedClientTests_GetAllBreachesAsync
    : HaveIBeenPwnedClientTestsBase
  {
    [Fact]
    public async Task GetAllBreachesAsync_WithValidInput_Succeeds()
    {
      using var httpClient = new HttpClient(new MockHttpMessageHandler());
      using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

      var result = await c.GetAllBreachesAsync();

      Assert.NotNull(result);
      Assert.NotEmpty(result);
    }

    [Fact]
    public async Task GetAllBreachesAsync_CancellationToken_WithValidInput_Succeeds()
    {
      using var cancellationTokenSource = new CancellationTokenSource();
      using var httpClient = new HttpClient(new MockHttpMessageHandler());
      using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

      var result = await c.GetAllBreachesAsync(cancellationTokenSource.Token);

      Assert.NotNull(result);
      Assert.NotEmpty(result);
    }

    [Fact]
    public async Task GetAllBreachesAsync_AfterDispose_Throws()
    {
      using var httpClient = new HttpClient(new MockHttpMessageHandler());

      var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);
      c.Dispose();

      await Assert.ThrowsAsync<ObjectDisposedException>(() => c.GetAllBreachesAsync());
    }

    [Fact]
    public async Task GetAllBreachesAsync_CancellationToken_AfterDispose_Throws()
    {
      using var httpClient = new HttpClient(new MockHttpMessageHandler());

      var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);
      c.Dispose();

      await Assert.ThrowsAsync<ObjectDisposedException>(() => c.GetAllBreachesAsync(CancellationToken.None));
    }

    [Fact]
    public async Task GetAllBreachesAsync_RateLimitExceeded_ThrowsRateLimitExceededException()
    {
      using var cancellationTokenSource = new CancellationTokenSource();
      using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 429));
      using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

      await Assert.ThrowsAsync<RateLimitExceededException>(() => c.GetAllBreachesAsync());
    }

    [Fact]
    public async Task GetAllBreachesAsync_CancellationToken_RateLimitExceeded_ThrowsRateLimitExceededException()
    {
      using var cancellationTokenSource = new CancellationTokenSource();
      using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 429));
      using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

      await Assert.ThrowsAsync<RateLimitExceededException>(() => c.GetAllBreachesAsync(CancellationToken.None));
    }

    [Fact]
    public async Task GetAllBreachesAsync_WithImATeapot_Throws()
    {
      using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 418));
      using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

      await Assert.ThrowsAsync<HaveIBeenPwnedClientException>(() => c.GetAllBreachesAsync());
    }

    [Fact]
    public async Task GetAllBreachesAsync_CancellationToken_WithImATeapot_Throws()
    {
      using var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 418));
      using var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

      await Assert.ThrowsAsync<HaveIBeenPwnedClientException>(() => c.GetAllBreachesAsync(CancellationToken.None));
    }
  }
}
