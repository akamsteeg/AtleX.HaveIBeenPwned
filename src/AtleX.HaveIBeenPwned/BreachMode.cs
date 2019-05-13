using System;

namespace AtleX.HaveIBeenPwned
{
  /// <summary>
  /// Represents the breach mode
  /// </summary>
  [Flags]
  public enum BreachMode
  {
    /// <summary>
    /// The default, verified, breaches
    /// </summary>
    Default,

    /// <summary>
    /// Include unverified breaches
    /// </summary>
    IncludeUnverified = 1,

    /// <summary>
    /// Include all breaches
    /// </summary>
    All = IncludeUnverified
  }
}
