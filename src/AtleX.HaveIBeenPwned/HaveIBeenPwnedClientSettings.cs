// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

namespace AtleX.HaveIBeenPwned;

/// <summary>
/// Represents the settings for an <see cref="HaveIBeenPwnedClient"/>
/// </summary>
public class HaveIBeenPwnedClientSettings
{
  /// <summary>
  /// <para>
  /// Gets or sets the application name. This cannot be null or an empty string.
  /// </para>
  /// <para>
  /// For more information see: <see href="https://haveibeenpwned.com/API/v3#UserAgent"/>
  /// </para>
  /// </summary>
  public string ApplicationName
  {
    get;
    set;
  } = string.Empty;

  /// <summary>
  /// <para>
  /// Gets or sets the API key used to authenticate when checking breaches or
  /// pastes. This can be left null or empty if you only want to check passwords
  /// or get all the breaches from the system
  /// </para>
  /// <para>
  /// For more information see: <see href="https://haveibeenpwned.com/API/Key"/>
  /// </para>
  /// </summary>
  public string? ApiKey
  {
    get;
    set;
  }

  /// <summary>
  ///<para>
  /// Gets or sets whether to pad the responses for pwned password checks with bogus data
  /// for additional security. Defaults to true. (Recommended)
  ///</para>
  /// <para>
  /// When set to true, the responses are bigger and use additional bandwith
  /// </para>
  /// <para>
  /// For more information see: <see href="https://haveibeenpwned.com/API/v3#PwnedPasswordsPadding"/>
  /// </para>
  /// </summary>
  public bool RequestPaddingForPwnedPasswordResponses
  {
    get;
    set;
  } = Constants.Settings.PaddingForPwnedPasswordsDefaultValue;
}
