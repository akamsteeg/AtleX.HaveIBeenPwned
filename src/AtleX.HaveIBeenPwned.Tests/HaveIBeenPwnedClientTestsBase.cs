// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

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
      RequestPaddingForPwnedPasswordResponses = true,
    };

    this.ClientSettings = settings;
  }
}
