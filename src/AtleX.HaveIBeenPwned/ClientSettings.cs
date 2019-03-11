using SwissArmyKnife;
using System;
using System.Diagnostics.CodeAnalysis;

namespace AtleX.HaveIBeenPwned
{
  /// <summary>
  /// Represents the settings for an <see cref="IHaveIBeenPwnedClient"/>
  /// </summary>
  [ExcludeFromCodeCoverage]
  public class ClientSettings
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
    /// Gets the default <see cref="ClientSettings"/>
    /// </summary>
    public static ClientSettings Default => new ClientSettings();
  }
}
