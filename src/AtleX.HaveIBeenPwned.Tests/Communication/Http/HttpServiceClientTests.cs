using AtleX.HaveIBeenPwned.Communication.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests.Communication.Http
{
  public class HttpServiceClientTests
  {
    [Fact]
    public void Ctor_WithNullValueForSettingsParam_Throws()
    {
      using (var httpClient = new HttpClient())
      {
        Assert.Throws<ArgumentNullException>(() => new HttpServiceClient(null, httpClient));
      }
    }

    [Fact]
    public void Ctor_WithNullValueForClientParam_Throws()
    {
      using (var httpClient = new HttpClient())
      {
        Assert.Throws<ArgumentNullException>(() => new HttpServiceClient(HttpClientSettings.Default, null));
      }
    }

    [Fact]
    public void Ctor_WithValueForSettingsParam_DoesNotThrow()
    {
      using (var httpClient = new HttpClient())
      {
        new HttpServiceClient(HttpClientSettings.Default, httpClient);
      }
    }
  }
}
