using AtleX.HaveIBeenPwned.Helpers;
using Newtonsoft.Json;
using Pitcher;
using SwissArmyKnife;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AtleX.HaveIBeenPwned
{
  /// <summary>
  /// Represents an <see cref="IHaveIBeenPwnedClient"/> that communicates via HTTPS with
  /// the HaveIBeenPwned.com API
  /// </summary>
  public sealed class HaveIBeenPwnedClient
    : Disposable, IHaveIBeenPwnedClient
  {
    /// <summary>
    /// Gets the <see cref="HttpClient"/> to use
    /// </summary>
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Gets whether the <see cref="HttpClient"/> must be disposed or not
    /// </summary>
    /// <remarks>
    /// When we create the <see cref="HttpClient"/> ourselves, it must be
    /// disposed. When it was injected via the constructor, we must not
    /// dispose it. After all, the <see cref="HttpClient"/> can be used
    /// by the callee too and it would be rude and error-prone to dispose
    /// it in that case.
    /// </remarks>
    private readonly bool _enableClientDisposing;

    /// <summary>
    /// Gets the base uri of the HaveIBeenPwned.com API
    /// </summary>
    private const string ApiBaseUri = "https://haveibeenpwned.com/api/v2";

    /// <summary>
    /// Gets the base uri of the HaveIBeenPwned.com Pwned PAsswords API
    /// </summary>
    private const string PwnedPasswordsBaseUri = "https://api.pwnedpasswords.com/range";

    /// <summary>
    /// Initializes a new instance of <see cref="HaveIBeenPwnedClient"/> with the
    /// specified <see cref="HaveIBeenPwnedClientSettings"/> and <see cref="HttpClient"/>
    /// </summary>
    /// <param name="settings">
    /// The <see cref="HaveIBeenPwnedClientSettings"/> to use
    /// </param>
    public HaveIBeenPwnedClient(HaveIBeenPwnedClientSettings settings)
      : this(settings, new HttpClient())
    {
      this._enableClientDisposing = true;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="HaveIBeenPwnedClient"/> with the
    /// specified <see cref="HaveIBeenPwnedClientSettings"/> and <see cref="HttpClient"/>
    /// </summary>
    /// <param name="settings">
    /// The <see cref="HaveIBeenPwnedClientSettings"/> to use
    /// </param>
    /// <param name="client">
    /// The <see cref="HttpClient"/> to use when communicating with the
    /// HaveIBeenPwned API
    /// </param>
    public HaveIBeenPwnedClient(HaveIBeenPwnedClientSettings settings, HttpClient client)
    {
      Throw.ArgumentNull.WhenNull(settings, nameof(settings));
      Throw.ArgumentNull.WhenNull(client, nameof(client));

      this._httpClient = ConfigureHttpClient(client, settings);
      this._enableClientDisposing = false;
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
      Throw.ArgumentNull.When(account.IsNullOrWhiteSpace(), nameof(account));
      this.ThrowIfDisposed();

      var result = await this.GetBreachesInternalAsync(account, BreachMode.Default, CancellationToken.None)
        .ConfigureAwait(false);

      return result;
    }

    /// <summary>
    /// Get the breaches for an account
    /// </summary>
    /// <param name="account">
    /// The account to get the breaches for
    /// </param>
    /// <param name="cancellationToken">
    /// The <see cref="CancellationToken"/> for this operation
    /// </param>
    /// <returns>
    /// An awaitable <see cref="Task{TResult}"/> with the collection of every
    /// <see cref="Breach"/> the account was found in
    /// </returns>
    public async Task<IEnumerable<Breach>> GetBreachesAsync(string account, CancellationToken cancellationToken)
    {
      Throw.ArgumentNull.When(account.IsNullOrWhiteSpace(), nameof(account));
      this.ThrowIfDisposed();

      var result = await this.GetBreachesInternalAsync(account, BreachMode.Default, cancellationToken)
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
      this.ThrowIfDisposed();

      var result = await this.GetBreachesInternalAsync(account, modes, CancellationToken.None)
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
    /// <param name="cancellationToken">
    /// The <see cref="CancellationToken"/> for this operation
    /// </param>
    /// <returns>
    /// An awaitable <see cref="Task{TResult}"/> with the collection of every
    /// <see cref="Breach"/> the account was found in
    /// </returns>
    public async Task<IEnumerable<Breach>> GetBreachesAsync(string account, BreachMode modes, CancellationToken cancellationToken)
    {
      Throw.ArgumentNull.When(account.IsNullOrWhiteSpace(), nameof(account));
      this.ThrowIfDisposed();

      var result = await this.GetBreachesInternalAsync(account, modes, cancellationToken)
        .ConfigureAwait(false);

      return result;
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
      this.ThrowIfDisposed();

      var result = await this.GetPastesInternalAsync(emailAddress, CancellationToken.None)
        .ConfigureAwait(false);

      return result;
    }

    /// <summary>
    /// Get the pastes for an email address
    /// </summary>
    /// <param name="emailAddress">
    /// The email address to get the pastes for
    /// </param>
    /// <param name="cancellationToken">
    /// The <see cref="CancellationToken"/> for this operation
    /// </param>
    /// <returns>
    /// An awaitable <see cref="Task{TResult}"/> with the collection of every
    /// <see cref="Paste"/> the email address was found in
    /// </returns>
    public async Task<IEnumerable<Paste>> GetPastesAsync(string emailAddress, CancellationToken cancellationToken)
    {
      Throw.ArgumentNull.When(emailAddress.IsNullOrWhiteSpace(), nameof(emailAddress));
      this.ThrowIfDisposed();

      var result = await this.GetPastesInternalAsync(emailAddress, cancellationToken)
        .ConfigureAwait(false);

      return result;
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
      this.ThrowIfDisposed();

      var result = await this.IsPwnedPasswordInternalAsync(password, CancellationToken.None)
        .ConfigureAwait(false);

      return result;
    }

    /// <summary>
    /// Gets whether the specified password is found in a password list
    /// </summary>
    /// <param name="password">
    /// The password to check
    /// </param>
    /// <param name="cancellationToken">
    /// The <see cref="CancellationToken"/> for this operation
    /// </param>
    /// <returns>
    /// An awaitable <see cref="Task{TResult}"/> with a <see cref="bool"/>
    /// indicating whether the password was found or not
    /// </returns>
    public async Task<bool> IsPwnedPasswordAsync(string password, CancellationToken cancellationToken)
    {
      Throw.ArgumentNull.When(password.IsNullOrWhiteSpace(), nameof(password));
      this.ThrowIfDisposed();

      var result = await this.IsPwnedPasswordInternalAsync(password, cancellationToken)
        .ConfigureAwait(false);

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
      if (disposing && this._enableClientDisposing)
      {
        this._httpClient.Dispose();
      }
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
    /// <param name="cancellationToken">
    /// The <see cref="CancellationToken"/> for this operation
    /// </param>
    /// <returns>
    /// An awaitable <see cref="Task{TResult}"/> with the collection of every
    /// <see cref="Breach"/> the account was found in
    /// </returns>
    private async Task<IEnumerable<Breach>> GetBreachesInternalAsync(string account, BreachMode modes, CancellationToken cancellationToken)
    {
      var uriBuilder = new UriBuilder($"{ApiBaseUri}/breachedaccount/{account}");

      if (modes == BreachMode.All || (modes & BreachMode.IncludeUnverified) == BreachMode.IncludeUnverified)
      {
        uriBuilder.Query = "includeUnverified=true";
      }

      var results = await this.GetAsync<IEnumerable<Breach>>(uriBuilder.Uri, cancellationToken)
        .ConfigureAwait(false);

      return results ?? Enumerable.Empty<Breach>();
    }

    /// <summary>
    /// Get the pastes for an email address
    /// </summary>
    /// <param name="emailAddress">
    /// The email address to get the pastes for
    /// </param>
    /// <param name="cancellationToken">
    /// The <see cref="CancellationToken"/> for this operation
    /// </param>
    /// <returns>
    /// An awaitable <see cref="Task{TResult}"/> with the collection of every
    /// <see cref="Paste"/> the email address was found in
    /// </returns>
    private async Task<IEnumerable<Paste>> GetPastesInternalAsync(string emailAddress, CancellationToken cancellationToken)
    {
      Throw.ArgumentNull.When(emailAddress.IsNullOrWhiteSpace(), nameof(emailAddress));
      this.ThrowIfDisposed();

      var requestUri = new Uri($"{ApiBaseUri}/pasteaccount/{emailAddress}");

      var results = await this.GetAsync<IEnumerable<Paste>>(requestUri, cancellationToken)
        .ConfigureAwait(false);

      return results ?? Enumerable.Empty<Paste>();
    }

    /// <summary>
    /// Gets whether the specified password is found in a password list
    /// </summary>
    /// <param name="password">
    /// The password to check
    /// </param>
    /// <param name="cancellationToken">
    /// The <see cref="CancellationToken"/> for this operation
    /// </param>
    /// <returns>
    /// An awaitable <see cref="Task{TResult}"/> with a <see cref="bool"/>
    /// indicating whether the password was found or not
    /// </returns>
    private async Task<bool> IsPwnedPasswordInternalAsync(string password, CancellationToken cancellationToken)
    {
      Throw.ArgumentNull.When(password.IsNullOrWhiteSpace(), nameof(password));
      this.ThrowIfDisposed();

      var (kAnonimityPart, kAnonimitySuffix) = KAnonimityHelper.GetKAnonimityPartsForPassword(password);

      var requestUri = new Uri($"{PwnedPasswordsBaseUri}/{kAnonimityPart}");

      using (var data = await this.GetAsync(requestUri, cancellationToken).ConfigureAwait(false))
      using (var streamReader = new StreamReader(data))
      {
        var result = false;

        while (!streamReader.EndOfStream && !result)
        {
          cancellationToken.ThrowIfCancellationRequested();

          var currentLine = await streamReader
            .ReadLineAsync()
            .ConfigureAwait(false);

          if (currentLine.StartsWith(kAnonimitySuffix))
          {
            result = true;
            break;
          }
        }

        return result;
      }
    }

    /// <summary>
    /// Performs a GET request to the specified uri
    /// </summary>
    /// <typeparam name="T">
    /// The type to deserialize the JSON content of the request to
    /// </typeparam>
    /// <param name="url">
    /// The uri to request
    /// </param>
    /// <param name="cancellationToken">
    /// The <see cref="CancellationToken"/> for this operation
    /// </param>
    /// <returns>
    /// An awaitable <see cref="Task{TResult}"/> of the specified type
    /// </returns>
    private async Task<T> GetAsync<T>(Uri url, CancellationToken cancellationToken)
    {
      Throw.ArgumentNull.WhenNull(url, nameof(url));
      this.ThrowIfDisposed();

      using (var data = await this.GetAsync(url, cancellationToken).ConfigureAwait(false))
      using (var streamReader = new StreamReader(data))
      using (var jsonReader = new JsonTextReader(streamReader))
      {
        var jsonSerializer = new JsonSerializer();
        var result = jsonSerializer.Deserialize<T>(jsonReader);

        return result;
      }
    }

    /// <summary>
    /// Performs a GET request to the specified uri
    /// </summary>
    /// <param name="url">
    /// The uri to request
    /// </param>
    /// <param name="cancellationToken">
    /// The <see cref="CancellationToken"/> for this operation
    /// </param>
    /// <returns>
    /// An awaitable <see cref="Task{TResult}"/> of <see cref="Stream"/>
    /// </returns>
    private async Task<Stream> GetAsync(Uri url, CancellationToken cancellationToken)
    {
      Throw.ArgumentNull.WhenNull(url, nameof(url));
      this.ThrowIfDisposed();

      var result = Stream.Null;

      try
      {
        using (var response = await this._httpClient
        .GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
        .ConfigureAwait(false))
        {
          if (response.IsSuccessStatusCode)
          {
            result = new MemoryStream();

            await response
              .Content
              .CopyToAsync(result)
              .ConfigureAwait(false);

            result.Reset();
          }
          else
          {
            HandleErrorResponse(response);
          }
        }
      }
      catch (Exception e) when (!(e is HaveIBeenPwnedClientException))
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
      Throw.ArgumentNull.WhenNull(response, nameof(response));

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
    /// Configure the <see cref="HttpClient"/> with the specified <see cref="HaveIBeenPwnedClientSettings"/>
    /// </summary>
    /// <param name="client">
    /// The <see cref="HttpClient"/> to setup
    /// </param>
    /// <param name="settings">
    /// The <see cref="HaveIBeenPwnedClientSettings"/>
    /// </param>
    /// <returns>
    /// The configured <see cref="HttpClient"/>
    /// </returns>
    private static HttpClient ConfigureHttpClient(HttpClient client, HaveIBeenPwnedClientSettings settings)
    {
      Throw.ArgumentNull.WhenNull(client, nameof(client));
      Throw.ArgumentNull.WhenNull(settings, nameof(settings));

      client.DefaultRequestHeaders.Clear();
      client.DefaultRequestHeaders.Add("Accept", "application/json");
      client.DefaultRequestHeaders.Add("User-Agent", settings.ApplicationName);
      client.Timeout = settings.TimeOut;

      return client;
    }
  }
}
