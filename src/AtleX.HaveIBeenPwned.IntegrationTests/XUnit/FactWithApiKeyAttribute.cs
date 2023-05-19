using SwissArmyKnife;
using Xunit;

namespace AtleX.HaveIBeenPwned.IntegrationTests.XUnit;

/// <summary>
/// Attribute that is applied to a method to indicate that it is a fact that should be run
/// by the test runner, but only if the required API key is set
/// </summary>
internal sealed class FactWithApiKeyAttribute : FactAttribute
{
  /// <summary>
  /// Initializes a new instance of <see cref="FactWithApiKeyAttribute"/>
  /// </summary>
  public FactWithApiKeyAttribute()
  {
    if (PrivateConstants.ApiKey.IsNullOrEmpty())
    {
      this.Skip = "No API key set";
    }
  }
}
