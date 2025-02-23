// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

using System;
using System.Diagnostics;
using AtleX.HaveIBeenPwned.Helpers;
using SwissArmyKnife.Helpers;

namespace AtleX.HaveIBeenPwned;

/// <summary>
/// Represents a breach from a paste
/// </summary>
/// <remarks>
/// See <see href="https://haveibeenpwned.com/API/v3#PasteModel"/> for more information
/// </remarks>
[DebuggerDisplay("{Title}")]
public sealed class Paste
  : IEquatable<Paste>
{
  /// <summary>
  /// Gets or sets the source of the paste
  /// </summary>
  public string? Source { get; set; }

  /// <summary>
  /// Gets or sets the Id of the paste
  /// </summary>
  public string? Id { get; set; }

  /// <summary>
  /// Gets or sets the title of the paste
  /// </summary>
  public string? Title { get; set; }

  /// <summary>
  /// Gets or sets the <see cref="DateTime"/> the paste was posted
  /// </summary>
  public DateTime? Date { get; set; }

  /// <summary>
  /// Gets or sets the number of accounts in the paste
  /// </summary>
  public int EmailCount { get; set; }

  /// <inheritDoc />
  public override bool Equals(object? obj) => this == obj;

  /// <inheritDoc />
  public bool Equals(Paste? other) => this == other;

  /// <inheritDoc />
  public override int GetHashCode() => HashCodeHelper.GetHashCode(this.Id!);

  /// <inheritDoc />
  public override string ToString() => this.Title ?? string.Empty;

  /// <inheritDoc />
  public static bool operator ==(Paste? left, Paste? right) => EqualityHelper.Equals(left, right);

  /// <inheritDoc />
  public static bool operator ==(Paste? left, object? right) => EqualityHelper.Equals(left, right);

  /// <inheritDoc />
  public static bool operator !=(Paste? left, Paste? right) => !EqualityHelper.Equals(left, right);

  /// <inheritDoc />
  public static bool operator !=(Paste? left, object? right) => !EqualityHelper.Equals(left, right);
}
