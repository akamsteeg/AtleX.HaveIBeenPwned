using Pitcher;

namespace AtleX.HaveIBeenPwned.Communication
{
  /// <summary>
  /// Represents a <see cref="HaveIBeenPwnedClientException"/> for exceeding the rate limit
  /// </summary>
  public sealed class RateLimitExceededException
    : HaveIBeenPwnedClientException
  {
    /// <summary>
    /// Gets the number of seconds to wait before retrying
    /// </summary>
    public int RetryAfter
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
    public RateLimitExceededException(int retryAfter)
      : base("Rate limit exceeded")
    {
      Throw.ArgumentOutOfRange.When(retryAfter <= 0, nameof(retryAfter));

      this.RetryAfter = retryAfter;
    }
  }
}
