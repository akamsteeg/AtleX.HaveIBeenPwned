﻿using Pitcher;
using System;

namespace AtleX.HaveIBeenPwned;

/// <summary>
/// Represents a factory to create a <see cref="Uri"/> for all actions of the
/// HaveIBeenPwned API
/// </summary>
internal static class UriFactory
{
  /// <summary>
  /// Gets the bas euri of the breaches endpoint
  /// </summary>
  private static readonly Uri BreachesUri = new(Constants.Uris.BreachesUri);

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
    Throw.ArgumentNull.WhenNullOrEmpty(account, nameof(account));

    Uri? result;

    var baseUri = $"{Constants.Uris.BreachedAccountBaseUri}/{account}";

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
      result = new(baseUri);
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
    Throw.ArgumentNull.WhenNullOrEmpty(emailAddress, nameof(emailAddress));

    var result = new Uri($"{Constants.Uris.PasteAccountBaseUri}/{emailAddress}");

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
    Throw.ArgumentNull.WhenNullOrEmpty(kAnonimitySuffixPart, nameof(kAnonimitySuffixPart));

    var result = new Uri($"{Constants.Uris.PwnedPasswordsBaseUri}/{kAnonimitySuffixPart}");

    return result;
  }
}
