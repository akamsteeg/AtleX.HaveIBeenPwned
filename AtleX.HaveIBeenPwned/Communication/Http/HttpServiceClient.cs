using AtleX.HaveIBeenPwned.Data;
using Pitcher;
using SwissArmyKnife;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
    /// Gets the base uri of the HaveIBeenPwned.com API
    /// </summary>
    private const string BaseUri = "https://haveibeenpwned.com/api/v2/";

    /// <summary>
    /// Initializes a new instance of <see cref="HttpServiceClient"/> with the
    /// specified <see cref="ClientSettings"/>
    /// </summary>
    /// <param name="settings">
    /// The <see cref="ClientSettings"/> to use
    /// </param>
    public HttpServiceClient(ClientSettings settings)
    {
      this._clientSettings = settings ?? throw new ArgumentNullException(nameof(settings));

      this._httpClient = CreateHttpClient(settings);
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

      var uriBuilder = new UriBuilder($"{BaseUri}breachedaccount/{account}");

      if (modes == BreachMode.All || (modes & BreachMode.IncludeUnverified) == BreachMode.IncludeUnverified)
      {
        uriBuilder.Query = "includeUnverified=true";
      }

      var results = await this.GetAsync<IEnumerable<Breach>>(uriBuilder.Uri)
        .ConfigureAwait(false);

      return results;
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
    /// <typeparam name="T">
    /// The type of data to get from the service
    /// </typeparam>
    /// <param name="url">
    /// The uri to request
    /// </param>
    /// <returns>
    /// An awaitable <see cref="Task{TResult}"/>
    /// </returns>
    private async Task<T> GetAsync<T>(Uri url)
    {
      this.ThrowIfDisposed();

      var response = await this._httpClient
        .GetAsync(url)
        .ConfigureAwait(false);

      T result = default;
      try
      {
        if (response.IsSuccessStatusCode)
        {
          var content = await response.Content
            .ReadAsStringAsync()
            .ConfigureAwait(false);

          result = JsonConvert.DeserializeObject<T>(content);
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
      if ((int)response.StatusCode == 429) // Rate limit exceeded
      {
        var retryAfter = response.Headers.RetryAfter.Delta?.Seconds;
        throw new RateLimitExceededException(retryAfter.Value);
      }
      else
      {
        throw new HaveIBeenPwnedClientException($"An error occured ({response.StatusCode.ToString()})");
      }
    }

    /// <summary>
    /// Create a new <see cref="HttpClient"/> with the specified <see cref="ClientSettings"/>
    /// </summary>
    /// <param name="settings">
    /// The <see cref="ClientSettings"/>
    /// </param>
    /// <returns>
    /// A new <see cref="HttpClient"/>
    /// </returns>
    private static HttpClient CreateHttpClient(ClientSettings settings)
    {
      var result = new HttpClient();
      result.DefaultRequestHeaders.Add("Accept", "application/json");
      result.DefaultRequestHeaders.Add("User-Agent", settings.ApplicationName);
      result.Timeout = settings.TimeOut;

      return result;
    }
  }
}
