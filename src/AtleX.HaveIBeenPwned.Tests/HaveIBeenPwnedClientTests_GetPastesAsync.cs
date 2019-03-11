using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests
{
  public class HaveIBeenPwnedClientTests_GetPastesAsync
    : HaveIBeenPwnedClientTestsBase
  {
    [Fact]
    public async Task GetPastesAsync_WithNullValueForEmailAddress_Throws()
    {
      var c = new HaveIBeenPwnedClient();

      await Assert.ThrowsAsync<ArgumentNullException>(async () => await c.GetPastesAsync(null));
    }

    [Fact]
    public async Task GetPastesAsync_CancellationToken_WithNullValueForEmailAddress_Throws()
    {
      var c = new HaveIBeenPwnedClient();

      await Assert.ThrowsAsync<ArgumentNullException>(async () => await c.GetPastesAsync(null, CancellationToken.None));
    }

    [Fact]
    public async Task GetPastesAsync_CallAfterDispose_Throws()
    {
      var c = new HaveIBeenPwnedClient();
      c.Dispose();

      await Assert.ThrowsAsync<ObjectDisposedException>(async () => await c.GetPastesAsync("DUMMY"));
    }

    [Fact]
    public async Task GetPastesAsync_CancellationToken_CallAfterDispose_Throws()
    {
      var c = new HaveIBeenPwnedClient();
      c.Dispose();

      await Assert.ThrowsAsync<ObjectDisposedException>(async () => await c.GetPastesAsync("DUMMY", CancellationToken.None));
    }

    [Fact]
    public async Task GetPastesAsync_WithValidValue_DoesNotThrow()
    {
      var ic = CreateServiceClient();
      var c = new HaveIBeenPwnedClient(new ClientSettings(), ic);

      var result = await c.GetPastesAsync("DUMMY");

      Assert.NotNull(result);
    }

    [Fact]
    public async Task GetPastesAsync_CancellationToken_WithValidValue_DoesNotThrow()
    {
      var ic = CreateServiceClient();
      var c = new HaveIBeenPwnedClient(new ClientSettings(), ic);

      var result = await c.GetPastesAsync("DUMMY", CancellationToken.None);

      Assert.NotNull(result);
    }
  }
}
