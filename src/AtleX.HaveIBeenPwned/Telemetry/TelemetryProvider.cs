// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace AtleX.HaveIBeenPwned.Telemetry;

/// <summary>
/// The provder for telemetry
/// </summary>
internal static class TelemetryProvider
{
  /// <summary>
  /// Gets the version of the library
  /// </summary>
  private static readonly string AssemblyVersion = typeof(IHaveIBeenPwnedClient).Assembly.GetName().Version!.ToString();

  /// <summary>
  /// Gets the <see cref="System.Diagnostics.ActivitySource"/> for tracing
  /// </summary>
  private static readonly ActivitySource ActivitySource = new(TelemetryNames.RootName, AssemblyVersion);

  /// <summary>
  /// Gets a <see cref="System.Diagnostics.Metrics.Meter"/> for counting values
  /// </summary>
  private static readonly Meter Meter = new(TelemetryNames.RootName, AssemblyVersion);

  /// <summary>
  /// Gets the <see cref="Counter{T}"/> for counting the total number of
  /// requests performed with all instances of <see
  /// cref="HaveIBeenPwnedClient"/> over the lifetime of the application
  /// </summary>
  private static readonly Counter<long> TotalRequests = Meter.CreateCounter<long>(TelemetryNames.Counters.TotalNumberOfRequests, "requests");

  /// <summary>
  /// Gets the <see cref="Activity"/> used for tracing the requests
  /// </summary>
  /// <returns>
  /// The <see cref="Activity"/> used for tracing requests, or null when tracing is disabled
  /// </returns>
  public static Activity? TraceRequest(string action)
  {
    var result = ActivitySource.StartActivity(TelemetryNames.Activities.Request, ActivityKind.Client);
    result?.AddTag("Action", action);

    return result;
  }

  /// <summary>
  /// Increments the <see cref="Counter{T}"/> with the total number of requests
  /// performed by one
  /// </summary>
  public static void IncrementTotalRequestsCounter() => TotalRequests.Add(1);
}