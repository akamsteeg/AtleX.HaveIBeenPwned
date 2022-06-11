using Xunit;

namespace AtleX.HaveIBeenPwned.Tests;

public class SiteBreachTests
{
  [Fact]
  public void ToString_ReturnsValueOfNameProperty()
  {
    const string title = "BREACH_TITLE";

    var p = new SiteBreach()
    {
      Title = title,
    };

    Assert.Equal(title, p.Title);
  }

  [Fact]
  public void Equals_WithNullObject_ReturnsFalse()
  {
    var p = new SiteBreach();

    Assert.False(p.Equals((object)null));
  }

  [Fact]
  public void Equals_WithWrongTypeObject_ReturnsFalse()
  {
    var p = new SiteBreach();

    Assert.False(p.Equals(string.Empty));
  }

  [Fact]
  public void Equals_WithSameTypeObjectButDifferentValues_ReturnsFalse()
  {
    var p = new SiteBreach()
    {
      Name = "FIRST",
    };

    var other = new SiteBreach()
    {
      Name = "SECOND",
    };

    Assert.False(p.Equals((object)other));
  }

  [Fact]
  public void Equals_WithSameTypeObjectAndSameValues_ReturnsTrue()
  {
    var p = new SiteBreach()
    {
      Name = "VALUE",
    };

    var other = new SiteBreach()
    {
      Name = "VALUE",
    };

    Assert.True(p.Equals((object)other));
  }

  [Fact]
  public void Equals_WithSameTypeButDifferentValues_ReturnsFalse()
  {
    var p = new SiteBreach()
    {
      Name = "FIRST",
    };

    var other = new SiteBreach()
    {
      Name = "SECOND",
    };

    Assert.False(p.Equals(other));
  }

  [Fact]
  public void Equals_WithSameTypeAndSameValues_ReturnsTrue()
  {
    var p = new SiteBreach()
    {
      Name = "VALUE",
    };

    var other = new SiteBreach()
    {
      Name = "VALUE",
    };

    Assert.True(p.Equals(other));
  }

  [Fact]
  public void GetHashCode_ReturnsDifferentHashCodesForDifferentValues()
  {
    var p = new SiteBreach()
    {
      Name = "FIRST",
    };

    var other = new SiteBreach()
    {
      Name = "SECOND",
    };

    Assert.NotEqual(p.GetHashCode(), other.GetHashCode());
  }

  [Fact]
  public void GetHashCode_ReturnsSameHashCodesForSameValues()
  {
    var p = new SiteBreach()
    {
      Name = "VALUE",
    };

    var other = new SiteBreach()
    {
      Name = "VALUE",
    };

    Assert.Equal(p.GetHashCode(), other.GetHashCode());
  }
}
