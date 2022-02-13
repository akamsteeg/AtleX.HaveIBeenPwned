using AtleX.HaveIBeenPwned.Helpers;
using Pitcher;
using SwissArmyKnife;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AtleX.HaveIBeenPwned;

/// <summary>
/// <para>Represents an <see cref="IHaveIBeenPwnedClient"/> that communicates via HTTPS with
/// the HaveIBeenPwned.com API.</para>
///
/// <para>This class is thread-safe.</para>
/// </summary>
public sealed class HaveIBeenPwnedClient
  : Disposable, IHaveIBeenPwnedClient
{
  /// <summary>
  /// Gets the <see cref="HaveIBeenPwnedClientSettings"/> for this <see cref="HaveIBeenPwnedClient"/>
  /// </summary>
  private readonly HaveIBeenPwnedClientSettings _clientSettings;

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
  /// Initializes a new instance of <see cref="HaveIBeenPwnedClient"/> with the
  /// specified <see cref="HaveIBeenPwnedClientSettings"/> and <see cref="HttpClient"/>
  /// </summary>
  /// <param name="settings">
  /// The <see cref="HaveIBeenPwnedClientSettings"/> to use
  /// </param>
  public HaveIBeenPwnedClient(HaveIBeenPwnedClientSettings settings)
    : this(settings, new HttpClient(), mustDisposeClient: true)
  {
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
    : this(settings, client, mustDisposeClient: false)
  {
  }

  /// <summary>
  /// Initializes a new instance of <see cref="HaveIBeenPwnedClient"/> with
  /// the specified <see cref="HaveIBeenPwnedClientSettings"/> and <see cref="HttpClient"/>
  /// </summary>
  /// <param name="settings">
  /// The <see cref="HaveIBeenPwnedClientSettings"/> to use
  /// </param>
  /// <param name="client">
  /// The <see cref="HttpClient"/> to use when communicating with the
  /// HaveIBeenPwned API
  /// </param>
  /// <param name="mustDisposeClient">
  /// True when the <see cref="HttpClient"/> was created by this <see
  /// cref="HaveIBeenPwnedClient"/> and must be disposed; false otherwise
  /// </param>
  private HaveIBeenPwnedClient(HaveIBeenPwnedClientSettings settings, HttpClient client, bool mustDisposeClient)
  {
    Throw.ArgumentNull.WhenNull(settings, nameof(settings));
    Throw.ArgumentNull.WhenNullOrWhiteSpace(settings.ApplicationName,
      nameof(settings.ApplicationName),
      $"{nameof(HaveIBeenPwnedClientSettings)}.{nameof(settings.ApplicationName)} cannot be null or empty");
    Throw.ArgumentNull.WhenNull(client, nameof(client));

    this._clientSettings = settings;
    this._httpClient = ConfigureHttpClient(client, settings);
    this._enableClientDisposing = mustDisposeClient;
  }

  /// <inheritdoc />
  public async Task<IEnumerable<SiteBreach>> GetAllBreachesAsync() =>
    await this.GetAllBreachesInternalAsync(CancellationToken.None)
      .ConfigureAwait(false);

  /// <inheritdoc />
  public async Task<IEnumerable<SiteBreach>> GetAllBreachesAsync(CancellationToken cancellationToken) =>
    await this.GetAllBreachesInternalAsync(cancellationToken)
      .ConfigureAwait(false);

  /// <inheritdoc />
  public async Task<IEnumerable<Breach>> GetBreachesAsync(string account) =>
    await this.GetBreachesInternalAsync(account, BreachMode.Default, CancellationToken.None)
      .ConfigureAwait(false);

  /// <inheritdoc />
  public async Task<IEnumerable<Breach>> GetBreachesAsync(string account, CancellationToken cancellationToken) =>
    await this.GetBreachesInternalAsync(account, BreachMode.Default, cancellationToken)
      .ConfigureAwait(false);

  /// <inheritdoc />
  public async Task<IEnumerable<Breach>> GetBreachesAsync(string account, BreachMode modes) =>
    await this.GetBreachesInternalAsync(account, modes, CancellationToken.None)
      .ConfigureAwait(false);

  /// <inheritdoc />
  public async Task<IEnumerable<Breach>> GetBreachesAsync(string account, BreachMode modes, CancellationToken cancellationToken) =>
    await this.GetBreachesInternalAsync(account, modes, cancellationToken)
      .ConfigureAwait(false);

  /// <inheritdoc />
  public async Task<IEnumerable<Paste>> GetPastesAsync(string emailAddress) =>
    await this.GetPastesInternalAsync(emailAddress, CancellationToken.None)
      .ConfigureAwait(false);

  /// <inheritdoc />
  public async Task<IEnumerable<Paste>> GetPastesAsync(string emailAddress, CancellationToken cancellationToken) =>
    await this.GetPastesInternalAsync(emailAddress, cancellationToken)
      .ConfigureAwait(false);

  /// <inheritdoc />
  public async Task<bool> IsPwnedPasswordAsync(string password) =>
    await this.IsPwnedPasswordInternalAsync(password, CancellationToken.None)
      .ConfigureAwait(false);

  /// <inheritdoc />
  public async Task<bool> IsPwnedPasswordAsync(string password, CancellationToken cancellationToken) =>
    await this.IsPwnedPasswordInternalAsync(password, cancellationToken)
      .ConfigureAwait(false);

  /// <inheritdoc />
  protected override void Dispose(bool disposing)
  {
    if (disposing && this._enableClientDisposing)
    {
      this._httpClient.Dispose();
    }
  }

  /// <summary>
  /// Get all site breaches in the system
  /// </summary>
  /// <param name="cancellationToken">
  /// The <see cref="CancellationToken"/> for this operation
  /// </param>
  /// <returns>
  /// An awaitable <see cref="Task{TResult}"/> with the collection of every
  /// <see cref="SiteBreach"/> in the system
  /// </returns>
  private async Task<IEnumerable<SiteBreach>> GetAllBreachesInternalAsync(CancellationToken cancellationToken)
  {
    this.ThrowIfDisposed();

    var uri = UriFactory.GetAllBreachesUri();

    using var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

    var result = await this.GetAsync<IEnumerable<SiteBreach>>(requestMessage, cancellationToken)
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
  private async Task<IEnumerable<Breach>> GetBreachesInternalAsync(string account, BreachMode modes, CancellationToken cancellationToken)
  {
    Throw.ArgumentNull.WhenNullOrWhiteSpace(account, nameof(account));
    this.ThrowIfDisposed();

    var uri = UriFactory.GetBreachesForAccountUri(account, modes);

    var results = await this.GetAuthenticatedAsync<IEnumerable<Breach>>(uri, cancellationToken)
      .ConfigureAwait(false);

    return results;
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
    Throw.ArgumentNull.WhenNullOrWhiteSpace(emailAddress, nameof(emailAddress));
    this.ThrowIfDisposed();

    var requestUri = UriFactory.GetPasteAccountUri(emailAddress);

    var results = await this.GetAuthenticatedAsync<IEnumerable<Paste>>(requestUri, cancellationToken)
      .ConfigureAwait(false);

    return results;
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
    Throw.ArgumentNull.WhenNullOrWhiteSpace(password, nameof(password));
    this.ThrowIfDisposed();

    var result = false;

    var (kAnonimityPart, kAnonimityRemainder) = KAnonimityHelper.GetKAnonimityPartsForPassword(password);

    var requestUri = UriFactory.GetPwnedPasswordUri(kAnonimityPart);

    using var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);

    if (this._clientSettings.RequestPaddingForPwnedPasswordResponses)
    {
      requestMessage.Headers.Add("Add-Padding", "true");
    }

    using var response = await this.ExecuteRequestAsync(requestMessage, cancellationToken).ConfigureAwait(false);

    if (response.StatusCode == HttpStatusCode.OK)
    {
      var content = await response.Content
        .ReadAsStringAsync()
        .ConfigureAwait(false);

      result = content.Contains(kAnonimityRemainder);
    }

    return result;
  }

  /// <summary>
  /// Performs an GET request to the specified uri
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
  private async Task<T> GetAuthenticatedAsync<T>(Uri url, CancellationToken cancellationToken)
    where T : notnull
  {
    Throw<InvalidApiKeyException>.When(this._clientSettings.ApiKey.IsNullOrWhiteSpace());

    this.ThrowIfDisposed();

    using var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);

    requestMessage.Headers.Add("hibp-api-key", this._clientSettings.ApiKey);

    var result = await this.GetAsync<T>(requestMessage, cancellationToken)
      .ConfigureAwait(false);

    return result;
  }
  /// <summary>
  /// Performs a GET request to the specified uri
  /// </summary>
  /// <typeparam name="T">
  /// The type to deserialize the JSON content of the request to
  /// </typeparam>
  /// <param name="requestMessage">
  /// The <see cref="HttpRequestMessage"/> to send
  /// </param>
  /// <param name="cancellationToken">
  /// The <see cref="CancellationToken"/> for this operation
  /// </param>
  /// <returns>
  /// An awaitable <see cref="Task{TResult}"/> of the specified type
  /// </returns>
  private async Task<T> GetAsync<T>(HttpRequestMessage requestMessage, CancellationToken cancellationToken)
    where T : notnull
  {
    this.ThrowIfDisposed();

    using var response = await this.ExecuteRequestAsync(requestMessage, cancellationToken).ConfigureAwait(false);
    using var content = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

    var result = await JsonSerializer
      .DeserializeAsync<T>(content, cancellationToken: cancellationToken)
      .ConfigureAwait(false);

    if (result is null)
    {
      throw new InvalidOperationException($"Response could not be parsed to {nameof(T)}");
    }

    return result;
  }

  /// <summary>
  /// Performs a GET request to the specified uri
  /// </summary>
  /// <param name="requestMessage">
  /// The <see cref="HttpRequestMessage"/> to send
  /// </param>
  /// <param name="cancellationToken">
  /// The <see cref="CancellationToken"/> for this operation
  /// </param>
  /// <returns>
  /// An awaitable <see cref="Task{TResult}"/> of <see cref="HttpResponseMessage"/>
  /// </returns>
  private async Task<HttpResponseMessage> ExecuteRequestAsync(HttpRequestMessage requestMessage, CancellationToken cancellationToken)
  {
    if (requestMessage.Method != HttpMethod.Get) { throw new InvalidOperationException($"Request method '{requestMessage.Method}' not supported"); }
    this.ThrowIfDisposed();

    var result = await this._httpClient
       .SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
       .ConfigureAwait(false);

    if (!result.IsSuccessStatusCode)
    {
      HandleErrorResponse(result);
    }

    return result;
  }

  /// <summary>
  /// Handle the error response from the specified <see cref="HttpResponseMessage"/>/>
  /// </summary>
  /// <param name="response">
  /// The <see cref="HttpResponseMessage"/> to handle the errors for
  /// </param>
  private static void HandleErrorResponse(HttpResponseMessage response)
  {
    var statusCode = (int)response.StatusCode;

    switch (statusCode)
    {
      case 429: // Rate limit exceeded
        {
          // If we don't get a retry-after value from the HaveIBeenPwnedService, we revert to their specified default of 1500 ms.
          var retryAfter = response.Headers.RetryAfter.Delta ?? 1500.MilliSeconds();
          throw new RateLimitExceededException(retryAfter);
        }
      case 404: // Not found
        {
          return; // Do nothing
        }
      case 401:
        {
          throw new InvalidApiKeyException();
        }
      default:
        {
          throw new HaveIBeenPwnedClientException($"An error occured ({statusCode} {response.ReasonPhrase})");
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
    var acceptJsonHeader = new MediaTypeWithQualityHeaderValue("application/json");
    client.DefaultRequestHeaders.Accept.Add(acceptJsonHeader);
    client.DefaultRequestHeaders.Add("User-Agent", settings.ApplicationName);

    return client;
  }
}
