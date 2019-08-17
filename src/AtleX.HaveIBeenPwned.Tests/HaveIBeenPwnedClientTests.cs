using AtleX.HaveIBeenPwned.Tests.Mocks;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests
{
  public class HaveIBeenPwnedClientTests
  {
    [Fact]
    public void Ctor_WithNullValueForSettingsParam_Throws()
    {
      using (var httpClient = new HttpClient())
      {
        Assert.Throws<ArgumentNullException>(() => new HaveIBeenPwnedClient(null, httpClient));
      }
    }

    [Fact]
    public void Ctor_WithNullValueForClientParam_Throws()
    {
      using (var httpClient = new HttpClient())
      {
        Assert.Throws<ArgumentNullException>(() => new HaveIBeenPwnedClient(HaveIBeenPwnedClientSettings.Default, null));
      }
    }

    [Fact]
    public void Ctor_WithValueForSettingsParam_DoesNotThrow()
    {
      using (var httpClient = new HttpClient())
      {
        new HaveIBeenPwnedClient(HaveIBeenPwnedClientSettings.Default, httpClient);
      }
    }

    [Fact]
    public void Ctor_WithoutValueForSettingsParamAndHttpClient_DoesNotThrow()
    {
      var c = new HaveIBeenPwnedClient(HaveIBeenPwnedClientSettings.Default);
    }

    [Fact]
    public void Dispose_DoesNotThrow()
    {
      var c = new HaveIBeenPwnedClient(HaveIBeenPwnedClientSettings.Default);
      
      c.Dispose();
    }

    [Fact]
    public async Task Dispose_WhenCreatedWithHttpClient_DoesNotDisposeHttpClient()
    {
      using (var httpClient = new HttpClient(new MockHttpMessageHandler()))
      {
        var c = new HaveIBeenPwnedClient(HaveIBeenPwnedClientSettings.Default, httpClient);

        await Assert.ThrowsAsync<InvalidOperationException>(() => httpClient.GetAsync("/")); // Not an ObjectDisposedException
      }
    }

    [Fact]
    public void Objects_ImplementsIHaveIBeenPwnedBreachesClient()
    {
      using (var httpClient = new HttpClient(new MockHttpMessageHandler()))
      {
        var c = new HaveIBeenPwnedClient(HaveIBeenPwnedClientSettings.Default, httpClient);

        Assert.IsAssignableFrom<IHaveIBeenPwnedBreachesClient>(c);
      }
    }

    [Fact]
    public void Objects_ImplementsIHaveIBeenPwnedPasswordClient()
    {
      using (var httpClient = new HttpClient(new MockHttpMessageHandler()))
      {
        var c = new HaveIBeenPwnedClient(HaveIBeenPwnedClientSettings.Default, httpClient);

        Assert.IsAssignableFrom<IHaveIBeenPwnedPasswordClient>(c);
      }
    }

    [Fact]
    public void Objects_ImplementsIHaveIBeenPwnedPastesClient()
    {
      using (var httpClient = new HttpClient(new MockHttpMessageHandler()))
      {
        var c = new HaveIBeenPwnedClient(HaveIBeenPwnedClientSettings.Default, httpClient);

        Assert.IsAssignableFrom<IHaveIBeenPwnedPastesClient>(c);
      }
    }

    [Fact]
    public void Objects_IHaveIBeenPwnedClient()
    {
      using (var httpClient = new HttpClient(new MockHttpMessageHandler()))
      {
        var c = new HaveIBeenPwnedClient(HaveIBeenPwnedClientSettings.Default, httpClient);

        Assert.IsAssignableFrom<IHaveIBeenPwnedClient>(c);
      }
    }
  }
}
