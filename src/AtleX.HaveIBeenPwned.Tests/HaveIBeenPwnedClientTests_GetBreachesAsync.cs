using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests
{
  public class HaveIBeenPwnedClientTests_GetBreachesAsync
    : HaveIBeenPwnedClientTestsBase
  {
    [Fact]
    public async Task GetBreachesAsync_WithNullValueForAccount_Throws()
    {
      var c = new HaveIBeenPwnedClient();

      await Assert.ThrowsAsync<ArgumentNullException>(async () => await c.GetBreachesAsync(null));
    }

    [Fact]
    public async Task GetBreachesAsync_CancellationToken_WithNullValueForAccount_Throws()
    {
      var c = new HaveIBeenPwnedClient();

      await Assert.ThrowsAsync<ArgumentNullException>(async () => await c.GetBreachesAsync(null, CancellationToken.None));
    }

    [Fact]
    public async Task GetBreachesAsync_CallAfterDispose_Throws()
    {
      var c = new HaveIBeenPwnedClient();
      c.Dispose();

      await Assert.ThrowsAsync<ObjectDisposedException>(async () => await c.GetBreachesAsync("DUMMY"));
    }

    [Fact]
    public async Task GetBreachesAsync_CancellationToken_CallAfterDispose_Throws()
    {
      var c = new HaveIBeenPwnedClient();
      c.Dispose();

      await Assert.ThrowsAsync<ObjectDisposedException>(async () => await c.GetBreachesAsync("DUMMY", CancellationToken.None));
    }

    [Fact]
    public async Task GetBreachesAsync_WithValidValue_DoesNotThrow()
    {
      var ic = CreateServiceClient();
      var c = new HaveIBeenPwnedClient(ic);

      var result = await c.GetBreachesAsync("DUMMY");

      Assert.NotNull(result);
    }

    [Fact]
    public async Task GetBreachesAsync_CancellationToken_WithValidValue_DoesNotThrow()
    {
      var ic = CreateServiceClient();
      var c = new HaveIBeenPwnedClient(ic);

      var result = await c.GetBreachesAsync("DUMMY", CancellationToken.None);

      Assert.NotNull(result);
    }
  }
}
