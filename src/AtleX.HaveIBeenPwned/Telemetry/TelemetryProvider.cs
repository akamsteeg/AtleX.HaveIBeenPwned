// Copyright (c) Alex Kamsteeg (https://atlex.nl/)
// License: MIT (See LICENSE file)

using System.Diagnostics.Metrics;
using System.Diagnostics;

namespace AtleX.HaveIBeenPwned.Telemetry;

/// <summary>
/// The provder for telemetry
/// </summary>
internal static class TelemetryProvider
{
  private static readonly string AssemblyVersion = typeof(IHaveIBeenPwnedClient).Assembly.GetName().Version!.ToString();

  private static readonly ActivitySource ActivitySource = new(TelemetryNames.RootName, AssemblyVersion);

  private static readonly Meter Meter = new (TelemetryNames.RootName, AssemblyVersion);

  private static readonly Counter<long> TotalRequests = Meter.CreateCounter<long>(TelemetryNames.Meters.TotalNumberOfRequests, "requests");

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