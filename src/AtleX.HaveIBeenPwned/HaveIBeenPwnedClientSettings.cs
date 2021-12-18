using System;
using System.Diagnostics.CodeAnalysis;

namespace AtleX.HaveIBeenPwned;

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
  ///<para>
  /// Gets or sets whether to pad the responses for pwned password checks with bogus data for additional security
  ///</para>
  /// <para>
  /// The responses are bigger and use additional bandwith
  /// </para>
  /// <para>
  /// For more information see: https://haveibeenpwned.com/API/v3#PwnedPasswordsPadding
  /// </para>
  /// </summary>
  public bool RequestPaddingForPwnedPasswordResponses
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
