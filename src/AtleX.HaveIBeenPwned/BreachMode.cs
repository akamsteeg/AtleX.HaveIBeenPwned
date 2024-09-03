// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

using System;

namespace AtleX.HaveIBeenPwned;

/// <summary>
/// Represents the breach mode
/// </summary>
[Flags]
public enum BreachMode
{
#pragma warning disable S2346 // Flags enumerations zero-value members should be named "None"

  /// <summary>
  /// The default, verified and unverified, breaches
  /// </summary>
  Default,
#pragma warning restore S2346 // Flags enumerations zero-value members should be named "None"

  /// <summary>
  /// Exclude unverified breaches
  /// </summary>
  ExcludeUnverified = 1,

  /// <summary>
  /// Include all breaches
  /// </summary>
  All = Default
}
