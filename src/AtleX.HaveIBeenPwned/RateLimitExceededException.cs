// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

using System;
using System.Diagnostics;

namespace AtleX.HaveIBeenPwned;

/// <summary>
/// Represents a <see cref="HaveIBeenPwnedClientException"/> for exceeding the
/// rate limit
/// </summary>
/// <remarks>
/// <para>
/// Initializes a new instance of <see cref="RateLimitExceededException"/>
/// with the <see cref="TimeSpan"/> to wait before retrying
/// </para>
/// <para>
/// It is recommended to not retry after the timespan specified in <see
/// cref="RetryAfter"/> but wait an additional few milliseconds. <see
/// href="https://haveibeenpwned.com/API/v3#RateLimiting">The
/// documentation</see> recommends waiting an additional 100 milliseconds.
/// </para>
/// </remarks>
/// <param name="retryAfter">
/// The <see cref="TimeSpan"/> to wait before retrying
/// </param>
#pragma warning disable RCS1194 // Implement exception constructors.
// You exceeded the rate limit and have all the info you need in this
// exception. Therefor there's no need to override constructors.
[DebuggerDisplay("Retry after {RetryAfter.TotalMilliseconds} ms.")]
public sealed class RateLimitExceededException(TimeSpan retryAfter)
#pragma warning restore RCS1194 // Implement exception constructors.
  : HaveIBeenPwnedClientException("Rate limit exceeded")
{
  /// <summary>
  /// Gets the <see cref="TimeSpan"/> to wait before retrying
  /// </summary>
  /// <remarks>
  /// It is recommended to not retry immediately but wait an
  /// additional few milliseconds. <see
  /// href="https://haveibeenpwned.com/API/v3#RateLimiting">The
  /// documentation</see> recommends waiting an additional 100 milliseconds.
  /// </remarks>
  public TimeSpan RetryAfter
  {
    get;
  } = (retryAfter.TotalSeconds >= 0d) ? retryAfter : TimeSpan.Zero;
}
