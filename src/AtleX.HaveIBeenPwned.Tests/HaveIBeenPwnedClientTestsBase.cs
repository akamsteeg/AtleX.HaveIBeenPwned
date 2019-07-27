using SwissArmyKnife;
using System;

namespace AtleX.HaveIBeenPwned.Tests
{
  public abstract class HaveIBeenPwnedClientTestsBase
  {
    public HaveIBeenPwnedClientSettings ClientSettings
    {
      get;
    }

    public HaveIBeenPwnedClientTestsBase()
    {
      var settings = HaveIBeenPwnedClientSettings.Default;

      settings.ApiKey = "DUMMYKEY";

      this.ClientSettings = settings;
    }
  }
}
