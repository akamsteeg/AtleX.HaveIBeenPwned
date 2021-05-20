using System;

namespace AtleX.HaveIBeenPwned
{
  internal static class UriFactory
  {
    /// <summary>
    /// Gets the base uri of the HaveIBeenPwned.com API
    /// </summary>
    private const string ApiBaseUri = "https://haveibeenpwned.com/api/v3";

    /// <summary>
    /// Gets the base uri of the HaveIBeenPwned.com Pwned PAsswords API
    /// </summary>
    private const string PwnedPasswordsBaseUri = "https://api.pwnedpasswords.com/range";

    /// <summary>
    /// Gets the base uri of the breachedaccount endpoint
    /// </summary>
    private const string BreachedAccountBaseUri = ApiBaseUri + "/breachedaccount";

    /// <summary>
    /// Gets the base uri of the pasteaccount endpoint
    /// </summary>
    private const string PasteAccountBaseUri = ApiBaseUri + "/pasteaccount";

    /// <summary>
    /// Gets the bas euri of the breaches endpoint
    /// </summary>
    private static readonly Uri BreachesUri = new(ApiBaseUri + "/breaches");

    /// <summary>
    /// Gets the <see cref="Uri"/> to get all breaches available in the system
    /// </summary>
    /// <returns>
    /// The <see cref="Uri"/> to get all breaches
    /// </returns>
    public static Uri GetAllBreachesUri() => BreachesUri;

    /// <summary>
    /// Gets the <see cref="Uri"/> to get the breaches the specified account was
    /// part of
    /// </summary>
    /// <param name="account">
    /// The account name to get the breaches for
    /// </param>
    /// <param name="modes">
    /// The combination of <see cref="BreachMode"/> options to get
    /// </param>
    /// <returns>
    /// The <see cref="Uri"/> to get the breaches
    /// </returns>
    public static Uri GetBreachesForAccountUri(string account, BreachMode modes)
    {
      Uri? result;

      var baseUri = $"{BreachedAccountBaseUri}/{account}";

      if (modes.HasFlag(BreachMode.ExcludeUnverified))
      {
        var uriBuilder = new UriBuilder(baseUri)
        {
          Query = "includeUnverified=false"
        };

        result = uriBuilder.Uri;
      }
      else
      {
        result = new Uri(baseUri);
      }

      return result;
    }

    /// <summary>
    /// Gets the <see cref="Uri"/> to get the pastes the specified account was
    /// part of
    /// </summary>
    /// <param name="emailAddress">
    /// The email address to search the pastes for
    /// </param>
    /// <returns>
    /// The <see cref="Uri"/> to get the pastes
    /// </returns>
    public static Uri GetPasteAccountUri(string emailAddress)
    {
      var result = new Uri($"{PasteAccountBaseUri}/{emailAddress}");

      return result;
    }

    /// <summary>
    /// Gets the <see cref="Uri"/> to get the KAnonimy suffixes for the
    /// specified KAnonimity prefix
    /// </summary>
    /// <param name="kAnonimitySuffixPart">
    /// The prefix of the KAnonimity hashes
    /// </param>
    /// <returns>
    /// The <see cref="Uri"/> to get the KAnonimy suffixes 
    /// </returns>
    public static Uri GetPwnedPasswordUri(string kAnonimitySuffixPart)
    {
      var result = new Uri($"{PwnedPasswordsBaseUri}/{kAnonimitySuffixPart}");

      return result;
    }
  }
}
