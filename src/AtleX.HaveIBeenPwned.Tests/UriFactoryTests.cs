// Copyright (c) Alex Kamsteeg (https://atlex.nl/)
// License: MIT (See LICENSE file)

using System;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests;

public class UriFactoryTests
{
  [Fact]
  public void GetAllBreachesUri_ReturnsCorrectUri()
  {
    var expected = new Uri("https://haveibeenpwned.com/api/v3/breaches");

    var actual = UriFactory.GetAllBreachesUri();

    Assert.Equal(expected, actual);
  }


  [Fact]
  public void GetLatestBreachUri_ReturnsCorrectUri()
  {
    var expected = new Uri("https://haveibeenpwned.com/api/v3/latestbreach");

    var actual = UriFactory.GetLatestBreachUri();

    Assert.Equal(expected, actual);
  }

  [Theory]
  [InlineData((string)null)]
  [InlineData("")]
  public void GetBreachesForAccountUri_WithNullOrEmptyAccount_ThrowsArgumentNullException(string account)
  {
    Assert.ThrowsAny<ArgumentNullException>(() => UriFactory.GetBreachesForAccountUri(account, BreachMode.Default));
  }

  [Fact]
  public void GetBreachesForAccountUri_WithBreachModeDefault_ReturnsCorrectUri()
  {
    const string account = "TESTACCOUNT";

    var expected = new Uri($"https://haveibeenpwned.com/api/v3/breachedaccount/{account}");

    var actual = UriFactory.GetBreachesForAccountUri(account, BreachMode.Default);

    Assert.Equal(expected, actual);
  }

  [Theory]
  [InlineData(BreachMode.ExcludeUnverified, "includeUnverified=false")]
  public void GetBreachesForAccountUri_WithBreachModeOtherThanDefault_ReturnsCorrectUri(BreachMode breachMode, string expectedQueryStringPart)
  {
    const string account = "TESTACCOUNT";

    var result = UriFactory.GetBreachesForAccountUri(account, breachMode);

    Assert.Contains(expectedQueryStringPart, result.Query);
  }


  [Theory]
  [InlineData((string)null)]
  [InlineData("")]
  public void GetBreachedDomainUsersUri_WithNullOrEmptSuffix_ThrowsArgumentNullException(string domain)
  {
    Assert.ThrowsAny<ArgumentNullException>(() => UriFactory.GetBreachedDomainUsersUri(domain));
  }

  [Fact]
  public void GetBreachedDomainUsersUri_WithCorrectSuffix_ReturnsCorrectUri()
  {
    const string kDomain = "TESTDOMAIN";

    var expected = new Uri($"https://haveibeenpwned.com/api/v3/breacheddomain/{kDomain}");

    var actual = UriFactory.GetBreachedDomainUsersUri(kDomain);

    Assert.Equal(expected, actual);
  }

  [Theory]
  [InlineData((string)null)]
  [InlineData("")]
  public void GetPasteAccountUri_WithNullOrEmptyAccount_ThrowsArgumentNullException(string account)
  {
    Assert.ThrowsAny<ArgumentNullException>(() => UriFactory.GetPasteAccountUri(account));
  }

  [Fact]
  public void GetPasteAccountUri_ReturnsCorrectUri()
  {
    const string account = "TESTACCOUNT";

    var expected = new Uri($"https://haveibeenpwned.com/api/v3/pasteaccount/{account}");

    var actual = UriFactory.GetPasteAccountUri(account);

    Assert.Equal(expected, actual);
  }

  [Theory]
  [InlineData((string)null)]
  [InlineData("")]
  public void GetPwnedPasswordUri_WithNullOrEmptSuffix_ThrowsArgumentNullException(string suffix)
  {
    Assert.ThrowsAny<ArgumentNullException>(() => UriFactory.GetPwnedPasswordUri(suffix));
  }

  [Fact]
  public void GetPwnedPasswordUri_WithCorrectSuffix_ReturnsCorrectUri()
  {
    const string kAnonimitySuffix = "TESTSUFFIX";

    var expected = new Uri($"https://api.pwnedpasswords.com/range/{kAnonimitySuffix}");

    var actual = UriFactory.GetPwnedPasswordUri(kAnonimitySuffix);

    Assert.Equal(expected, actual);
  }

  [Theory]
  [InlineData((string)null)]
  [InlineData("")]
  public void GetBreachedDomainUsersUri_WithNullOrEmptDomain_ThrowsArgumentNullException(string suffix)
  {
    Assert.ThrowsAny<ArgumentNullException>(() => UriFactory.GetBreachedDomainUsersUri(suffix));
  }

  [Fact]
  public void GetBreachedDomainUsersUri_WithCorrectDomain_ReturnsCorrectUri()
  {
    const string domain = "example.com";

    var expected = new Uri($"https://haveibeenpwned.com/api/v3/breacheddomain/{domain}");

    var actual = UriFactory.GetBreachedDomainUsersUri(domain);

    Assert.Equal(expected, actual);
  }

  [Fact]
  public void GetSubscribedDomainsUri_ReturnsCorrectUri()
  {
    var expected = new Uri($"https://haveibeenpwned.com/api/v3/subscribeddomains");

    var actual = UriFactory.GetSubscribedDomainsUri();

    Assert.Equal(expected, actual);
  }
}
