using AtleX.HaveIBeenPwned.Helpers;
using System;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests.Helpers
{
  public class KAnonimityHelperTests
  {
    [Fact]
    public void GetKAnonimityPartsForPassword_WithPassword_GeneratesValidKAnonimityParts()
    {
      var (kAnonimityPart, kAnonimityRemainder) = KAnonimityHelper.GetKAnonimityPartsForPassword("DUMMY");

      Assert.NotNull(kAnonimityPart);
      Assert.Equal(5, kAnonimityPart.Length);
      Assert.Equal("9600B", kAnonimityPart);

      Assert.NotNull(kAnonimityRemainder);
      Assert.Equal(35, kAnonimityRemainder.Length);
      Assert.Equal("5F6438B9ED6A23BBEF20A8C2B0C53A39449", kAnonimityRemainder);
    }
  }
}
