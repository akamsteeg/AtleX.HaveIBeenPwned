using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using System;
using AtleX.HaveIBeenPwned.Helpers;
using SwissArmyKnife.Helpers;
using System.Linq;

namespace AtleX.HaveIBeenPwned;

/// <summary>
/// Represents a user of a domain with breaches
/// </summary>
/// <remarks>
/// See <see href="https://haveibeenpwned.com/API/v3#BreachesForDomain"/> for
/// more information
/// </remarks>
[DebuggerDisplay("{Alias}")]
[ExcludeFromCodeCoverage]
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
  public IEnumerable<string> Breaches { get; set; } = Enumerable.Empty<string>();

  /// <summary>
  /// Determines whether the specified object is equal to the current object
  /// </summary>
  /// <param name="obj">
  /// The object to compare with the current object
  /// </param>
  /// <returns>
  /// True if the specified object is equal to the current object; false otherwise
  /// </returns>
  public override bool Equals(object? obj) => EqualityHelper.Equals(this, obj);

  /// <summary>
  /// Indicates whether the current object is equal to another object of the
  /// same type
  /// </summary>
  /// <param name="other">
  /// An object to compare with this object
  /// </param>
  /// <returns>
  /// True if the current object is equal to the other parameter; false otherwise
  /// </returns>
  public bool Equals(DomainUser? other) => EqualityHelper.Equals(this, other);

  /// <summary>
  /// Serves as the default hash function
  /// </summary>
  /// <returns>
  /// A hash code for the current object
  /// </returns>
  public override int GetHashCode() => HashCodeHelper.GetHashCode(this.Alias!);

  /// <summary>
  /// Returns a string that represents the current object
  /// </summary>
  /// <returns>
  /// A string that represents the current object
  /// </returns>
  public override string ToString() => this.Alias ?? string.Empty;
}
