// Copyright (c) Alex Kamsteeg (https://atlex.nl/)
// License: MIT (See LICENSE file)

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
#pragma warning disable S1075 // Refactor your code not to use hardcoded absolute paths or URIs
    // The HaveIBeenPwned.com API uses these URIs. It's the way it is. For private
    // implementations, maybe with a proxy, users of the lib are free to implement
    // one of the interfaces themselves

    /// <summary>
    /// Gets the base uri of the HaveIBeenPwned.com API
    /// </summary>
    private const string ApiBaseUri = "https://haveibeenpwned.com/api/v3";

    /// <summary>
    /// Gets the base uri of the HaveIBeenPwned.com Pwned PAsswords API
    /// </summary>
    public const string PwnedPasswordsBaseUri = "https://api.pwnedpasswords.com/range";
#pragma warning restore S1075

    /// <summary>
    /// Gets the base uri of the breachedaccount endpoint
    /// </summary>
    public const string BreachedAccountBaseUri = ApiBaseUri + "/breachedaccount";

    /// <summary>
    /// Gets the base uri of the breacheddomain endpoint
    /// </summary>
    public const string BreachedDomainBaseUri = ApiBaseUri + "/breacheddomain";

    /// <summary>
    /// Gets the uri of the subscribed domains
    /// </summary>
    public const string SubscribedDomainsUri = ApiBaseUri + "/subscribeddomains";

    /// <summary>
    /// Gets the base uri of the pasteaccount endpoint
    /// </summary>
    public const string PasteAccountBaseUri = ApiBaseUri + "/pasteaccount";

    /// <summary>
    /// Gets the base uri of the breaches endpoint
    /// </summary>
    public const string BreachesUri = ApiBaseUri + "/breaches";

    /// <summary>
    /// Gets the uri for the latest breach
    /// </summary>
    public const string LatestBreachUri = ApiBaseUri + "/latestbreach";
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
  /// Represents constant values for communication with the HaveIBeenPwned.com API
  /// </summary>
  internal static class Communication
  {
    /// <summary>
    /// Represents constant values for communication via HTTP with the
    /// HaveIBeenPwned.com API
    /// </summary>
    internal static class Http
    {
      /// <summary>
      /// Gets the media type
      /// </summary>
      public const string MediaType = "application/json";

      /// <summary>
      /// Gets the default retry timeout in milliseconds when a HTTP/429
      /// response is received
      /// </summary>
      public const int DefaultRetryValue = 1500;
    }
  }

  /// <summary>
  /// Represents constant values for <see href="https://en.wikipedia.org/wiki/K-anonymity">KAnonimity</see>
  /// </summary>
  /// <remarks>
  /// See <see
  /// href="https://blog.cloudflare.com/validating-leaked-passwords-with-k-anonymity">the
  /// CloudFlare blog</see> for more ifnromation about the usage in the
  /// HaveIBeenPwned API
  /// </remarks>
  internal static class KAnonimity
  {
    /// <summary>
    /// Gets the length of the KAnonimity part to send to the HaveIBeenPwned API
    /// </summary>
    public const int PartLength = 5;

    /// <summary>
    /// Gets the length of the remainder of the hash that serves as the suffix
    /// of the KAnonimity system
    /// </summary>
    public const int RemainderLength = 35;

    /// <summary>
    /// Gets the total length of a hash for KAnonimity usage.
    /// </summary>
    /// <remarks>
    /// Curently, this library only supports checking for SHA 1 hashes and not NTLM
    /// </remarks>
    public const int TotalLength = PartLength + RemainderLength;  // SHA1 hash is 40 characters long
  }

  /// <summary>
  /// Represents constant values for <see cref="Helpers.HashCodeHelper"/>
  /// </summary>
  internal static class HashCode
  {
    /// <summary>
    /// Gets the prime number to start the calculation of the hash code with
    /// </summary>
    public const int InitialPrimeNumber = 17;

    /// <summary>
    /// Gets the prime number to multiply a previously calculated value with
    /// </summary>
    public const int MultiplierPrimeNumber = 23;
  }
}