using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace AtleX.HaveIBeenPwned
{
  /// <summary>
  /// Represents a single breach in the system
  /// </summary>
  [DebuggerDisplay("{Name}")]
  [ExcludeFromCodeCoverage]
  public sealed class Breach
  {
    /// <summary>
    /// Gets or sets the name of the breach
    /// </summary>
    public string Name { get; set; } = default!;
  }
}
