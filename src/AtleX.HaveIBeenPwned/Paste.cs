using AtleX.HaveIBeenPwned.Helpers;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace AtleX.HaveIBeenPwned
{
  /// <summary>
  /// Represents a breach from a paste
  /// </summary>
  [DebuggerDisplay("{Title}")]
  [ExcludeFromCodeCoverage]
  public sealed class Paste
    : IEquatable<Paste>
  {
    /// <summary>
    /// Gets or sets the source of the paste
    /// </summary>
    public string? Source { get; set; } = default;

    /// <summary>
    /// Gets or sets the Id of the paste
    /// </summary>
    public string? Id { get; set; } = default;

    /// <summary>
    /// Gets or sets the title of the paste
    /// </summary>
    public string? Title { get; set; } = default;

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
    public override bool Equals(object obj)
    {
      var result = false;

      if (!(obj is null) && obj is Paste other)
      {
        result = this.Equals(other);
      }

      return result;
    }

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
    public bool Equals(Paste other)
    {
      var result = false;

      if (!(other is null))
      {
        result = (this.GetHashCode() == other.GetHashCode());
      }

      return result;
    }

    /// <summary>
    /// Serves as the default hash function
    /// </summary>
    /// <returns>
    /// A hash code for the current object
    /// </returns>
    public override int GetHashCode()
    {
      var result = HashCodeHelper.GetHashCode(this.Id ?? string.Empty, this.Source ?? string.Empty);

      return result;
    }

    /// <summary>
    /// Returns a string that represents the current object
    /// </summary>
    /// <returns>
    /// A string that represents the current object
    /// </returns>
    public override string ToString()
    {
      return this.Title ?? string.Empty;
    }
  }
}
