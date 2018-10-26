using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests
{
  public class HaveIBeenPwnedClientTests_GetBreachesAsync
    : HaveIBeenPwnedClientTestsBase
  {
    [Fact]
    public async Task GetBreachesAsync_WithNullValueForAccountParameter_Throws()
    {
      var c = new HaveIBeenPwnedClient();

      await Assert.ThrowsAsync<ArgumentNullException>(async () => await c.GetBreachesAsync(null));
    }

    [Fact]
    public async Task GetBreachesAsync_CallAfterDispose_Throws()
    {
      var c = new HaveIBeenPwnedClient();
      c.Dispose();

      await Assert.ThrowsAsync<ObjectDisposedException>(async () => await c.GetBreachesAsync("DUMMY"));
    }
  }
}
