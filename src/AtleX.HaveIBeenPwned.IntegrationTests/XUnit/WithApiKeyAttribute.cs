using System;
using System.Reflection;
using System.Threading;
using Xunit.Sdk;

namespace AtleX.HaveIBeenPwned.IntegrationTests.XUnit;

/// <summary>
/// Represents a tests that requires an API key
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
internal sealed class WithApiKeyAttribute
  : BeforeAfterTestAttribute
{
  /// <inheritDoc />
  public override void After(MethodInfo methodUnderTest)
  {
    base.After(methodUnderTest);

    Thread.Sleep(Constants.Tests.DelayBetweenTests);
  }
}
