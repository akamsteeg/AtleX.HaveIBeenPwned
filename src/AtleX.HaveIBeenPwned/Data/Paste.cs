using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace AtleX.HaveIBeenPwned.Data
{
  /// <summary>
  /// Represents a breach from a paste
  /// </summary>
  [DebuggerDisplay("{Title}")]
  [ExcludeFromCodeCoverage]
  public sealed class Paste
  {
    /// <summary>
    /// Gets or sets the source of the paste
    /// </summary>
    public string Source { get; set; }

    /// <summary>
    /// Gets or sets the Id of the paste
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the paste
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="DateTime"/> the paste was posted
    /// </summary>
    public DateTime? Date { get; set; }

    /// <summary>
    /// Gets or sets the number of accounts in the paste
    /// </summary>
    public int EmailCount { get; set; }
  }
}
