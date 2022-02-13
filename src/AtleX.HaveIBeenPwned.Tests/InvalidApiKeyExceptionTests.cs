using System;
using System.Linq;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests;

public class InvalidApiKeyExceptionTests
{
  [Fact]
  public void Message_ContainsCorrectText()
  {
    var e = new InvalidApiKeyException();

    Assert.Equal("The API key is unknown or expired", e.Message);
  }

  [Fact]
  public void Is_HaveIBeenPwnedClientException()
  {
    var e = new InvalidApiKeyException();

    Assert.IsAssignableFrom<HaveIBeenPwnedClientException>(e);
  }
}
