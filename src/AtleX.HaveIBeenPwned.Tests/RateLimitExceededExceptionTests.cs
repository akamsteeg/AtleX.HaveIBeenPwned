using SwissArmyKnife;
using System;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests;

public class RateLimitExceededExceptionTests
{
  [Theory]
  [InlineData(-1)]
  [InlineData(-1000)]
  public void Ctor_WithLessThanZeroValueForRetryAfterParam_Throws(int milliseconds)
  {
    var value = TimeSpan.FromMilliseconds(milliseconds);
    Assert.Throws<ArgumentOutOfRangeException>(() => new RateLimitExceededException(value));
  }

  [Theory]
  [InlineData(1)]
  [InlineData(1000)]
  public void Ctor_WithZeroOrMoreForRetryAfterParam_DoesNotThrow(int milliseconds)
  {
    var value = TimeSpan.FromMilliseconds(milliseconds);

    new RateLimitExceededException(value);
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
