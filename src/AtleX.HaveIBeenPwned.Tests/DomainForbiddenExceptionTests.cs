// Copyright (c) Alex Kamsteeg (https://atlex.nl/)
// License: MIT (See LICENSE file)

using Xunit;

namespace AtleX.HaveIBeenPwned.Tests;
public class DomainForbiddenExceptionTests
{
  [Fact]
  public void Message_ContainsCorrectText()
  {
    var e = new DomainForbiddenException();

    Assert.Equal("Access to the domain is forbidden with the specified API key", e.Message);
  }

  [Fact]
  public void Is_HaveIBeenPwnedClientException()
  {
    var e = new DomainForbiddenException();

    Assert.IsAssignableFrom<HaveIBeenPwnedClientException>(e);
  }
}
