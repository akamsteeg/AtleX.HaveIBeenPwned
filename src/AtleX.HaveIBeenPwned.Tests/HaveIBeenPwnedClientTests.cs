using AtleX.HaveIBeenPwned.Communication;
using AtleX.HaveIBeenPwned.Data;
using AtleX.HaveIBeenPwned.Tests.Mocks;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests
{
  public class HaveIBeenPwnedClientTests
  {
    [Fact]
    public void Ctor_WithNullValueForClientSettingsParam_Throws()
    {
      Assert.Throws<ArgumentNullException>(() => new HaveIBeenPwnedClient(null));
    }

    [Fact]
    public void Ctor_WithNullValueForServiceClientParam_Throws()
    {
      Assert.Throws<ArgumentNullException>(() => new HaveIBeenPwnedClient(null));
    }

    [Fact]
    public void Dispose_DoesNotDisposeInjectedDisposableClient()
    {
      var disposableFakeClient = new DisposableFakeClient();

      using (var hibpClient = new HaveIBeenPwnedClient(disposableFakeClient))
      {
        // nop
      }

      Assert.False(disposableFakeClient.IsDisposed);
    }

    [Fact]
    public void Dispose_WithNonDisposableInjectedClient_DoesNotThrow()
    {
      var fakeClient = new Mock<IHaveIBeenPwnedClient>().Object;

      Assert.False(fakeClient is IDisposable);

      using (var hibpClient = new HaveIBeenPwnedClient(fakeClient))
      {
        // nop
      }
    }
  }
}
