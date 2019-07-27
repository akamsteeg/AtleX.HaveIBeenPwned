using AtleX.HaveIBeenPwned.Helpers;
using System;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests.Helpers
{
  public class KAnonimityHelperTests
  {
    [Fact]
    public void GetKAnonimityPartsForPassword_WithEmptyPassword_Throws()
    {
      Assert.Throws<ArgumentNullException>(() => KAnonimityHelper.GetKAnonimityPartsForPassword(""));
    }

    [Fact]
    public void GetKAnonimityPartsForPassword_WithPassword_GeneratesValidKAnonimityParts()
    {
      var (kAnonimityPart, kAnonimitySuffix) = KAnonimityHelper.GetKAnonimityPartsForPassword("DUMMY");

      Assert.NotNull(kAnonimityPart);
      Assert.Equal(5, kAnonimityPart.Length);
      Assert.Equal("9600B", kAnonimityPart);

      Assert.NotNull(kAnonimitySuffix);
      Assert.Equal(35, kAnonimitySuffix.Length);
      Assert.Equal("5F6438B9ED6A23BBEF20A8C2B0C53A39449", kAnonimitySuffix);
    }
  }
}
