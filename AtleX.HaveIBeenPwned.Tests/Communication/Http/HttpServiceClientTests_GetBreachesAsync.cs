using AtleX.HaveIBeenPwned.Communication.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests.Communication.Http
{
  public class HttpServiceClientTests_GetBreachesAsync
  {
    [Fact]
    public async Task GetBreachesAsync_WithNullValueForAccount_Throws()
    {
      var c = new HttpServiceClient(new ClientSettings());

      await Assert.ThrowsAsync<ArgumentNullException>(() => c.GetBreachesAsync(null));
    }

    [Fact]
    public async Task GetBreachesAsync_AfterDispose_Throws()
    {
      var c = new HttpServiceClient(new ClientSettings());
      c.Dispose();

      await Assert.ThrowsAsync<ObjectDisposedException>(() => c.GetBreachesAsync("DUMMY"));
    }
  }
}
