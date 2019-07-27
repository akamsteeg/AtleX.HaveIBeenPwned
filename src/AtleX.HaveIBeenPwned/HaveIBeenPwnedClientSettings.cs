using SwissArmyKnife;
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
    /// Gets or sets the application name (defaults to "AtleX.HaveIBeenPwned")
    /// </summary>
    public string ApplicationName
    {
      get;
      set;
    } = "AtleX.HaveIBeenPwned";

    /// <summary>
    /// Gets or sets the <see cref="TimeSpan"/> as timeout when communicating
    /// with the HaveIBeenPwned.com service (defaults to 10 seconds)
    /// </summary>
    public TimeSpan TimeOut
    {
      get;
      set;
    } = 10.Seconds();

    /// <summary>
    /// Gets or sets the API key used to authenticate when checking breaches or pastes
    /// </summary>
    public string ApiKey
    {
      get;
      set;
    }

    /// <summary>
    /// Gets the default <see cref="HaveIBeenPwnedClientSettings"/>
    /// </summary>
    public static HaveIBeenPwnedClientSettings Default => new HaveIBeenPwnedClientSettings();
  }
}
