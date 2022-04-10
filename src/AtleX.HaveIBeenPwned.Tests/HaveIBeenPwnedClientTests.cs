using AtleX.HaveIBeenPwned.Tests.Mocks;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests;

public class HaveIBeenPwnedClientTests
  : HaveIBeenPwnedClientTestsBase
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
      Assert.Throws<ArgumentNullException>(() => new HaveIBeenPwnedClient(this.ClientSettings, null));
    }
  }

  [Fact]
  public void Ctor_WithValueForSettingsParam_DoesNotThrow()
  {
    using (new HaveIBeenPwnedClient(this.ClientSettings))
    {
      
    }
  }

  [Fact]
  public void Ctor_WithNullValueForApplicationNameInSettingsParam_ThrowsArgumentNullException()
  {
    var s = new HaveIBeenPwnedClientSettings()
    {
      ApplicationName = null,
    };

    Assert.Throws<ArgumentNullException>(() => new HaveIBeenPwnedClient(s));
  }

  [Fact]
  public void Ctor_WithNullValueForApplicationNameInSettingsParamAndValidHttpClient_ThrowsArgumentNullException()
  {
    using (var httpClient = new HttpClient())
    {
      var s = new HaveIBeenPwnedClientSettings()
      {
        ApplicationName = null,
      };

      Assert.Throws<ArgumentNullException>(() => new HaveIBeenPwnedClient(s, httpClient));
    }
  }

  [Fact]
  public void Ctor_OverridesUserAgentOfHttpClient()
  {
    using (var httpClient = new HttpClient())
    using (var client = new HaveIBeenPwnedClient(this.ClientSettings, httpClient))
    {
      Assert.Equal(this.ClientSettings.ApplicationName, httpClient.DefaultRequestHeaders.UserAgent.ToString());
    }
  }

  [Fact]
  public void Ctor_AppendsAplicationJsonMediaTypeToAcceptHeader()
  {
    using (var httpClient = new HttpClient())
    using (var client = new HaveIBeenPwnedClient(this.ClientSettings, httpClient))
    {
      Assert.Contains("application/json", httpClient.DefaultRequestHeaders.Accept.ToString());
    }
  }

  [Fact]
  public void Dispose_DoesNotThrow()
  {
    var c = new HaveIBeenPwnedClient(this.ClientSettings);
    
    c.Dispose();
  }

  [Fact]
  public async Task Dispose_WhenCreatedWithHttpClient_DoesNotDisposeInjectedHttpClient()
  {
    using (var httpClient = new HttpClient(new MockHttpMessageHandler()))
    {
      var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);
      c.Dispose();

      await Assert.ThrowsAsync<InvalidOperationException>(() => httpClient.GetAsync("/")); // Not an ObjectDisposedException
    }
  }

  [Fact]
  public void Object_ImplementsIHaveIBeenPwnedBreachesClient()
  {
    using (var httpClient = new HttpClient(new MockHttpMessageHandler()))
    {
      var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

      Assert.IsAssignableFrom<IHaveIBeenPwnedBreachesClient>(c);
    }
  }

  [Fact]
  public void Object_ImplementsIHaveIBeenPwnedPasswordClient()
  {
    using (var httpClient = new HttpClient(new MockHttpMessageHandler()))
    {
      var settings = this.ClientSettings;

      var c = new HaveIBeenPwnedClient(settings, httpClient);

      Assert.IsAssignableFrom<IHaveIBeenPwnedPasswordClient>(c);
    }
  }

  [Fact]
  public void Object_ImplementsIHaveIBeenPwnedPastesClient()
  {
    using (var httpClient = new HttpClient(new MockHttpMessageHandler()))
    {
      var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

      Assert.IsAssignableFrom<IHaveIBeenPwnedPastesClient>(c);
    }
  }

  [Fact]
  public void Object_ImplementsIHaveIBeenPwnedClient()
  {
    using (var httpClient = new HttpClient(new MockHttpMessageHandler()))
    {
      var c = new HaveIBeenPwnedClient(this.ClientSettings, httpClient);

      Assert.IsAssignableFrom<IHaveIBeenPwnedClient>(c);
    }
  }
}
