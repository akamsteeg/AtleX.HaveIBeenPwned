using AtleX.HaveIBeenPwned.Helpers;
using System;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests.Helpers;

public class KAnonimityHelperTests
{
  [Theory]
  [InlineData("DUMMY", "9600B", "5F6438B9ED6A23BBEF20A8C2B0C53A39449")]
  public void GetKAnonimityPartsForPassword_WithPassword_GeneratesValidKAnonimityParts(string password, string expectedKAnonimityPart, string expectedKAnonimityRemainder)
  {
    var (kAnonimityPart, kAnonimityRemainder) = KAnonimityHelper.GetKAnonimityPartsForPassword(password);

    Assert.NotNull(kAnonimityPart);
    Assert.Equal(5, kAnonimityPart.Length);
    Assert.Equal(expectedKAnonimityPart, kAnonimityPart);

    Assert.NotNull(kAnonimityRemainder);
    Assert.Equal(35, kAnonimityRemainder.Length);
    Assert.Equal(expectedKAnonimityRemainder, kAnonimityRemainder);
  }

  [Theory]
  [InlineData("")]
  [InlineData((string)null)]
  public void GetKAnonimityPartsForPassword_WithNullOrEmptyPassword_ThrowsArgumentNullException(string password)
  {
    Assert.Throws<ArgumentNullException>(() => KAnonimityHelper.GetKAnonimityPartsForPassword(password));
  }
}
