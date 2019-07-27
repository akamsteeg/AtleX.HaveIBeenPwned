using System;
using System.Net.Http;
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
  }
}
