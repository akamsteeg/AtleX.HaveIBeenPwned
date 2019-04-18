using AtleX.HaveIBeenPwned.Communication.Http;
using AtleX.HaveIBeenPwned.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests.Communication.Http
{
  public class HttpServiceClientTests_GetBreachesAsync
  {
    [Fact]
    public async Task GetBreachesAsync_WithNullValueForAccount_Throws()
    {
      using (var httpClient = new HttpClient(HttpMessageHandlerMockFactory.Create()))
      {
        var c = new HttpServiceClient(HttpClientSettings.Default, httpClient);

        await Assert.ThrowsAsync<ArgumentNullException>(() => c.GetBreachesAsync(null));
      }
    }

    [Fact]
    public async Task GetBreachesAsync_CancellationToken_WithNullValueForAccount_Throws()
    {
      using (var httpClient = new HttpClient(HttpMessageHandlerMockFactory.Create()))
      {
        var c = new HttpServiceClient(HttpClientSettings.Default, httpClient);

        await Assert.ThrowsAsync<ArgumentNullException>(() => c.GetBreachesAsync(null, CancellationToken.None));
      }
    }

    [Fact]
    public async Task GetBreachesAsync_AfterDispose_Throws()
    {
      using (var httpClient = new HttpClient(HttpMessageHandlerMockFactory.Create()))
      {
        var c = new HttpServiceClient(HttpClientSettings.Default, httpClient);
        c.Dispose();

        await Assert.ThrowsAsync<ObjectDisposedException>(() => c.GetBreachesAsync("DUMMY"));
      }
    }

    [Fact]
    public async Task GetBreachesAsync_CancellationToken_AfterDispose_Throws()
    {
      using (var httpClient = new HttpClient(HttpMessageHandlerMockFactory.Create()))
      {
        var c = new HttpServiceClient(HttpClientSettings.Default, httpClient);
        c.Dispose();

        await Assert.ThrowsAsync<ObjectDisposedException>(() => c.GetBreachesAsync("DUMMY", CancellationToken.None));
      }
    }

    [Fact]
    public async Task GetBreachesAsync_WithValidInput_Succeeds()
    {
      using (var httpClient = new HttpClient(HttpMessageHandlerMockFactory.Create()))
      using (var c = new HttpServiceClient(HttpClientSettings.Default, httpClient))
      {
        var result = await c.GetBreachesAsync("test@example.com");

        Assert.NotNull(result);
        Assert.NotEmpty(result);
      }
    }

    [Fact]
    public async Task GetBreachesAsync_CancellationToken_WithValidInput_Succeeds()
    {
      using (var httpClient = new HttpClient(HttpMessageHandlerMockFactory.Create()))
      using (var c = new HttpServiceClient(HttpClientSettings.Default, httpClient))
      {
        var result = await c.GetBreachesAsync("test@example.com", CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotEmpty(result);
      }
    }
  }
}
