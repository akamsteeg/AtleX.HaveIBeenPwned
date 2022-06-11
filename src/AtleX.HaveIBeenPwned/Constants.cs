namespace AtleX.HaveIBeenPwned;

/// <summary>
/// Represents constant values within the library
/// </summary>
internal static class Constants
{
  /// <summary>
  /// Represents constants for URIs
  /// </summary>
  internal static class Uris
  {
    /// <summary>
    /// Gets the base uri of the HaveIBeenPwned.com API
    /// </summary>
    private const string ApiBaseUri = "https://haveibeenpwned.com/api/v3";

    /// <summary>
    /// Gets the base uri of the HaveIBeenPwned.com Pwned PAsswords API
    /// </summary>
    public const string PwnedPasswordsBaseUri = "https://api.pwnedpasswords.com/range";

    /// <summary>
    /// Gets the base uri of the breachedaccount endpoint
    /// </summary>
    public const string BreachedAccountBaseUri = ApiBaseUri + "/breachedaccount";

    /// <summary>
    /// Gets the base uri of the pasteaccount endpoint
    /// </summary>
    public const string PasteAccountBaseUri = ApiBaseUri + "/pasteaccount";

    /// <summary>
    /// Gets the bas euri of the breaches endpoint
    /// </summary>
    public const string BreachesUri = ApiBaseUri + "/breaches";
  }

  /// <summary>
  /// Represents constant values for <see cref="HaveIBeenPwnedClientSettings"/>
  /// </summary>
  internal static class Settings
  {
    /// <summary>
    /// Gets the default value for <see
    /// cref="HaveIBeenPwnedClientSettings.RequestPaddingForPwnedPasswordResponses"/>
    /// when checking pwned passwords
    /// </summary>
    public const bool PaddingForPwnedPasswordsDefaultValue = true;
  }

  /// <summary>
  /// Gets the default retry timeout in milliseconds when a HTTP/429 response is received
  /// </summary>
  public const int DefaultRetryValue = 1500;
}
