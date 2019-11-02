using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AtleX.HaveIBeenPwned.IntegrationTests
{
  public class HaveIBeenPwnedClientTests_GetPastesAsync
    : HaveIBeenPwnedClientIntegrationTestsBase
  {
    [Fact]
    public async Task GetPastesAsync_WithValidInput_ReturnsResults()
    {
      using (var httpClient = new HttpClient())
      using (var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient))
      {
        var result = await c.GetPastesAsync("test@example.com");

        Assert.NotNull(result);
        Assert.NotEmpty(result);
      }
    }

    [Fact]
    public async Task GetPastesAsync_WithUnknownEmail_DoesNotThrow()
    {
      using (var httpClient = new HttpClient())
      using (var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient))
      {
        var result = await c.GetPastesAsync("random@example.com");

        Assert.NotNull(result);
        Assert.Empty(result);
      }
    }

    [Fact]
    public async Task GetPastesAsync_WithValidInputAndCancellationToken_ReturnsResults()
    {
      using (var cancellationTokenSource = new CancellationTokenSource())
      using (var httpClient = new HttpClient())
      using (var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient))
      {
        var result = await c.GetPastesAsync("test@example.com", cancellationTokenSource.Token);

        Assert.NotNull(result);
        Assert.NotEmpty(result);
      }
    }

    [Fact]
    public async Task GetPastesAsync_WithInvalidApiKey_ThrowsInvalidApiKeyException()
    {
      var settings = new HaveIBeenPwnedClientSettings()
      {
        ApiKey = "DUMMYAPIKEY",
      };

      using (var cancellationTokenSource = new CancellationTokenSource())
      using (var httpClient = new HttpClient())
      using (var c = new HaveIBeenPwnedClient(settings, httpClient))
      {
        await Assert.ThrowsAsync<InvalidApiKeyException>(() => c.GetPastesAsync("test@example.com"));
      }
    }

    [Fact]
    public async Task GetPastesAsync_WithCancellationTokenAndInvalidApiKey_ThrowsInvalidApiKeyException()
    {
      var settings = new HaveIBeenPwnedClientSettings()
      {
        ApiKey = "DUMMYAPIKEY",
      };

      using (var cancellationTokenSource = new CancellationTokenSource())
      using (var httpClient = new HttpClient())
      using (var c = new HaveIBeenPwnedClient(settings, httpClient))
      {
        await Assert.ThrowsAsync<InvalidApiKeyException>(() => c.GetPastesAsync("test@example.com", cancellationTokenSource.Token));
      }
    }

    [Fact]
    public async Task GetPastesAsync_WithUnknownEmailAndCancellationToken_DoesNotThrow()
    {
      using (var cancellationTokenSource = new CancellationTokenSource())
      using (var httpClient = new HttpClient())
      using (var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient))
      {
        var result = await c.GetPastesAsync("random@example.com", cancellationTokenSource.Token);

        Assert.NotNull(result);
        Assert.Empty(result);
      }
    }
  }
}
