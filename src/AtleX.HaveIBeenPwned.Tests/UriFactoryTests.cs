using System;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests
{
  public class UriFactoryTests
  {
    [Fact]
    public void GetAllBreachesUri_ReturnsCorrectUri()
    {
      var expected = new Uri("https://haveibeenpwned.com/api/v3/breaches");

      var actual = UriFactory.GetAllBreachesUri();

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
      const string kAnonimitySuffix = "TESTSUFFIS";

      var expected = new Uri($"https://api.pwnedpasswords.com/range/{kAnonimitySuffix}");

      var actual = UriFactory.GetPwnedPasswordUri(kAnonimitySuffix);

      Assert.Equal(expected, actual);
    }
  }
}
