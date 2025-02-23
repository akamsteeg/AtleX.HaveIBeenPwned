// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

using System;
using SwissArmyKnife;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests;

public class RateLimitExceededExceptionTests
{
  [Theory]
  [InlineData(-1)]
  [InlineData(-1000)]
  public void Ctor_WithLessThanZeroValueForRetryAfterParam_SetRetryAfterToZero(int milliseconds)
  {
    var value = TimeSpan.FromMilliseconds(milliseconds);

    var e = new RateLimitExceededException(value);

    Assert.Equal(TimeSpan.Zero, e.RetryAfter);
  }

  [Theory]
  [InlineData(1)]
  [InlineData(1000)]
  public void Ctor_WithZeroOrMoreForRetryAfterParam_DoesNotThrow(int milliseconds)
  {
    var value = TimeSpan.FromMilliseconds(milliseconds);

    var e = new RateLimitExceededException(value);

    Assert.Equal(milliseconds, e.RetryAfter.TotalMilliseconds);
  }

  [Fact]
  public void RetryAfter_Equals_CtorRetryAfterParameter()
  {
    var testValue = 100.Seconds();
    var e = new RateLimitExceededException(testValue);

    Assert.Equal(testValue, e.RetryAfter);
  }

  [Fact]
  public void Message_ContainsCorrectText()
  {
    var testValue = 100.Seconds();
    var e = new RateLimitExceededException(testValue);

    Assert.Equal("Rate limit exceeded", e.Message);
  }

  [Fact]
  public void Is_HaveIBeenPwnedClientException()
  {
    var e = new RateLimitExceededException(1.Seconds());

    Assert.IsAssignableFrom<HaveIBeenPwnedClientException>(e);
  }
}
