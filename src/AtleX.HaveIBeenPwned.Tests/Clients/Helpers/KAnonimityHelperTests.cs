using AtleX.HaveIBeenPwned.Clients.Helpers;
using System;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests.Clients.Helpers
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
