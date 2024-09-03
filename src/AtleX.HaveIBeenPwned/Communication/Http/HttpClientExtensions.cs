// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

using System.Net.Http.Headers;
using System.Net.Http;
using Pitcher;

namespace AtleX.HaveIBeenPwned.Communication.Http;

/// <summary>
/// Represents a configurator for <see cref="HttpClient"/> instances to
/// communicate correctly with the IHaveIBeenPwned API
/// </summary>
internal static class HttpClientExtensions
{
  /// <summary>
  /// Gets the <see cref="MediaTypeWithQualityHeaderValue"/> for communication with the HaveIBeenPwned API
  /// </summary>
  private static readonly MediaTypeWithQualityHeaderValue mediaTypeHeader = new(Constants.Communication.Http.MediaType);

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
  public static HttpClient ConfigureForHaveIBeenPwnedApi(this HttpClient client, HaveIBeenPwnedClientSettings settings)
  {
    Throw.ArgumentNull.WhenNull(client, nameof(client));
    Throw.ArgumentNull.WhenNull(settings, nameof(settings));
    Throw.ArgumentNull.WhenNullOrWhiteSpace(settings.ApplicationName,
      nameof(settings.ApplicationName),
      $"{nameof(HaveIBeenPwnedClientSettings)}.{nameof(settings.ApplicationName)} cannot be null or empty");

    client.DefaultRequestHeaders.Accept.Add(mediaTypeHeader);
    client.DefaultRequestHeaders.Add("User-Agent", settings.ApplicationName);

    return client;
  }
}
