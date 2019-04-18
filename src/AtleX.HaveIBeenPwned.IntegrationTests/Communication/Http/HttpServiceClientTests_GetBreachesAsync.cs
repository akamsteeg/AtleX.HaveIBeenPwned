using AtleX.HaveIBeenPwned.Communication.Http;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AtleX.HaveIBeenPwned.IntegrationTests.Communication.Http
{
  public class HttpServiceClientTests_GetBreachesAsync
  {
    [Fact]
    public async Task GetBreachesAsync_WithValidInput_DoesNotThrow()
    {
      using (var httpClient = new HttpClient())
      using (var c = new HttpServiceClient(HttpClientSettings.Default, httpClient))
      {
        var result = await c.GetBreachesAsync("test@example.com");

        Assert.NotNull(result);
      }
    }

    [Fact]
    public async Task GetBreachesAsync_WithValidInputAndVerifiedBreaches_DoesNotThrow()
    {
      using (var httpClient = new HttpClient())
      using (var c = new HttpServiceClient(HttpClientSettings.Default, httpClient))
      {
        var result = await c.GetBreachesAsync("test@example.com", BreachMode.IncludeUnverified);

        Assert.NotNull(result);
      }
    }

    [Fact]
    public async Task GetBreachesAsync_WithValidInputAndCancellationToken_DoesNotThrow()
    {
      using (var cancellationTokenSource = new CancellationTokenSource())
      using (var httpClient = new HttpClient())
      using (var c = new HttpServiceClient(HttpClientSettings.Default, httpClient))
      {
        var result = await c.GetBreachesAsync("test@example.com", cancellationTokenSource.Token);

        Assert.NotNull(result);
      }
    }

    [Fact]
    public async Task GetBreachesAsync_WithValidInputAndVerifiedBreachesAndCancellationToken_DoesNotThrow()
    {
      using (var cancellationTokenSource = new CancellationTokenSource())
      using (var httpClient = new HttpClient())
      using (var c = new HttpServiceClient(HttpClientSettings.Default, httpClient))
      {
        var result = await c.GetBreachesAsync("test@example.com", BreachMode.IncludeUnverified, cancellationTokenSource.Token);

        Assert.NotNull(result);
      }
    }
  }
}
