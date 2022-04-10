using SwissArmyKnife;
using System;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests;

public class RateLimitExceededExceptionTests
{
  [Fact]
  public void Ctor_WithLessThanZeroValueForRetryAfterParam_Throws()
  {
    Assert.Throws<ArgumentOutOfRangeException>(() => new RateLimitExceededException(-1.Seconds()));
  }

  [Fact]
  public void Ctor_WithExactlyZeroAsValueForRetryAfterParam_DoesNotThrow()
  {
    new RateLimitExceededException(0.Seconds());
  }

  [Fact]
  public void Ctor_WithMoreThanZeroValueForRetryAfterParam_DoesNotThrow()
  {
    new RateLimitExceededException(1.Seconds());
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

    Assert.Equal($"Rate limit exceeded, retry after {testValue.TotalSeconds} seconds", e.Message);
  }

  [Fact]
  public void Is_HaveIBeenPwnedClientException()
  {
    var e = new RateLimitExceededException(1.Seconds());

    Assert.IsAssignableFrom<HaveIBeenPwnedClientException>(e);
  }
}
