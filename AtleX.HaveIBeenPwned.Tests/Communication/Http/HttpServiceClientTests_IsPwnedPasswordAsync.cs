using AtleX.HaveIBeenPwned.Communication.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests.Communication.Http
{
  public class HttpServiceClientTests_IsPwnedPasswordAsync
  {
    [Fact]
    public async Task IsPwnedPasswordAsync_WithNullValueForPassword_Throws()
    {
      var c = new HttpServiceClient(new ClientSettings());

      await Assert.ThrowsAsync<ArgumentNullException>(() => c.IsPwnedPasswordAsync(null));
    }

    [Fact]
    public async Task IsPwnedPasswordAsync_AfterDispose_Throws()
    {
      var c = new HttpServiceClient(new ClientSettings());
      c.Dispose();

      await Assert.ThrowsAsync<ObjectDisposedException>(() => c.IsPwnedPasswordAsync("DUMMY"));
    }
  }
}
