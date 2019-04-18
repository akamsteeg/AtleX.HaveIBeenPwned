using AtleX.HaveIBeenPwned.Communication.Http;
using System;
using System.Net.Http;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests.Communication.Http
{
  public class HttpHaveIBeenPwnedClientTests
  {
    [Fact]
    public void Ctor_WithNullValueForSettingsParam_Throws()
    {
      using (var httpClient = new HttpClient())
      {
        Assert.Throws<ArgumentNullException>(() => new HttpHaveIBeenPwnedClient(null, httpClient));
      }
    }

    [Fact]
    public void Ctor_WithNullValueForClientParam_Throws()
    {
      using (var httpClient = new HttpClient())
      {
        Assert.Throws<ArgumentNullException>(() => new HttpHaveIBeenPwnedClient(HttpHaveIBeenPwnedClientSettings.Default, null));
      }
    }

    [Fact]
    public void Ctor_WithValueForSettingsParam_DoesNotThrow()
    {
      using (var httpClient = new HttpClient())
      {
        new HttpHaveIBeenPwnedClient(HttpHaveIBeenPwnedClientSettings.Default, httpClient);
      }
    }
  }
}
