using AtleX.HaveIBeenPwned.Helpers;
using SwissArmyKnife.Helpers;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace AtleX.HaveIBeenPwned;

/// <summary>
/// Represents a single breach in the system
/// </summary>
/// <remarks>
/// See <see href="https://haveibeenpwned.com/API/v3#BreachesForAccount"/> for
/// more information
/// </remarks>
[DebuggerDisplay("{Name}")]
[ExcludeFromCodeCoverage]
public sealed class Breach
  : IEquatable<Breach>
{
  /// <summary>
  /// Gets or sets the name of the breach
  /// </summary>
  public string? Name { get; set; }

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
  public bool Equals(Breach? other) => EqualityHelper.Equals(this, other);

  /// <summary>
  /// Serves as the default hash function
  /// </summary>
  /// <returns>
  /// A hash code for the current object
  /// </returns>
  public override int GetHashCode() => HashCodeHelper.GetHashCode(this.Name!);

  /// <summary>
  /// Returns a string that represents the current object
  /// </summary>
  /// <returns>
  /// A string that represents the current object
  /// </returns>
  public override string ToString() => this.Name ?? string.Empty;
}
