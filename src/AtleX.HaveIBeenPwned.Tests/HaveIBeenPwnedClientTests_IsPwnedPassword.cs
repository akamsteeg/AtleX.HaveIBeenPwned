using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests
{
  public class HaveIBeenPwnedClientTests_IsPwnedPassword
    : HaveIBeenPwnedClientTestsBase
  {
    [Fact]
    public async Task IsPwnedPasswordAsync_WithNullValueForPassword_Throws()
    {
      var c = new HaveIBeenPwnedClient();

      await Assert.ThrowsAsync<ArgumentNullException>(async () => await c.IsPwnedPasswordAsync(null));
    }

    [Fact]
    public async Task IsPwnedPasswordAsync_CancellationToken_WithNullValueForPassword_Throws()
    {
      var c = new HaveIBeenPwnedClient();

      await Assert.ThrowsAsync<ArgumentNullException>(async () => await c.IsPwnedPasswordAsync(null, CancellationToken.None));
    }

    [Fact]
    public async Task IsPwnedPasswordAsync_CallAfterDispose_Throws()
    {
      var c = new HaveIBeenPwnedClient();
      c.Dispose();

      await Assert.ThrowsAsync<ObjectDisposedException>(async () => await c.IsPwnedPasswordAsync("DUMMY"));
    }

    [Fact]
    public async Task IsPwnedPasswordAsync_CancellationToken_CallAfterDispose_Throws()
    {
      var c = new HaveIBeenPwnedClient();
      c.Dispose();

      await Assert.ThrowsAsync<ObjectDisposedException>(async () => await c.IsPwnedPasswordAsync("DUMMY", CancellationToken.None));
    }

    [Fact]
    public async Task IsPwnedPasswordAsync_WithValidValue_DoesNotThrow()
    {
      var ic = CreateServiceClient();
      var c = new HaveIBeenPwnedClient(ic);

      var result = await c.IsPwnedPasswordAsync("DUMMY");

      Assert.True(result);
    }

    [Fact]
    public async Task IsPwnedPasswordAsync_CancellationToken_WithValidValue_DoesNotThrow()
    {
      var ic = CreateServiceClient();
      var c = new HaveIBeenPwnedClient(ic);

      var result = await c.IsPwnedPasswordAsync("DUMMY", CancellationToken.None);

      Assert.True(result);
    }
  }
}
