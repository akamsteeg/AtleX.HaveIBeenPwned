// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

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

  [Theory]
  [InlineData("")]
  [InlineData("MESSAGE")]
  public void Ctor_WithMessage_SetsMessageProperty(string message)
  {
    var e = new HaveIBeenPwnedClientException(message);

    Assert.Equal(message, e.Message);
  }

  [Fact]
  public void Ctor_WithMessageAndNullInnerException_DoesNotThrow()
  {
    var e = new HaveIBeenPwnedClientException("TEST", null);
  }

  [Fact]
  public void Ctor_WithMessageAndInnerException_Succeed()
  {
    var e = new HaveIBeenPwnedClientException("TEST", new Exception("INNEREXCEPTION"));

    Assert.NotNull(e.InnerException);
    Assert.Equal("INNEREXCEPTION", e.InnerException.Message);
  }
}
