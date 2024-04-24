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

  /// <inheritDoc />
  public override bool Equals(object? obj) => this == obj;

  /// <inheritDoc />
  public bool Equals(Breach? other) => this == other;

  /// <inheritDoc />
  public override int GetHashCode() => HashCodeHelper.GetHashCode(this.Name!);

  /// <inheritDoc />
  public override string ToString() => this.Name ?? string.Empty;

  /// <inheritDoc />
  public static bool operator ==(Breach? left, Breach? right) => EqualityHelper.Equals(left, right);

  /// <inheritDoc />
  public static bool operator ==(Breach? left, object? right) => EqualityHelper.Equals(left, right);

  /// <inheritDoc />
  public static bool operator !=(Breach? left, Breach? right) => !EqualityHelper.Equals(left, right);

  /// <inheritDoc />
  public static bool operator !=(Breach? left, object? right) => !EqualityHelper.Equals(left, right);
}
