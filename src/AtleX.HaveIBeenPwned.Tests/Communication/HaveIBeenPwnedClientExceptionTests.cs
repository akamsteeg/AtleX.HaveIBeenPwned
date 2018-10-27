using AtleX.HaveIBeenPwned.Communication;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests.Communication
{
  public class HaveIBeenPwnedClientExceptionTests
  {
    [Fact]
    public void Ctor_WithNoParams_DoesNotThrow()
    {
      var e = new HaveIBeenPwnedClientException();
    }

    [Fact]
    public void Ctor_WithEmptyMessage_DoesNotThrow()
    {
      var e = new HaveIBeenPwnedClientException("");
    }

    [Fact]
    public void Ctor_WithMessage_DoesNotThrow()
    {
      var e = new HaveIBeenPwnedClientException("TEST");
    }

    [Fact]
    public void Ctor_WithMessageAndNullInnerException_DoesNotThrow()
    {
      var e = new HaveIBeenPwnedClientException("TEST", null);
    }

    [Fact]
    public void Ctor_WithMessageAndInnerException_DoesNotThrow()
    {
      var e = new HaveIBeenPwnedClientException("TEST", new Exception());
    }
  }
}
