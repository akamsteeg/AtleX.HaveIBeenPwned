// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

using AtleX.HaveIBeenPwned.Helpers;
using System;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests.Helpers;

public class KAnonimityHelperTests
{
  [Theory]
  [InlineData("DUMMY", "9600B", "5F6438B9ED6A23BBEF20A8C2B0C53A39449")]
  [InlineData("-&HxcB_dH+M@BZAX", "8B3D6", "3D8F7F721C1D728A23B31B01939DFCA265B")]
  [InlineData("これはパスワードです", "77D72", "439DD942B422AA211A5BDDE2F4DC6AD218F")] // "thisisapassword" in Japanese, to test unicode compatibility
  public void GetKAnonimityPartsForPassword_WithPassword_GeneratesValidKAnonimityParts(string password, string expectedKAnonimityPart, string expectedKAnonimityRemainder)
  {
    var (kAnonimityPart, kAnonimityRemainder) = KAnonimityHelper.GetKAnonimityPartsForPassword(password);

    Assert.NotNull(kAnonimityPart);
    Assert.Equal(Constants.KAnonimity.PartLength, kAnonimityPart.Length);
    Assert.Equal(expectedKAnonimityPart, kAnonimityPart);

    Assert.NotNull(kAnonimityRemainder);
    Assert.Equal(Constants.KAnonimity.RemainderLength, kAnonimityRemainder.Length);
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
