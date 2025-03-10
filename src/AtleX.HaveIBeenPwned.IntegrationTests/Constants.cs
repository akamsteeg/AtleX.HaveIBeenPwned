// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

using System;
using SwissArmyKnife;

namespace AtleX.HaveIBeenPwned.IntegrationTests;

internal static class Constants
{
  public static class Tests
  {
    /// <summary>
    /// Gets the <see cref="TimeSpan"/> to wait at the end of each test to prevent hotting the rate limit
    /// </summary>
    public static readonly TimeSpan DelayBetweenTests = 6050.MilliSeconds();

    /// <summary>
    /// Gets the name of the integration tests to use as the application name
    /// when calling the HaveIBeenPwned.com API
    /// </summary>
    public const string ApplicationName = "AtleX.HaveIBeenPwned.IntegrationTests";

    /// <summary>
    /// Represents the test categories used
    /// </summary>
    public static class Categories
    {
      /// <summary>
      /// Represents a test category for tests that require an API key
      /// </summary>
      public static class RequiresApiKeyCategory
      {
        /// <summary>
        /// Gets the name of the test category
        /// </summary>
        public const string Name = "RequiresApiKey";
      }
    }
  }
}
