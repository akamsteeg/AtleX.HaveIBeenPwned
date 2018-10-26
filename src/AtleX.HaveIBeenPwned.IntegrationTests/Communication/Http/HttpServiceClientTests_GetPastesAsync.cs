using AtleX.HaveIBeenPwned.Communication.Http;
using AtleX.HaveIBeenPwned.IntegrationTests.XUnit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AtleX.HaveIBeenPwned.IntegrationTests.Communication.Http
{
  public class HttpServiceClientTests_GetPastesAsync
  {
    [RunnableInDebugOnlyAttribute]
    public async Task GetPastesAsync_WithValidInput_ReturnsResults()
    {
      var c = new HttpServiceClient(new ClientSettings());

      var result = await c.GetPastesAsync("test@example.com");

      Assert.NotNull(result);
      Assert.NotEmpty(result);
    }

    [RunnableInDebugOnlyAttribute]
    public async Task GetPastesAsync_WithUnknownEmail_DoesNotThrow()
    {
      var c = new HttpServiceClient(new ClientSettings());

      var result = await c.GetPastesAsync("random@example.com");

      Assert.NotNull(result);
      Assert.Empty(result);
    }
  }
}
