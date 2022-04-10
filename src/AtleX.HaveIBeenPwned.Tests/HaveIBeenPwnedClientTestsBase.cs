namespace AtleX.HaveIBeenPwned.Tests;

public abstract class HaveIBeenPwnedClientTestsBase
{
  public HaveIBeenPwnedClientSettings ClientSettings
  {
    get;
  }

  public HaveIBeenPwnedClientTestsBase()
  {
    var settings = new HaveIBeenPwnedClientSettings()
    {
      ApiKey = "DUMMYKEY",
      ApplicationName = "Unit.Tests",
    };

    this.ClientSettings = settings;
  }
}
