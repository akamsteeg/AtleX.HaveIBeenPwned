using AtleX.HaveIBeenPwned.Communication;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests.Communication
{
  public class RateLimitExceededExceptionTests
  {
    [Fact]
    public void Ctor_WithZeroValueForRetryAfterParam_Throws()
    {
      Assert.Throws<ArgumentOutOfRangeException>(() => new RateLimitExceededException(0));
    }

    [Fact]
    public void Ctor_WithLessThanZeroValueForRetryAfterParam_Throws()
    {
      Assert.Throws<ArgumentOutOfRangeException>(() => new RateLimitExceededException(-1));
    }

    [Fact]
    public void Ctor_WithMoreThanZeroValueForRetryAfterParam_DoesNotThrow()
    {
      var e = new RateLimitExceededException(1);
    }
  }
}
