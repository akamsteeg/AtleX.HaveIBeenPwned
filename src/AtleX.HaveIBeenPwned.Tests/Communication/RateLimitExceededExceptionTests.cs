using AtleX.HaveIBeenPwned.Communication;
using System;
using System.Linq;
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

    [Fact]
    public void RetryAfter_Equals_CtorRetryAfterParameter()
    {
      const int testValue = 100;
      var e = new RateLimitExceededException(testValue);

      Assert.Equal(testValue, e.RetryAfter);
    }

    [Fact]
    public void Is_Serializable()
    {
      var attributes = typeof(RateLimitExceededException).GetCustomAttributes(inherit: false);

      var hasSerializableAttribute = attributes.Any(a => a.GetType() == typeof(SerializableAttribute));

      Assert.True(hasSerializableAttribute);
    }
  }
}
