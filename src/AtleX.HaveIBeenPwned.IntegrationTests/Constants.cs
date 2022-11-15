using SwissArmyKnife;
using System;

namespace AtleX.HaveIBeenPwned.IntegrationTests;
internal static class Constants
{
  public static class Tests
  {
    /// <summary>
    /// Gets the <see cref="TimeSpan"/> to wait at the end of each test to prevent hotting the rate limit
    /// </summary>
    public static readonly TimeSpan DelayBetweenTests = 7.Seconds();

    public const string ApplicationName = "AtleX.HaveIBeenPwned.IntegrationTests";

    public static class Categories
    {
      public static class RequiresApiKeyCategory
      {
        public const string Name = "RequiresApiKey";
      }
    }
  }
}
