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
    /// with the <see cref="TimeSpan"/> to wait before retrying
    /// </summary>
    /// <param name="retryAfter">
    /// The <see cref="TimeSpan"/> to wait before retrying
    /// </param>
    public RateLimitExceededException(TimeSpan retryAfter)
      : base($"Rate limit exceeded, retry after {retryAfter.TotalSeconds} seconds")
    {
      Throw.ArgumentOutOfRange.When(retryAfter.TotalSeconds < 0d, nameof(retryAfter));

      this.RetryAfter = retryAfter;
    }
  }
}
