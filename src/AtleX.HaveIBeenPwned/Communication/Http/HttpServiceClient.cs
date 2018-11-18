using AtleX.HaveIBeenPwned.Data;
using Pitcher;
using SwissArmyKnife;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using AtleX.HaveIBeenPwned.Communication.Helpers;

namespace AtleX.HaveIBeenPwned.Communication.Http
{
  /// <summary>
  /// Represents an <see cref="IServiceClient"/> that communicates via HTTPS with
  /// the HaveIBeenPwned.com API
  /// </summary>
  public sealed class HttpServiceClient
    : Disposable, IServiceClient
  {
    /// <summary>
    /// Gets the <see cref="ClientSettings"/> to use
    /// </summary>
    private readonly ClientSettings _clientSettings;

    /// <summary>
    /// Gets the <see cref="HttpClient"/> to use
    /// </summary>
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Gets the collection of characters that indicate a newline
    /// </summary>
    private static readonly char[] NewlineChars = new[] { '\r', '\n' };

    /// <summary>
    /// Gets the base uri of the HaveIBeenPwned.com API
    /// </summary>
    private const string ApiBaseUri = "https://haveibeenpwned.com/api/v2";

    /// <summary>
    /// Gets the base uri of the HaveIBeenPwned.com Pwned PAsswords API
    /// </summary>
    private const string PwnedPasswordsBaseUri = "https://api.pwnedpasswords.com/range";

    /// <summary>
    /// Initializes a new instance of <see cref="HttpServiceClient"/> with the
    /// specified <see cref="ClientSettings"/> and <see cref="HttpClient"/>
    /// </summary>
    /// <param name="settings">
    /// The <see cref="ClientSettings"/> to use
    /// </param>
    /// <param name="client">
    /// The <see cref="HttpClient"/> to use when communicating with the
    /// HaveIBeenPwned API
    /// </param>
    public HttpServiceClient(ClientSettings settings, HttpClient client)
    {
      this._clientSettings = settings ?? throw new ArgumentNullException(nameof(settings));
      _ = client ?? throw new ArgumentNullException(nameof(client));

      this._httpClient = ConfigureHttpClient(client, settings);
    }

    /// <summary>
    /// Get the breaches for an account
    /// </summary>
    /// <param name="account">
    /// The account to get the breaches for
    /// </param>
    /// <returns>
    /// An awaitable <see cref="Task{TResult}"/> with the collection of every
    /// <see cref="Breach"/> the account was found in
    /// </returns>
    public async Task<IEnumerable<Breach>> GetBreachesAsync(string account)
    {
      var result = await this.GetBreachesAsync(account, BreachMode.None)
        .ConfigureAwait(false);

      return result;
    }

    /// <summary>
    /// Get the breaches for an account
    /// </summary>
    /// <param name="account">
    /// The account to get the breaches for
    /// </param>
    /// <param name="modes">
    /// The <see cref="BreachMode"/> flags to specify extra breaches to get
    /// </param>
    /// <returns>
    /// An awaitable <see cref="Task{TResult}"/> with the collection of every
    /// <see cref="Breach"/> the account was found in
    /// </returns>
    public async Task<IEnumerable<Breach>> GetBreachesAsync(string account, BreachMode modes)
    {
      Throw.ArgumentNull.When(account.IsNullOrWhiteSpace(), nameof(account));

      var uriBuilder = new UriBuilder($"{ApiBaseUri}/breachedaccount/{account}");

      if (modes == BreachMode.All || (modes & BreachMode.IncludeUnverified) == BreachMode.IncludeUnverified)
      {
        uriBuilder.Query = "includeUnverified=true";
      }

      var content = await this.GetAsync(uriBuilder.Uri)
        .ConfigureAwait(false);

      var results = JsonConvert.DeserializeObject<IEnumerable<Breach>>(content);

      return results;
    }

    /// <summary>
    /// Get the pastes for an email address
    /// </summary>
    /// <param name="emailAddress">
    /// The email address to get the pastes for
    /// </param>
    /// <returns>
    /// An awaitable <see cref="Task{TResult}"/> with the collection of every
    /// <see cref="Paste"/> the email address was found in
    /// </returns>
    public async Task<IEnumerable<Paste>> GetPastesAsync(string emailAddress)
    {
      Throw.ArgumentNull.When(emailAddress.IsNullOrWhiteSpace(), nameof(emailAddress));

      var requestUri = new Uri($"{ApiBaseUri}/pasteaccount/{emailAddress}");

      var content = await this.GetAsync(requestUri)
        .ConfigureAwait(false);

      var results = JsonConvert.DeserializeObject<IEnumerable<Paste>>(content);

      return results ?? Enumerable.Empty<Paste>();
    }

    /// <summary>
    /// Gets whether the specified password is found in a password list
    /// </summary>
    /// <param name="password">
    /// The password to check
    /// </param>
    /// <returns>
    /// An awaitable <see cref="Task{TResult}"/> with a <see cref="bool"/>
    /// indicating whether the password was found or not
    /// </returns>
    public async Task<bool> IsPwnedPasswordAsync(string password)
    {
      Throw.ArgumentNull.When(password.IsNullOrWhiteSpace(), nameof(password));

      var sha1HashOfPassword = KAnonimityHelper.GetHashForPassword(password);

      // We need to send the first 5 characters to the service
      var kAnonimityPart = sha1HashOfPassword.Substring(0, 5);
      // We receive the remainder of the hash (40 minus the 5 characters sent) back
      var kAnonimitySuffix = sha1HashOfPassword.Substring(5);

      var requestUri = new Uri($"{PwnedPasswordsBaseUri}/{kAnonimityPart}");

      var content = await this.GetAsync(requestUri)
        .ConfigureAwait(false);

      var hashes = content.Split(NewlineChars, StringSplitOptions.RemoveEmptyEntries);

      var result = false;

      foreach (var currentKAnonimityHash in hashes)
      {
        var currentHashSuffix = currentKAnonimityHash.Substring(0, 35);
        if (currentHashSuffix == kAnonimitySuffix)
        {
          result = true;
          break;
        }
      }

      return result;
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or
    /// resetting unmanaged resources
    /// </summary>
    /// <param name="disposing">
    /// True when disposing, false when finalizing
    /// </param>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        this._httpClient.Dispose();
      }
    }

