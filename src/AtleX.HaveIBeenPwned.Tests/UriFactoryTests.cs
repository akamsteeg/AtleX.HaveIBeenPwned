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

    [Fact]
    public void GetPasteAccountUri_ReturnsCorrectUri()
    {
      const string account = "TESTACCOUNT";

      var expected = new Uri($"https://haveibeenpwned.com/api/v3/pasteaccount/{account}");

      var actual = UriFactory.GetPasteAccountUri(account);

      Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetPwnedPasswordUri_ReturnsCorrectUri()
    {
      const string kAnonimitySuffix = "TESTSUFFIS";

      var expected = new Uri($"https://api.pwnedpasswords.com/range/{kAnonimitySuffix}");

      var actual = UriFactory.GetPwnedPasswordUri(kAnonimitySuffix);

      Assert.Equal(expected, actual);
    }
  }
}
