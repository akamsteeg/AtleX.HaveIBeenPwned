namespace AtleX.HaveIBeenPwned.IntegrationTests
{
  public abstract class HaveIBeenPwnedClientIntegrationTestsBase
  {
    public HaveIBeenPwnedClientSettings ClientSettings
    {
      get;
    }

    public HaveIBeenPwnedClientIntegrationTestsBase()
    {
      var settings = new HaveIBeenPwnedClientSettings()
      {
        ApiKey = "DUMMYKEY",
        ApplicationName = "AtleX.HaveIBeenPwned.IntegrationTests",
      };

      this.ClientSettings = settings;
    }
  }
}
