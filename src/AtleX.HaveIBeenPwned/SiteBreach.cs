// Copyright (c) Alex Kamsteeg (https://atlex.nl/)
// License: MIT (See LICENSE file)

using AtleX.HaveIBeenPwned.Helpers;
using SwissArmyKnife.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace AtleX.HaveIBeenPwned;

/// <summary>
/// Represents a single site breach in the system
/// </summary>
/// <remarks>
/// See <see href="https://haveibeenpwned.com/API/v3#BreachModel"/> for more information
/// </remarks>
[DebuggerDisplay("{Name}")]
public sealed class SiteBreach
  : IEquatable<SiteBreach>
{
  /// <summary>
  /// Gets or sets the name of the breach. This is the identifier of the breach
  /// </summary>
  public string? Name { get; set; }

  /// <summary>
  /// Gets or sets the title of the breach
  /// </summary>
  public string? Title { get; set; }

  /// <summary>
  /// Gets or sets the title of the breach
  /// </summary>
  public string? Domain { get; set; }

  /// <summary>
  /// Gets or sets the date of the breach
  /// </summary>
  public DateTime BreachDate { get; set; }

  /// <summary>
  /// Gets or sets the date when the breach was added to HaveIBeenPwned.com
  /// </summary>
  public DateTime AddedDate { get; set; }

  /// <summary>
  /// Gets or sets the date when the breach was last modified
  /// </summary>
  public DateTime ModifiedDate { get; set; }

  /// <summary>
  /// Gets or sets the number of accounts in the breach
  /// </summary>
  public int PwnCount { get; set; }

  /// <summary>
  /// Gets or sets the description of the breach
  /// </summary>
  public string? Description { get; set; }

  /// <summary>
  /// Gets or sets the types of data included in the breach
  /// </summary>
  public IEnumerable<string>? DataClasses { get; set; }

  /// <summary>
  /// Gets or sets whether the breach is verified or not
  /// </summary>
  public bool IsVerified { get; set; }

  /// <summary>
  /// Gets or sets whether the breach is fabricated or not
  /// </summary>
  public bool IsFabricated { get; set; }

  /// <summary>
  /// Gets or sets whether the breach is sensitive or not
  /// </summary>
  public bool IsSensitive { get; set; }

  /// <summary>
  /// Gets or sets whether the breach is retired or not
  /// </summary>
  public bool IsRetired { get; set; }

  /// <summary>
  /// Gets or sets whether the breach is from a spam list or not
  /// </summary>
  public bool IsSpamList { get; set; }

  /// <summary>
  /// Gets or sets whether the breach is from malware or not
  /// </summary>
  public bool IsMalware { get; set; }

  /// <summary>
  /// Gets or sets whether the breach is from malware or not
  /// </summary>
  public Uri? LogoPath { get; set; }

  /// <inheritDoc />
  public override bool Equals(object? obj) => this == obj;

  /// <inheritDoc />
  public bool Equals(SiteBreach? other) => this == other;

  /// <inheritDoc />
  public override int GetHashCode() => HashCodeHelper.GetHashCode(this.Name!);

  /// <inheritDoc />
  public override string ToString() => this.Name!;

  /// <inheritDoc />
  public static bool operator ==(SiteBreach? left, SiteBreach? right) => EqualityHelper.Equals(left, right);

  /// <inheritDoc />
  public static bool operator ==(SiteBreach? left, object? right) => EqualityHelper.Equals(left, right);

  /// <inheritDoc />
  public static bool operator !=(SiteBreach? left, SiteBreach? right) => !EqualityHelper.Equals(left, right);

  /// <inheritDoc />
  public static bool operator !=(SiteBreach? left, object? right) => !EqualityHelper.Equals(left, right);
}
