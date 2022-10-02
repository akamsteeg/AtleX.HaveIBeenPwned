using Pitcher;
using SwissArmyKnife;
using System;

namespace AtleX.HaveIBeenPwned;

/// <summary>
/// Represents a <see cref="HaveIBeenPwnedClientException"/> for exceeding the rate limit
/// </summary>
#pragma warning disable RCS1194 // Implement exception constructors.
// You exceeded the rate limit and have all the info you need in this
// exception. Therefor there's no need to override constructors.
public sealed class RateLimitExceededException
#pragma warning restore RCS1194 // Implement exception constructors.
  : HaveIBeenPwnedClientException
{
  /// <summary>
  /// Gets the <see cref="TimeSpan"/> to wait before retrying
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
    : base("Rate limit exceeded")
  {
    this.RetryAfter = (retryAfter.TotalSeconds >= 0) ? retryAfter : 0.Seconds();
  }
}