    /// <summary>
    /// Performs a GET request to the specified uri
    /// </summary>
    /// <param name="url">
    /// The uri to request
    /// </param>
    /// <returns>
    /// An awaitable <see cref="Task{TResult}"/>
    /// </returns>
    private async Task<string> GetAsync(Uri url)
    {
      this.ThrowIfDisposed();

      var response = await this._httpClient
        .GetAsync(url)
        .ConfigureAwait(false);

      var result = string.Empty;
      try
      {
        if (response.IsSuccessStatusCode)
        {
          result = await response.Content
            .ReadAsStringAsync()
            .ConfigureAwait(false);
        }
        else
        {
          HandleErrorResponse(response);
        }
      }
      catch (Exception e)
      {
        throw new HaveIBeenPwnedClientException("An error occured", e);
      }

      return result;
    }

    /// <summary>
    /// Handle the error response from the specified <see cref="HttpResponseMessage"/>
    /// </summary>
    /// <param name="response">
    /// The <see cref="HttpResponseMessage"/> to handle the errors for
    /// </param>
    private static void HandleErrorResponse(HttpResponseMessage response)
    {
      switch ((int)response.StatusCode)
      {
        case 429: // Rate limit exceeded
          {
            var retryAfter = response.Headers.RetryAfter.Delta?.Seconds;
            throw new RateLimitExceededException(retryAfter.Value);
          }
        case 404: // Not found
          {
            return; // Do nothing
          }
        default:
          {
            throw new HaveIBeenPwnedClientException($"An error occured ({response.StatusCode.ToString()} {response.ReasonPhrase})");
          }
      }
    }

    /// <summary>
    /// Configure the <see cref="HttpClient"/> with the specified <see cref="ClientSettings"/>
    /// </summary>
    /// <param name="client">
    /// The <see cref="HttpClient"/> to setup
    /// </param>
    /// <param name="settings">
    /// The <see cref="ClientSettings"/>
    /// </param>
    /// <returns>
    /// The configured <see cref="HttpClient"/>
    /// </returns>
    private static HttpClient ConfigureHttpClient(HttpClient client, ClientSettings settings)
    {
      client.DefaultRequestHeaders.Clear();
      client.DefaultRequestHeaders.Add("Accept", "application/json");
      client.DefaultRequestHeaders.Add("User-Agent", settings.ApplicationName);
      client.Timeout = settings.TimeOut;

      return client;
    }
  }
}
