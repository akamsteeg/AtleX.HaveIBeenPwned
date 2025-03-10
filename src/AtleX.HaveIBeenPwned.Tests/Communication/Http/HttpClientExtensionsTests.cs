// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

using System;
using System.Net.Http;
using AtleX.HaveIBeenPwned.Communication.Http;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests.Communication.Http;

public class HttpClientExtensionsTests
{
  private static readonly HaveIBeenPwnedClientSettings _settings = new()
  {
    ApplicationName = "UNITTESTS"
  };

  [Fact]
  public void ConfigureHttpClient_SetsCorrectAcceptHeader()
  {
    var hc = new HttpClient();

    hc = hc.ConfigureForHaveIBeenPwnedApi(_settings);

    Assert.Equal(Constants.Communication.Http.MediaType, hc.DefaultRequestHeaders.Accept.ToString());
  }

  [Fact]
  public void ConfigureHttpClient_SetsCorrectApplicationName()
  {
    var hc = new HttpClient();

    hc = hc.ConfigureForHaveIBeenPwnedApi(_settings);

    Assert.Equal(_settings.ApplicationName, hc.DefaultRequestHeaders.UserAgent.ToString());
  }

  [Theory]
  [InlineData((string)null)]
  [InlineData("")]
  public void ConfigureHttpClient_WithNullOrEmptyApplicationName_Throws(string applicationName)
  {
    var hc = new HttpClient();

    var s = new HaveIBeenPwnedClientSettings()
    {
      ApplicationName = applicationName
    };

    Assert.Throws<ArgumentNullException>(() => hc.ConfigureForHaveIBeenPwnedApi(s));
  }
}
