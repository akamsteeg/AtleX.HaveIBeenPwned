namespace AtleX.HaveIBeenPwned.IntegrationTests;

public abstract class HaveIBeenPwnedClientIntegrationTestsBase
{
  protected static HaveIBeenPwnedClientSettings CreateSettings()
  {
    var result = new HaveIBeenPwnedClientSettings()
    {
      ApiKey = "DUMMYKEY",
      ApplicationName = "AtleX.HaveIBeenPwned.IntegrationTests",
    };

    return result;
  }
}
