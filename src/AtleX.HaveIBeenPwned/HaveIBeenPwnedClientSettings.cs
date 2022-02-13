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
}
