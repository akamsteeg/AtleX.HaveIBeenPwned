// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

using System;
using System.Collections.Generic;
using System.Diagnostics;
using AtleX.HaveIBeenPwned.Helpers;
using SwissArmyKnife.Helpers;

namespace AtleX.HaveIBeenPwned;

/// <summary>
/// Represents a user of a domain with breaches
/// </summary>
/// <remarks>
/// See <see href="https://haveibeenpwned.com/API/v3#BreachesForDomain"/> for
/// more information
/// </remarks>
[DebuggerDisplay("{Alias}")]
public sealed class DomainUser
  : IEquatable<DomainUser>
{
  /// <summary>
  /// <para>
  /// Gets the alias of the user
  /// </para>
  /// <para>
  /// Generally, this is the email address without the domain. E.g. when
  /// searching the domain "example.com" it will return "user@" as the alias
  /// </para>
  /// </summary>
  public string? Alias { get; set; }

  /// <summary>
  /// The collection of names of breaches. This maps to <see cref="Breach.Name"/>.
  /// </summary>
  public IEnumerable<string> Breaches { get; set; } = [];

  /// <inheritDoc />
  public override bool Equals(object? obj) => this == obj;

  /// <inheritDoc />
  public bool Equals(DomainUser? other) => this == other;

  /// <inheritDoc />
  public override int GetHashCode() => HashCodeHelper.GetHashCode(this.Alias!);

  /// <inheritDoc />
  public override string ToString() => this.Alias ?? string.Empty;

  /// <inheritDoc />
  public static bool operator ==(DomainUser? left, DomainUser? right) => EqualityHelper.Equals(left, right);

  /// <inheritDoc />
  public static bool operator ==(DomainUser? left, object? right) => EqualityHelper.Equals(left, right);

  /// <inheritDoc />
  public static bool operator !=(DomainUser? left, DomainUser? right) => !EqualityHelper.Equals(left, right);

  /// <inheritDoc />
  public static bool operator !=(DomainUser? left, object? right) => !EqualityHelper.Equals(left, right);
}
