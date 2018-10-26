using AtleX.HaveIBeenPwned.Communication;
using AtleX.HaveIBeenPwned.Data;
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
      Assert.Throws<ArgumentNullException>(() => new HaveIBeenPwnedClient(new ClientSettings(), null));
    }
  }
}
