using AtleX.HaveIBeenPwned.Communication.Http;
using AtleX.HaveIBeenPwned.Helpers;
using AtleX.HaveIBeenPwned.Serialization.Json;
using Pitcher;
using SwissArmyKnife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AtleX.HaveIBeenPwned;

/// <summary>
/// <para>
/// Represents an <see cref="IHaveIBeenPwnedClient"/> that communicates via
/// HTTPS with the <see href="https://haveibeenpwned.com/">HaveIBeenPwned</see>
/// service API.
/// </para>
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
  /// Gets the <see cref="JsonSerializerOptions"/> to use when (de)serializing JSON
  /// </summary>
  private static readonly JsonSerializerOptions JsonOptions = JsonSerializerOptionsFactory.Create();

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
    Throw.ArgumentNull.WhenNull(client, nameof(client));

    this._clientSettings = settings;
    this._httpClient = client.ConfigureForHaveIBeenPwnedApi(settings);
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
  public async Task<SiteBreach?> GetLatestBreachAsync() =>
    await this.GetLatestBreachInternalAsync(CancellationToken.None)
    .ConfigureAwait(false);

  /// <inheritdoc />
  public async Task<SiteBreach?> GetLatestBreachAsync(CancellationToken cancellationToken) =>
    await this.GetLatestBreachInternalAsync(cancellationToken)
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
  public async Task<IEnumerable<DomainUser>> GetBreachedDomainUsersAsync(string domain) =>
    await this.GetBreachedDomainUsersInternalAsync(domain, CancellationToken.None)
    .ConfigureAwait(false);

  /// <inheritdoc />
  public async Task<IEnumerable<DomainUser>> GetBreachedDomainUsersAsync(string domain, CancellationToken cancellationToken) =>
    await this.GetBreachedDomainUsersInternalAsync(domain, cancellationToken)
    .ConfigureAwait(false);

  /// <inheritdoc />
  protected sealed override void Dispose(bool disposing)
  {
    if (disposing && this._enableClientDisposing)
    {
      this._httpClient.Dispose();
    }
  }

  /// <inheritdoc cref="IHaveIBeenPwnedBreachesClient.GetAllBreachesAsync(CancellationToken)"/>
  private async Task<IEnumerable<SiteBreach>> GetAllBreachesInternalAsync(CancellationToken cancellationToken)
  {
    this.ThrowIfDisposed();
    cancellationToken.ThrowIfCancellationRequested();

    var uri = UriFactory.GetAllBreachesUri();

    using var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

    var result = await this.GetAsync<IEnumerable<SiteBreach>>(requestMessage, cancellationToken)
      .ConfigureAwait(false);

    return result ?? Enumerable.Empty<SiteBreach>();
  }

  /// <inheritdoc cref="IHaveIBeenPwnedBreachesClient.GetBreachesAsync(string, BreachMode, CancellationToken)"/>
  private async Task<IEnumerable<Breach>> GetBreachesInternalAsync(string account, BreachMode modes, CancellationToken cancellationToken)
  {
    Throw.ArgumentNull.WhenNullOrWhiteSpace(account, nameof(account));
    this.ThrowIfDisposed();
    cancellationToken.ThrowIfCancellationRequested();

    var uri = UriFactory.GetBreachesForAccountUri(account, modes);

    var results = await this.GetAuthenticatedAsync<IEnumerable<Breach>>(uri, cancellationToken)
      .ConfigureAwait(false);

    return results ?? Enumerable.Empty<Breach>();
  }

  /// <inheritdoc cref="IHaveIBeenPwnedBreachesClient.GetLatestBreachAsync(CancellationToken)"/>
  private async Task<SiteBreach?> GetLatestBreachInternalAsync(CancellationToken cancellationToken)
  {
    this.ThrowIfDisposed();
    cancellationToken.ThrowIfCancellationRequested();

    var uri = UriFactory.GetLatestBreachUri();

    using var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

    var result = await this.GetAsync<SiteBreach>(requestMessage, cancellationToken)
      .ConfigureAwait(false);

    return result;
  }

  /// <inheritdoc cref="IHaveIBeenPwnedPastesClient.GetPastesAsync(string, CancellationToken)"/>
  private async Task<IEnumerable<Paste>> GetPastesInternalAsync(string emailAddress, CancellationToken cancellationToken)
  {
    Throw.ArgumentNull.WhenNullOrWhiteSpace(emailAddress, nameof(emailAddress));
    this.ThrowIfDisposed();
    cancellationToken.ThrowIfCancellationRequested();

    var requestUri = UriFactory.GetPasteAccountUri(emailAddress);

    var results = await this.GetAuthenticatedAsync<IEnumerable<Paste>>(requestUri, cancellationToken)
      .ConfigureAwait(false);

    return results ?? Enumerable.Empty<Paste>();
  }

  /// <inheritdoc cref="IHaveIBeenPwnedPasswordClient.IsPwnedPasswordAsync(string, CancellationToken)"/>
  private async Task<bool> IsPwnedPasswordInternalAsync(string password, CancellationToken cancellationToken)
  {
    Throw.ArgumentNull.WhenNullOrWhiteSpace(password, nameof(password));
    this.ThrowIfDisposed();
    cancellationToken.ThrowIfCancellationRequested();

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
#if NET6_0_OR_GREATER
        .ReadAsStringAsync(cancellationToken)
#else
        .ReadAsStringAsync()
#endif
        .ConfigureAwait(false);

      result = content.Contains(kAnonimityRemainder);
    }

    return result;
  }

  /// <inheritdoc cref="IHaveIBeenPwnedDomainClient.GetBreachedDomainUsersAsync(string, CancellationToken)"/>
  private async Task<IEnumerable<DomainUser>> GetBreachedDomainUsersInternalAsync(string domain, CancellationToken cancellationToken)
  {
    Throw.ArgumentNull.WhenNullOrWhiteSpace(domain, nameof(domain));
    this.ThrowIfDisposed();
    cancellationToken.ThrowIfCancellationRequested();

    var requestUri = UriFactory.GetBreachedDomainUsersUri(domain);

    var records = await this.GetAuthenticatedAsync<Dictionary<string, IEnumerable<string>>>(requestUri, cancellationToken)
      .ConfigureAwait(false);

    var result = Enumerable.Empty<DomainUser>();

    if (records is not null)
    {
      var users = new List<DomainUser>();

      foreach (var currentRecord in records)
      {
        var newDomainUser = new DomainUser()
        {
          Alias = currentRecord.Key,
          Breaches = currentRecord.Value,
        };

        users.Add(newDomainUser);
      }

      result = users;
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
  private async Task<T?> GetAuthenticatedAsync<T>(Uri url, CancellationToken cancellationToken)
    where T : notnull
  {
    Throw<InvalidApiKeyException>.When(this._clientSettings.ApiKey.IsNullOrWhiteSpace());
    this.ThrowIfDisposed();
    cancellationToken.ThrowIfCancellationRequested();

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
  private async Task<T?> GetAsync<T>(HttpRequestMessage requestMessage, CancellationToken cancellationToken)
    where T : notnull
  {
    this.ThrowIfDisposed();
    cancellationToken.ThrowIfCancellationRequested();

    using var response = await this.ExecuteRequestAsync(requestMessage, cancellationToken).ConfigureAwait(false);

    T? result = default;

    if (response.IsSuccessStatusCode)
    {
      using var content = await response
     .Content
#if NET6_0_OR_GREATER
       .ReadAsStreamAsync(cancellationToken)
#else
        .ReadAsStreamAsync()
#endif
     .ConfigureAwait(false);

      result = await JsonSerializer
     .DeserializeAsync<T>(content, JsonOptions, cancellationToken)
     .ConfigureAwait(false);
    }
    else
    {
      HandleErrorResponse(response);
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
    cancellationToken.ThrowIfCancellationRequested();

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

    var requestUri = response.RequestMessage?.RequestUri?.AbsoluteUri;

    switch (statusCode)
    {
      // Rate limit exceeded
      case 429:
        {
          // If we don't get a retry-after value from the HaveIBeenPwnedService,
          // we revert to the default value specified in the docs (https://haveibeenpwned.com/API/v3#RateLimiting)
          var retryAfter = response.Headers.RetryAfter?.Delta ?? Constants.Communication.Http.DefaultRetryValue.MilliSeconds();
          throw new RateLimitExceededException(retryAfter);
        }
      // Unauthorized
      case 401:
        {
          throw new InvalidApiKeyException();
        }
      // Forbidden
      // When trying to access the users of a domain that the
      // specified API key doesn't have access to, a 403 is returned.
      case 403 when requestUri!.StartsWith(Constants.Uris.BreachedDomainBaseUri, StringComparison.OrdinalIgnoreCase):
        {
          throw new DomainForbiddenException();
        }
      // Not found
      // This is only valid for breaches for an account. Pastes for an account must return an empty collection when nothing
      // is available according to the API documentation and Pwned passwords should never return a 404. So we can only
      // ignore 404s for the breaches for an account.
      case 404 when requestUri!.StartsWith(Constants.Uris.BreachedAccountBaseUri, StringComparison.OrdinalIgnoreCase):
      case 404 when requestUri!.StartsWith(Constants.Uris.PasteAccountBaseUri, StringComparison.OrdinalIgnoreCase):
        {
          return; // Do nothing
        }
      // Service unavailable
      case 503:
        {
          throw new HaveIBeenPwnedClientException("The service is currently unavailable");
        }
      default:
        {
          throw new HaveIBeenPwnedClientException($"An error occured ({statusCode} {response.ReasonPhrase})");
        }
    }
  }
}
