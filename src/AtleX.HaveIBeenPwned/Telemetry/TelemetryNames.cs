// Copyright (c) Alex Kamsteeg (https://atlex.nl/)
// License: MIT (See LICENSE file)

using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace AtleX.HaveIBeenPwned.Telemetry;

/// <summary>
/// Represents the names of the different types of telemetry
/// </summary>
public static class TelemetryNames
{
  /// <summary>
  /// Gets the root name of the telemetry
  /// </summary>
  public const string RootName = "AtleX.HaveIBeenPwned";

  /// <summary>
  /// Represents the names of the activities
  /// </summary>
  public static class Activities
  {
    /// <summary>
    /// Gets the name of the Request <see cref="Activity"/>
    /// </summary>
    public const string Request = "Request";
  }

  /// <summary>
  /// Represents the names of the meters
  /// </summary>
  public static class Meters
  {
    /// <summary>
    /// Gets the name of the <see cref="Meter"/> with the total number of
    /// requests in the lifetime of the application
    /// </summary>
    public const string TotalNumberOfRequests = "TotalNumberOfRequests";
  }
}
