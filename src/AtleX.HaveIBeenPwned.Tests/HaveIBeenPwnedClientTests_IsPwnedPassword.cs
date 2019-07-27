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
      using (var httpClient = new HttpClient(HttpMessageHandlerMockFactory.Create()))
      using (var c = new HaveIBeenPwnedClient(HaveIBeenPwnedClientSettings.Default, httpClient))
      {
        await Assert.ThrowsAsync<ArgumentNullException>(() => c.IsPwnedPasswordAsync(null));
      }
    }

    [Fact]
    public async Task IsPwnedPasswordAsync_CancellationToken_WithNullValueForPassword_Throws()
    {
      using (var httpClient = new HttpClient(HttpMessageHandlerMockFactory.Create()))
      using (var c = new HaveIBeenPwnedClient(HaveIBeenPwnedClientSettings.Default, httpClient))
      {
        await Assert.ThrowsAsync<ArgumentNullException>(() => c.IsPwnedPasswordAsync(null, CancellationToken.None));
      }
    }

    [Fact]
    public async Task IsPwnedPasswordAsync_AfterDispose_Throws()
    {
      using (var httpClient = new HttpClient(HttpMessageHandlerMockFactory.Create()))
      {
        var c = new HaveIBeenPwnedClient(HaveIBeenPwnedClientSettings.Default, httpClient);
        c.Dispose();

        await Assert.ThrowsAsync<ObjectDisposedException>(() => c.IsPwnedPasswordAsync("DUMMY"));
      }
    }

    [Fact]
    public async Task IsPwnedPasswordAsync_CancellationToken_AfterDispose_Throws()
    {
      using (var httpClient = new HttpClient(HttpMessageHandlerMockFactory.Create()))
      {
        var c = new HaveIBeenPwnedClient(HaveIBeenPwnedClientSettings.Default, httpClient);
        c.Dispose();

        await Assert.ThrowsAsync<ObjectDisposedException>(() => c.IsPwnedPasswordAsync("DUMMY", CancellationToken.None));
      }
    }

    [Fact]
    public async Task IsPwnedPasswordAsync_WithValidInput_Succeeds()
    {
      using (var httpClient = new HttpClient(HttpMessageHandlerMockFactory.Create()))
      using (var c = new HaveIBeenPwnedClient(HaveIBeenPwnedClientSettings.Default, httpClient))
      {
        var result = await c.IsPwnedPasswordAsync("P@ssw0rd");

        Assert.True(result);
      }
    }

    [Fact]
    public async Task IsPwnedPasswordAsync_CancellationToken_WithValidInput_Succeeds()
    {
      using (var httpClient = new HttpClient(HttpMessageHandlerMockFactory.Create()))
      using (var c = new HaveIBeenPwnedClient(HaveIBeenPwnedClientSettings.Default, httpClient))
      {
        var result = await c.IsPwnedPasswordAsync("P@ssw0rd", CancellationToken.None);

        Assert.True(result);
      }
    }
  }
}
