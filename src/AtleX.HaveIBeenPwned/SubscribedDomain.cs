// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

using AtleX.HaveIBeenPwned.Helpers;
using SwissArmyKnife.Helpers;
using System;
using System.Diagnostics;

namespace AtleX.HaveIBeenPwned;

/// <summary>
/// Represents a breached domain in the system
/// </summary>
[DebuggerDisplay("{DomainName}")]
public sealed class SubscribedDomain
  : IEquatable<SubscribedDomain>
{
  /// <summary>
  /// Gets or sets the domain name
  /// </summary>
  public string? DomainName { get; set; }

  /// <summary>
  /// Gets or sets the number of pwned email addresses of the domain
  /// </summary>
  public int? PwnCount { get; set; }

  /// <summary>
  /// Gets or sets the number of pwned email addresses of the domain, excluding spam lists
  /// </summary>
  public int PwnCountExcludingSpamLists { get; set; }

  /// <summary>
  /// Gets or sets the number of pwned email addresses of the domain, excluding
  /// spam lists, at the time of the last subscription renewal. This can be used
  /// to determine if new email addresses were found in breaches
  /// </summary>
  public int PwnCountExcludingSpamListsAtLastSubscriptionRenewal { get; set; }

  /// <summary>
  /// Gets or sets the date when the subscription is up for renewal.
  /// </summary>
  public DateTime? NextSubscriptionRenewal { get; set; }

  /// <inheritDoc />
  public override bool Equals(object? obj) => this == obj;

  /// <inheritDoc />
  public bool Equals(SubscribedDomain? other) => this == other;

  /// <inheritDoc />
  public override int GetHashCode() => HashCodeHelper.GetHashCode(this.DomainName!);

  /// <inheritDoc />
  public override string ToString() => this.DomainName ?? string.Empty;

  /// <inheritDoc />
  public static bool operator ==(SubscribedDomain? left, SubscribedDomain? right) => EqualityHelper.Equals(left, right);

  /// <inheritDoc />
  public static bool operator ==(SubscribedDomain? left, object? right) => EqualityHelper.Equals(left, right);

  /// <inheritDoc />
  public static bool operator !=(SubscribedDomain? left, SubscribedDomain? right) => !EqualityHelper.Equals(left, right);

  /// <inheritDoc />
  public static bool operator !=(SubscribedDomain? left, object? right) => !EqualityHelper.Equals(left, right);
}
