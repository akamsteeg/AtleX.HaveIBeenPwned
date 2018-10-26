using AtleX.HaveIBeenPwned.Communication.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests.Communication.Http
{
  public class HttpServiceClientTests
  {
    [Fact]
    public void Ctor_WithNullValueForSettingsParam_Throws()
    {
      Assert.Throws<ArgumentNullException>(() => new HttpServiceClient(null));
    }

    [Fact]
    public void Ctor_WithValueForSettingsParam_DoesNotThrow()
    {
      new HttpServiceClient(ClientSettings.Default);
    }
  }
}
