using AtleX.HaveIBeenPwned.Communication.Http;
using AtleX.HaveIBeenPwned.IntegrationTests.XUnit;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace AtleX.HaveIBeenPwned.IntegrationTests.Communication.Http
{
  public class HttpServiceClientTests_IsPwnedPassword
  {
    [RunnableInDebugOnlyAttribute]
    public async Task IsPwnedPassword_WithValidKnownInput_ReturnsTrue()
    {
      using (var httpClient = new HttpClient())
      using (var c = new HttpServiceClient(new ClientSettings(), httpClient))
      {
        var result = await c.IsPwnedPasswordAsync("1234");

        Assert.True(result);
      }
    }

    [RunnableInDebugOnlyAttribute]
    public async Task IsPwnedPassword_WithValidUnknownInput_ReturnsFalse()
    {
      using (var httpClient = new HttpClient())
      using (var c = new HttpServiceClient(new ClientSettings(), httpClient))
      {
        var result = await c.IsPwnedPasswordAsync(Guid.NewGuid().ToString());

        Assert.False(result);
      }
    }
  }
}
