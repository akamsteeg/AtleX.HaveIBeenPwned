using System;

namespace AtleX.HaveIBeenPwned.IntegrationTests;
/// <summary>
/// Private settings that should never ever end up in source control
/// </summary>
internal static class PrivateSettings
{
  /// <summary>
  /// Gets the API key to use for integration tests that are requiring one
  /// </summary>
  /// <remarks>
  /// To use this without an environment variable, supply a value instead of an empty string.
  /// </remarks>
  public readonly static string ApiKey = Environment.GetEnvironmentVariable("IntegrationTests_ApiKey") ?? "";

  /// <summary>
  /// Gets the domain associated with the API key to retrieve the breached users
  /// </summary>
  /// <remarks>
  /// To use this without an environment variable, supply a value instead of an empty string.
  /// </remarks>
  public readonly static string OwnedDomain = Environment.GetEnvironmentVariable("IntegrationTests_Domain") ?? "";
}
