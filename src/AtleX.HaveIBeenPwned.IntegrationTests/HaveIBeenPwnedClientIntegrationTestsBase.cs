﻿using System;
using System.Threading;

namespace AtleX.HaveIBeenPwned.IntegrationTests;

public abstract class HaveIBeenPwnedClientIntegrationTestsBase
{
  protected static HaveIBeenPwnedClientSettings CreateSettings()
  {
    var result = new HaveIBeenPwnedClientSettings()
    {
      ApiKey = PrivateSettings.ApiKey,
      ApplicationName = Constants.Tests.ApplicationName,
    };

    return result;
  }
}
