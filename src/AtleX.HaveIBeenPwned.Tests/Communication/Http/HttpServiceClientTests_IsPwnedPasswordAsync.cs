using AtleX.HaveIBeenPwned.Communication.Http;
using AtleX.HaveIBeenPwned.Tests.Mocks;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests.Communication.Http
{
  public class HttpServiceClientTests_IsPwnedPasswordAsync
  {
    [Fact]
    public async Task IsPwnedPasswordAsync_WithNullValueForPassword_Throws()
    {
      using (var httpClient = new HttpClient(HttpMessageHandlerMockFactory.Create()))
      using (var c = new HttpServiceClient(new ClientSettings(), httpClient))
      {
        await Assert.ThrowsAsync<ArgumentNullException>(() => c.IsPwnedPasswordAsync(null));
      }
    }

    [Fact]
    public async Task IsPwnedPasswordAsync_CancellationToken_WithNullValueForPassword_Throws()
    {
      using (var httpClient = new HttpClient(HttpMessageHandlerMockFactory.Create()))
      using (var c = new HttpServiceClient(new ClientSettings(), httpClient))
      {
        await Assert.ThrowsAsync<ArgumentNullException>(() => c.IsPwnedPasswordAsync(null, CancellationToken.None));
      }
    }

    [Fact]
    public async Task IsPwnedPasswordAsync_AfterDispose_Throws()
    {
      using (var httpClient = new HttpClient(HttpMessageHandlerMockFactory.Create()))
      {
        var c = new HttpServiceClient(new ClientSettings(), httpClient);
        c.Dispose();

        await Assert.ThrowsAsync<ObjectDisposedException>(() => c.IsPwnedPasswordAsync("DUMMY"));
      }
    }

    [Fact]
    public async Task IsPwnedPasswordAsync_CancellationToken_AfterDispose_Throws()
    {
      using (var httpClient = new HttpClient(HttpMessageHandlerMockFactory.Create()))
      {
        var c = new HttpServiceClient(new ClientSettings(), httpClient);
        c.Dispose();

        await Assert.ThrowsAsync<ObjectDisposedException>(() => c.IsPwnedPasswordAsync("DUMMY", CancellationToken.None));
      }
    }

    [Fact]
    public async Task IsPwnedPasswordAsync_WithValidInput_Succeeds()
    {
      using (var httpClient = new HttpClient(HttpMessageHandlerMockFactory.Create()))
      using (var c = new HttpServiceClient(new ClientSettings(), httpClient))
      {
        var result = await c.IsPwnedPasswordAsync("P@ssw0rd");

        Assert.True(result);
      }
    }

    [Fact]
    public async Task IsPwnedPasswordAsync_CancellationToken_WithValidInput_Succeeds()
    {
      using (var httpClient = new HttpClient(HttpMessageHandlerMockFactory.Create()))
      using (var c = new HttpServiceClient(new ClientSettings(), httpClient))
      {
        var result = await c.IsPwnedPasswordAsync("P@ssw0rd", CancellationToken.None);

        Assert.True(result);
      }
    }
  }
}
