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
    /// No extra breaches
    /// </summary>
    None,

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
