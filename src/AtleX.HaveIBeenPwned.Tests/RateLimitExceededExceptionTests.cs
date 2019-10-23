using AtleX.HaveIBeenPwned;
using SwissArmyKnife;
using System;
using System.Linq;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests
{
  public class RateLimitExceededExceptionTests
  {
    [Fact]
    public void Ctor_WithLessThanZeroValueForRetryAfterParam_Throws()
    {
      Assert.Throws<ArgumentOutOfRangeException>(() => new RateLimitExceededException(-1.Seconds()));
    }

    [Fact]
    public void Ctor_WithLessThanZeroValueForRetryAfterParam_DoesNotThrow()
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
    public void Is_Serializable()
    {
      var attributes = typeof(RateLimitExceededException).GetCustomAttributes(inherit: false);

      var hasSerializableAttribute = attributes.Any(a => a.GetType() == typeof(SerializableAttribute));

      Assert.True(hasSerializableAttribute);
    }
  }
}
