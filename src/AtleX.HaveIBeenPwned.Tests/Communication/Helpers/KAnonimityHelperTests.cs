using AtleX.HaveIBeenPwned.Communication.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests.Communication.Helpers
{
  public class KAnonimityHelperTests
  {
    [Fact]
    public void GetHashForPassword_WithEmptyPassword_Throws()
    {
      Assert.Throws<ArgumentNullException>(() => KAnonimityHelper.GetHashForPassword(""));
    }

    [Fact]
    public void GetHashForPassword_WithPassword_GeneratesValidHash()
    {
      var hash = KAnonimityHelper.GetHashForPassword("DUMMY");

      Assert.NotNull(hash);
      Assert.Equal(40, hash.Length);
    }
  }
}
