using Xunit;

namespace AtleX.HaveIBeenPwned.Tests;

public class BreachTests
{
  [Fact]
  public void ToString_ReturnsValueOfNameProperty()
  {
    const string name = "BREACH_NAME";

    var b = new Breach()
    {
      Name = name,
    };

    Assert.Equal(name, b.Name);
  }

  [Fact]
  public void Equals_WithNullObject_ReturnsFalse()
  {
    var p = new Breach();

    Assert.False(p.Equals((object)null));
  }

  [Fact]
  public void Equals_WithWrongTypeObject_ReturnsFalse()
  {
    var p = new Breach();

    Assert.False(p.Equals(string.Empty));
  }

  [Fact]
  public void Equals_WithSameTypeObjectButDifferentValues_ReturnsFalse()
  {
    var p = new Breach()
    {
      Name = "FIRST",
    };

    var other = new Breach()
    {
      Name = "SECOND",
    };

    Assert.False(p.Equals((object)other));
  }

  [Fact]
  public void Equals_WithSameTypeObjectAndSameValues_ReturnsTrue()
  {
    var p = new Breach()
    {
      Name = "VALUE",
    };

    var other = new Breach()
    {
      Name = "VALUE",
    };

    Assert.True(p.Equals((object)other));
  }

  [Fact]
  public void Equals_WithSameTypeButDifferentValues_ReturnsFalse()
  {
    var p = new Breach()
    {
      Name = "FIRST",
    };

    var other = new Breach()
    {
      Name = "SECOND",
    };

    Assert.False(p.Equals(other));
  }

  [Fact]
  public void Equals_WithSameTypeAndSameValues_ReturnsTrue()
  {
    var p = new Breach()
    {
      Name = "VALUE",
    };

    var other = new Breach()
    {
      Name = "VALUE",
    };

    Assert.True(p.Equals(other));
  }

  [Fact]
  public void GetHashCode_ReturnsDifferentHashCodesForDifferentValues()
  {
    var p = new Breach()
    {
      Name = "FIRST",
    };

    var other = new Breach()
    {
      Name = "SECOND",
    };

    Assert.NotEqual(p.GetHashCode(), other.GetHashCode());
  }

  [Fact]
  public void GetHashCode_ReturnsSameHashCodesForSameValues()
  {
    var p = new Breach()
    {
      Name = "VALUE",
    };

    var other = new Breach()
    {
      Name = "VALUE",
    };

    Assert.Equal(p.GetHashCode(), other.GetHashCode());
  }
}
