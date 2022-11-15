using System;
using System.Threading;

namespace AtleX.HaveIBeenPwned.IntegrationTests;

public abstract class HaveIBeenPwnedClientIntegrationTestsBase
  : IDisposable
{
  public virtual void Dispose()
  {
    // Force a delay between every test to avoid hitting the rate limit
    Thread.Sleep(Constants.Tests.DelayBetweenTests);
  }

  protected static HaveIBeenPwnedClientSettings CreateSettings()
  {
    var result = new HaveIBeenPwnedClientSettings()
    {
      ApiKey = PrivateConstants.ApiKey,
      ApplicationName = "AtleX.HaveIBeenPwned.IntegrationTests",
    };

    return result;
  }
}
