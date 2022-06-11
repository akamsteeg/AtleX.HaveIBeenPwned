using System;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests;

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
