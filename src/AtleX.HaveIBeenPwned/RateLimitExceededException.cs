using Pitcher;
using System;

namespace AtleX.HaveIBeenPwned
{
  /// <summary>
  /// Represents a <see cref="HaveIBeenPwnedClientException"/> for exceeding the rate limit
  /// </summary>
  [Serializable]
  public sealed class RateLimitExceededException
    : HaveIBeenPwnedClientException
  {
    /// <summary>
    /// Gets the number of seconds to wait before retrying
    /// </summary>
    public TimeSpan RetryAfter
    {
      get;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="RateLimitExceededException"/>
    /// with the specified number of seconds to retry after
    /// </summary>
    /// <param name="retryAfter">
    /// The specified number of seconds to retry after
    /// </param>
    public RateLimitExceededException(TimeSpan retryAfter)
      : base("Rate limit exceeded")
    {
      Throw.ArgumentOutOfRange.When(retryAfter.TotalSeconds <= 0, nameof(retryAfter));

      this.RetryAfter = retryAfter;
    }
  }
}
