using AtleX.HaveIBeenPwned.Clients.Http;
using AtleX.HaveIBeenPwned.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests.Clients.Http
{
  public class HttpHaveIBeenPwnedClientTests_GetPastesAsync
  {
    [Fact]
    public async Task GetPastesAsync_WithNullValueForEmailAddress_Throws()
    {
      using (var httpClient = new HttpClient(HttpMessageHandlerMockFactory.Create()))
      using (var c = new HttpHaveIBeenPwnedClient(HttpHaveIBeenPwnedClientSettings.Default, httpClient))
      {
        await Assert.ThrowsAsync<ArgumentNullException>(() => c.GetPastesAsync(null));
      }
    }

    [Fact]
    public async Task GetPastesAsync_CancellationToken_WithNullValueForEmailAddress_Throws()
    {
      using (var httpClient = new HttpClient(HttpMessageHandlerMockFactory.Create()))
      using (var c = new HttpHaveIBeenPwnedClient(HttpHaveIBeenPwnedClientSettings.Default, httpClient))
      {
        await Assert.ThrowsAsync<ArgumentNullException>(() => c.GetPastesAsync(null, CancellationToken.None));
      }
    }

    [Fact]
    public async Task GetPastesAsync_AfterDispose_Throws()
    {
      using (var httpClient = new HttpClient(HttpMessageHandlerMockFactory.Create()))
      {
        var c = new HttpHaveIBeenPwnedClient(HttpHaveIBeenPwnedClientSettings.Default, httpClient);
        c.Dispose();

        await Assert.ThrowsAsync<ObjectDisposedException>(() => c.GetPastesAsync("DUMMY"));
      }
    }

    [Fact]
    public async Task GetPastesAsync_CancellationToken_AfterDispose_Throws()
    {
      using (var httpClient = new HttpClient(HttpMessageHandlerMockFactory.Create()))
      {
        var c = new HttpHaveIBeenPwnedClient(HttpHaveIBeenPwnedClientSettings.Default, httpClient);
        c.Dispose();

        await Assert.ThrowsAsync<ObjectDisposedException>(() => c.GetPastesAsync("DUMMY", CancellationToken.None));
      }
    }

    [Fact]
    public async Task GetPastesAsync_WithValidInput_Succeeds()
    {
      using (var httpClient = new HttpClient(HttpMessageHandlerMockFactory.Create()))
      using (var c = new HttpHaveIBeenPwnedClient(HttpHaveIBeenPwnedClientSettings.Default, httpClient))
      {
        var result = await c.GetPastesAsync("test@example.com");

        Assert.NotNull(result);
        Assert.NotEmpty(result);
      }
    }

    [Fact]
    public async Task GetPastesAsync_CancellationToken_WithValidInput_Succeeds()
    {
      using (var httpClient = new HttpClient(HttpMessageHandlerMockFactory.Create()))
      using (var c = new HttpHaveIBeenPwnedClient(HttpHaveIBeenPwnedClientSettings.Default, httpClient))
      {
        var result = await c.GetPastesAsync("test@example.com", CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotEmpty(result);
      }
    }
  }
}
