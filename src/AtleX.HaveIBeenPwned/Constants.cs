namespace AtleX.HaveIBeenPwned;

internal static class Constants
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

  /// <summary>
  /// Gets the default retry timeout in milliseconds when a HTTP/429 response is received
  /// </summary>
  public const int DefaultRetryValue = 1500;

  /// <summary>
  /// Gets the default value for padding when checking pwned passwords
  /// </summary>
  public const bool PaddingForPwnedPasswordsDefaultValue = true;
}
