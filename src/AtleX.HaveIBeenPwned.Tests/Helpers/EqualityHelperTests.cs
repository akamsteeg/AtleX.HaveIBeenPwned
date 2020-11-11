using AtleX.HaveIBeenPwned.Helpers;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests.Helpers
{
  public class EqualityHelperTests
  {
    [Fact]
    public void EqualsT_WithOtherNull_ReturnsFalse()
    {
      var result = EqualityHelper.Equals(this, (object)null);

      Assert.False(result);
    }

    [Fact]
    public void EqualsT_WithOtherDifferentObject_ReturnsFalse()
    {
      var result = EqualityHelper.Equals(this, new object());

      Assert.False(result);
    }

    [Fact]
    public void EqualsT_WithSameObject_ReturnsTrue()
    {
      var o = new Breach()
      {
        Name = "DUMMY"
      };

      var result = EqualityHelper.Equals(o, (object)o);

      Assert.True(result);
    }

    [Fact]
    public void EqualsTT_WithOtherNull_ReturnsFalse()
    {
      var result = EqualityHelper.Equals(new Breach(), (Breach)null);

      Assert.False(result);
    }

    [Fact]
    public void EqualsTT_WithOtherDifferentObject_ReturnsFalse()
    {
      var a = new Breach()
      {
        Name = "A"
      };

      var b = new Breach()
      {
        Name = "B"
      };

      var result = EqualityHelper.Equals(a, b);

      Assert.False(result);
    }

    [Fact]
    public void EqualsTT_WithOtherSameObject_ReturnsTrue()
    {
      var o = new Breach()
      {
        Name = "DUMMY"
      };

      var result = EqualityHelper.Equals(o, o);

      Assert.True(result);
    }
  }
}
