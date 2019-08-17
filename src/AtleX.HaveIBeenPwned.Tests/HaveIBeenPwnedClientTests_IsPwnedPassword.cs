using AtleX.HaveIBeenPwned.Tests.Mocks;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests
{
  public class HaveIBeenPwnedClientTests_IsPwnedPassword
    : HaveIBeenPwnedClientTestsBase
  {
    [Fact]
    public async Task IsPwnedPasswordAsync_WithNullValueForPassword_Throws()
    {
      using (var httpClient = new HttpClient(new MockHttpMessageHandler()))
      using (var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient))
      {
        await Assert.ThrowsAsync<ArgumentNullException>(() => c.IsPwnedPasswordAsync(null));
      }
    }

    [Fact]
    public async Task IsPwnedPasswordAsync_CancellationToken_WithNullValueForPassword_Throws()
    {
      using (var httpClient = new HttpClient(new MockHttpMessageHandler()))
      using (var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient))
      {
        await Assert.ThrowsAsync<ArgumentNullException>(() => c.IsPwnedPasswordAsync(null, CancellationToken.None));
      }
    }

    [Fact]
    public async Task IsPwnedPasswordAsync_AfterDispose_Throws()
    {
      using (var httpClient = new HttpClient(new MockHttpMessageHandler()))
      {
        var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);
        c.Dispose();

        await Assert.ThrowsAsync<ObjectDisposedException>(() => c.IsPwnedPasswordAsync("DUMMY"));
      }
    }

    [Fact]
    public async Task IsPwnedPasswordAsync_CancellationToken_AfterDispose_Throws()
    {
      using (var httpClient = new HttpClient(new MockHttpMessageHandler()))
      {
        var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);
        c.Dispose();

        await Assert.ThrowsAsync<ObjectDisposedException>(() => c.IsPwnedPasswordAsync("DUMMY", CancellationToken.None));
      }
    }
    
    [Fact]
    public async Task IsPwnedPasswordAsync_RateLimitExceeded_ThrowsRateLimitExceededException()
    {
      using (var cancellationTokenSource = new CancellationTokenSource())
      using (var httpClient = new HttpClient(new MockErroringHttpMessageHandler(System.Net.HttpStatusCode.TooManyRequests)))
      using (var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient))
      {
        await Assert.ThrowsAsync<RateLimitExceededException>(() => c.IsPwnedPasswordAsync("DUMMY"));
      }
    }

    [Fact]
    public async Task IsPwnedPasswordAsync_WithCancellationToken_RateLimitExceeded_ThrowsRateLimitExceededException()
    {
      using (var cancellationTokenSource = new CancellationTokenSource())
      using (var httpClient = new HttpClient(new MockErroringHttpMessageHandler(System.Net.HttpStatusCode.TooManyRequests)))
      using (var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient))
      {
        await Assert.ThrowsAsync<RateLimitExceededException>(() => c.IsPwnedPasswordAsync("DUMMY", CancellationToken.None));
      }
    }

    [Fact]
    public async Task IsPwnedPasswordAsync_KnownInput_Succeeds()
    {
      using (var httpClient = new HttpClient(new MockHttpMessageHandler()))
      using (var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient))
      {
        var result = await c.IsPwnedPasswordAsync("P@ssw0rd");

        Assert.True(result);
      }
    }

    [Fact]
    public async Task IsPwnedPasswordAsync_CancellationToken_KnownInput_Succeeds()
    {
      using (var httpClient = new HttpClient(new MockHttpMessageHandler()))
      using (var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient))
      {
        var result = await c.IsPwnedPasswordAsync("P@ssw0rd", CancellationToken.None);

        Assert.True(result);
      }
    }
    
    [Fact]
    public async Task IsPwnedPasswordAsync_UnknownInputSucceeds()
    {
      using (var httpClient = new HttpClient(new MockHttpMessageHandler()))
      using (var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient))
      {
        var result = await c.IsPwnedPasswordAsync("UNKNOWN");

        Assert.False(result);
      }
    }

    [Fact]
    public async Task IsPwnedPasswordAsync_CancellationToken_UnknownInputSucceeds()
    {
      using (var httpClient = new HttpClient(new MockHttpMessageHandler()))
      using (var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient))
      {
        var result = await c.IsPwnedPasswordAsync("UNKNOWN", CancellationToken.None);

        Assert.False(result);
      }
    }
  }
}
