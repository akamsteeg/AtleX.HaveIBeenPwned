﻿using AtleX.HaveIBeenPwned.Helpers;
using SwissArmyKnife.Helpers;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace AtleX.HaveIBeenPwned;

/// <summary>
/// Represents a breach from a paste
/// </summary>
/// <remarks>
/// See <see href="https://haveibeenpwned.com/API/v3#PasteModel"/> for more information
/// </remarks>
[DebuggerDisplay("{Title}")]
[ExcludeFromCodeCoverage]
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
  public bool Equals(Paste? other) => EqualityHelper.Equals(this, other);

  /// <summary>
  /// Serves as the default hash function
  /// </summary>
  /// <returns>
  /// A hash code for the current object
  /// </returns>
  public override int GetHashCode() => HashCodeHelper.GetHashCode(this.Id!);

  /// <summary>
  /// Returns a string that represents the current object
  /// </summary>
  /// <returns>
  /// A string that represents the current object
  /// </returns>
  public override string ToString() => this.Title ?? string.Empty;
}
