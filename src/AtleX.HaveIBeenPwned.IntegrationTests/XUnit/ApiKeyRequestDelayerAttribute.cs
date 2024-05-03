// Copyright (c) Alex Kamsteeg (https://atlex.nl/)
// License: MIT (See LICENSE file)

using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using Xunit.Sdk;

namespace AtleX.HaveIBeenPwned.IntegrationTests.XUnit;

/// <summary>
/// Represents a tests that requires an API key
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
internal sealed class ApiKeyRequestDelayerAttribute
  : BeforeAfterTestAttribute
{
  /// <inheritDoc />
  public override void After(MethodInfo methodUnderTest)
  {
    base.After(methodUnderTest);

    // Wait to prevent rate limit exceptions
    Trace.WriteLine($"Sleeping for {Constants.Tests.DelayBetweenTests.TotalMilliseconds} ms. after test '{methodUnderTest.Name}'...");
    Thread.Sleep(Constants.Tests.DelayBetweenTests);
  }
}
