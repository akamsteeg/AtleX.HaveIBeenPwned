using AtleX.HaveIBeenPwned.Tests.Mocks;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests
{
  public class HaveIBeenPwnedClientTests_GetBreachesAsync
    : HaveIBeenPwnedClientTestsBase
  {
    [Fact]
    public async Task GetBreachesAsync_WithNullValueForAccount_Throws()
    {
      using (var httpClient = new HttpClient(new MockHttpMessageHandler()))
      {
        var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

        await Assert.ThrowsAsync<ArgumentNullException>(() => c.GetBreachesAsync(null));
      }
    }

    [Fact]
    public async Task GetBreachesAsync_CancellationToken_WithNullValueForAccount_Throws()
    {
      using (var httpClient = new HttpClient(new MockHttpMessageHandler()))
      {
        var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

        await Assert.ThrowsAsync<ArgumentNullException>(() => c.GetBreachesAsync(null, CancellationToken.None));
      }
    }

    [Fact]
    public async Task GetBreachesAsync_AfterDispose_Throws()
    {
      using (var httpClient = new HttpClient(new MockHttpMessageHandler()))
      {
        var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);
        c.Dispose();

        await Assert.ThrowsAsync<ObjectDisposedException>(() => c.GetBreachesAsync("DUMMY"));
      }
    }

    [Fact]
    public async Task GetBreachesAsync_CancellationToken_AfterDispose_Throws()
    {
      using (var httpClient = new HttpClient(new MockHttpMessageHandler()))
      {
        var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);
        c.Dispose();

        await Assert.ThrowsAsync<ObjectDisposedException>(() => c.GetBreachesAsync("DUMMY", CancellationToken.None));
      }
    }

    [Fact]
    public async Task GetBreachesAsync_RateLimitExceeded_ThrowsRateLimitExceededException()
    {
      using (var cancellationTokenSource = new CancellationTokenSource())
      using (var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 429)))
      using (var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient))
      {
        await Assert.ThrowsAsync<RateLimitExceededException>(() => c.GetBreachesAsync("DUMMY"));
      }
    }

    [Fact]
    public async Task GetBreachesAsync_WithCancellationToken_RateLimitExceeded_ThrowsRateLimitExceededException()
    {
      using (var cancellationTokenSource = new CancellationTokenSource())
      using (var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 429)))
      using (var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient))
      {
        await Assert.ThrowsAsync<RateLimitExceededException>(() => c.GetBreachesAsync("DUMMY", CancellationToken.None));
      }
    }

    [Fact]
    public async Task GetBreachesAsync_WithInvalidApiKey_Throws()
    {
      using (var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 401)))
      {
        var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

        await Assert.ThrowsAsync<InvalidApiKeyException>(() => c.GetBreachesAsync("DUMMY"));
      }
    }

    [Fact]
    public async Task GetBreachesAsync_BreachModeAll_WithInvalidApiKey_Throws()
    {
      using (var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 401)))
      {
        var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

        await Assert.ThrowsAsync<InvalidApiKeyException>(() => c.GetBreachesAsync("DUMMY", BreachMode.All));
      }
    }

    [Fact]
    public async Task GetBreachesAsync_CancellationToken_WithoutApiKey_Throws()
    {
      using (var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 401)))
      using (var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient))
      {
        await Assert.ThrowsAsync<InvalidApiKeyException>(() => c.GetBreachesAsync("DUMMY", CancellationToken.None));
      }
    }

    [Fact]
    public async Task GetBreachesAsync_CancellationToken_WithInvalidApiKey_Throws()
    {
      using (var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 401)))
      {
        var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

        await Assert.ThrowsAsync<InvalidApiKeyException>(() => c.GetBreachesAsync("DUMMY", CancellationToken.None));
      }
    }

    [Fact]
    public async Task GetBreachesAsync_BreachModeAllCancellationToken_WithInvalidApiKey_Throws()
    {
      using (var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 401)))
      {
        var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

        await Assert.ThrowsAsync<InvalidApiKeyException>(() => c.GetBreachesAsync("DUMMY", BreachMode.All, CancellationToken.None));
      }
    }

    [Fact]
    public async Task GetBreachesAsync_WithoutApiKey_Throws()
    {
      using (var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 401)))
      using (var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient))
      {
        await Assert.ThrowsAsync<InvalidApiKeyException>(() => c.GetBreachesAsync("DUMMY"));
      }
    }

    [Fact]
    public async Task GetBreachesAsync_BreachModeAll_WithoutApiKey_Throws()
    {
      using (var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 401)))
      using (var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient))
      {
        await Assert.ThrowsAsync<InvalidApiKeyException>(() => c.GetBreachesAsync("DUMMY", BreachMode.All));
      }
    }

    [Fact]
    public async Task GetBreachesAsync_BreachModeAllCancellationToken_WithoutApiKey_Throws()
    {
      using (var httpClient = new HttpClient(new MockErroringHttpMessageHandler(desiredResultStatusCode: 401)))
      using (var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient))
      {
        await Assert.ThrowsAsync < InvalidApiKeyException>(() => c.GetBreachesAsync("DUMMY", BreachMode.All, CancellationToken.None));
      }
    }

    [Fact]
    public async Task GetBreachesAsync_WithValidInput_Succeeds()
    {
      using (var httpClient = new HttpClient(new MockHttpMessageHandler()))
      using (var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient))
      {
        var result = await c.GetBreachesAsync("test@example.com");

        Assert.NotNull(result);
        Assert.NotEmpty(result);
      }
    }

    [Fact]
    public async Task GetBreachesAsync_BreachModeAll_WithValidInput_Succeeds()
    {
      using (var httpClient = new HttpClient(new MockHttpMessageHandler()))
      using (var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient))
      {
        var result = await c.GetBreachesAsync("test@example.com", BreachMode.All);

        Assert.NotNull(result);
        Assert.NotEmpty(result);
      }
    }

    [Fact]
    public async Task GetBreachesAsync_CancellationToken_WithValidInput_Succeeds()
    {
      using (var httpClient = new HttpClient(new MockHttpMessageHandler()))
      using (var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient))
      {
        var result = await c.GetBreachesAsync("test@example.com", CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotEmpty(result);
      }
    }

    [Fact]
    public async Task GetBreachesAsync_BreachModeCancellationToken_WithValidInput_Succeeds()
    {
      using (var httpClient = new HttpClient(new MockHttpMessageHandler()))
      using (var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient))
      {
        var result = await c.GetBreachesAsync("test@example.com", BreachMode.All, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotEmpty(result);
      }
    }
  }
}
