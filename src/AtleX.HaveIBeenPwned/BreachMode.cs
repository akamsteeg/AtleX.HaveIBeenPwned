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
    /// The default, verified and unverified, breaches
    /// </summary>
    Default,

    /// <summary>
    /// Exclude unverified breaches
    /// </summary>
    ExcludeUnverified = 1,

    /// <summary>
    /// Include all breaches
    /// </summary>
    All = Default
  }
}
