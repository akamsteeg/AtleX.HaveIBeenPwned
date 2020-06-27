using System;
using System.Diagnostics.CodeAnalysis;

namespace AtleX.HaveIBeenPwned
{
  /// <summary>
  /// Represents the settings for an <see cref="HaveIBeenPwnedClient"/>
  /// </summary>
  [ExcludeFromCodeCoverage]
  public class HaveIBeenPwnedClientSettings
  {
    /// <summary>
    /// Gets or sets the application name
    /// </summary>
    public string? ApplicationName
    {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets the <see cref="TimeSpan"/> as timeout when communicating
    /// with the HaveIBeenPwned.com service (defaults to 10 seconds)
    /// </summary>
    [Obsolete("The timeout must now be configured on the HTTP client itself")]
    public TimeSpan TimeOut
    {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets the API key used to authenticate when checking breaches or pastes
    /// </summary>
    public string? ApiKey
    {
      get;
      set;
    }

    /// <summary>
    /// Gets the default <see cref="HaveIBeenPwnedClientSettings"/>
    /// </summary>
    [Obsolete("Instantiate HaveIBeenPwnedClientSettings and provide values for the required properties", error: true)]
    public static HaveIBeenPwnedClientSettings Default => throw new NotImplementedException("HaveIBeenPwnedClientSettings.Default is obsolete and must no longer be used");
  }
}
