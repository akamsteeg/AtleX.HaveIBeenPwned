namespace AtleX.HaveIBeenPwned;
internal static class Constants
{
  /// <summary>
  /// Gets the base uri of the HaveIBeenPwned.com API
  /// </summary>
  public const string ApiBaseUri = "https://haveibeenpwned.com/api/v3";

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
