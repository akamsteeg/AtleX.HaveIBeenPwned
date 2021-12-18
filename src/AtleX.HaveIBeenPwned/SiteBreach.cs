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
[DebuggerDisplay("{Name}")]
[ExcludeFromCodeCoverage]
public sealed class SiteBreach
  : IEquatable<SiteBreach>
{
  /// <summary>
  /// Gets or sets the name of the breach
  /// </summary>
  public string? Name { get; set; }

  /// <summary>
  /// Gets or sets the title of the breach
  /// </summary>
  public string? Title { get; set; }

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
  /// Determines whether the specified object is equal to the current object
  /// </summary>
  /// <param name="obj">
  /// The object to compare with the current object
  /// </param>
  /// <returns>
  /// True if the specified object is equal to the current object; false otherwise
  /// </returns>
  public override bool Equals(object obj) => EqualityHelper.Equals(this, obj);

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
  public bool Equals(SiteBreach other) => EqualityHelper.Equals(this, other);

  /// <summary>
  /// Serves as the default hash function
  /// </summary>
  /// <returns>
  /// A hash code for the current object
  /// </returns>
  public override int GetHashCode() => HashCodeHelper.GetHashCode(this.Name ?? string.Empty);

  /// <summary>
  /// Returns a string that represents the current object
  /// </summary>
  /// <returns>
  /// A string that represents the current object
  /// </returns>
  public override string ToString() => this.Name ?? string.Empty;
}
