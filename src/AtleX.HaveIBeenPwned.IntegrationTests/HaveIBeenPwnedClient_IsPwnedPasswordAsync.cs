// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AtleX.HaveIBeenPwned.IntegrationTests;

public class HaveIBeenPwnedClientTests_IsPwnedPasswordAsync
  : HaveIBeenPwnedClientIntegrationTestsBase
{
  [Theory]
  [InlineData("1234", true, true)]
  [InlineData("1234", true, false)]
  [InlineData("{04766D96-39B7-4A26-8E49-046362AB3BCB}", false, false)]
  [InlineData("{04766D96-39B7-4A26-8E49-046362AB3BCB}", false, true)]
  public async Task IsPwnedPassword_WithValidKnownInput_ReturnsTrue(string password, bool isExpected, bool requestPadding)
  {
    var settings = CreateSettings();
    settings.RequestPaddingForPwnedPasswordResponses = requestPadding;

    using var httpClient = new HttpClient();
    using var c = new HaveIBeenPwnedClient(settings, httpClient);

    var result = await c.IsPwnedPasswordAsync(password);

    Assert.Equal(isExpected, result);
  }

  [Theory]
  [InlineData("1234", true, true)]
  [InlineData("1234", true, false)]
  [InlineData("{04766D96-39B7-4A26-8E49-046362AB3BCB}", false, false)]
  [InlineData("{04766D96-39B7-4A26-8E49-046362AB3BCB}", false, true)]
  public async Task IsPwnedPassword_WithValidKnownInputAndCancellationToken_ReturnsTrue(string password, bool isExpected, bool requestPadding)
  {
    var settings = CreateSettings();
    settings.RequestPaddingForPwnedPasswordResponses = requestPadding;

    using var cancellationTokenSource = new CancellationTokenSource();
    using var httpClient = new HttpClient();
    using var c = new HaveIBeenPwnedClient(settings, httpClient);

    var result = await c.IsPwnedPasswordAsync(password, cancellationTokenSource.Token);

    Assert.Equal(isExpected, result);
  }
}
