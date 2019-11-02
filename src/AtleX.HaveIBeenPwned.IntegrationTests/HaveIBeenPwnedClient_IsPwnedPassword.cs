using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AtleX.HaveIBeenPwned.IntegrationTests
{
  public class HaveIBeenPwnedClientTests_IsPwnedPassword
    : HaveIBeenPwnedClientIntegrationTestsBase
  {
    [Fact]
    public async Task IsPwnedPassword_WithValidKnownInput_ReturnsTrue()
    {
      using (var httpClient = new HttpClient())
      using (var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient))
      {
        var result = await c.IsPwnedPasswordAsync("1234");

        Assert.True(result);
      }
    }

    [Fact]
    public async Task IsPwnedPassword_WithValidUnknownInput_ReturnsFalse()
    {
      using (var httpClient = new HttpClient())
      using (var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient))
      {
        var result = await c.IsPwnedPasswordAsync(Guid.NewGuid().ToString());

        Assert.False(result);
      }
    }

    [Fact]
    public async Task IsPwnedPassword_WithValidKnownInputAndCancellationToken__ReturnsTrue()
    {
      using (var cancellationTokenSource = new CancellationTokenSource())
      using (var httpClient = new HttpClient())
      using (var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient))
      {
        var result = await c.IsPwnedPasswordAsync("1234", cancellationTokenSource.Token);

        Assert.True(result);
      }
    }

    [Fact]
    public async Task IsPwnedPassword_WithValidUnknownInputAndCancellationToken__ReturnsFalse()
    {
      using (var cancellationTokenSource = new CancellationTokenSource())
      using (var httpClient = new HttpClient())
      using (var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient))
      {
        var result = await c.IsPwnedPasswordAsync(Guid.NewGuid().ToString(), cancellationTokenSource.Token);

        Assert.False(result);
      }
    }
  }
}
