// Copyright (c) Alex Kamsteeg (https://atlex.nl/)
// License: MIT (See LICENSE file)

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

  [Fact]
  public void EqualsOperater_WithNull_IsFalse()
  {
    var sb = new SiteBreach()
    {
      Name = "FIRST",
    };

    SiteBreach other = null;

    Assert.False(sb == other);
  }

  [Theory]
  [InlineData("sitebreah", true)]
  [InlineData("DUMMY", false)]
  public void EqualsOperater_WithSameType_IsExpected(string name, bool expected)
  {
    var sb = new SiteBreach()
    {
      Name = "sitebreah",
    };

    var other = new SiteBreach()
    {
      Name = name
    };

    Assert.Equal(expected, sb == other);
  }

  [Theory]
  [InlineData("sitebreah", true)]
  [InlineData("DUMMY", false)]
  public void EqualsOperater_WithObject_IsExpected(string name, bool expected)
  {
    var sb = new SiteBreach()
    {
      Name = "sitebreah",
    };

    var other = new SiteBreach()
    {
      Name = name
    };

    Assert.Equal(expected, sb == (object)other);
  }

  [Fact]
  public void EqualsOperater_WithObjectOfDifferentType_IsFalse()
  {
    var sb = new SiteBreach()
    {
      Name = "SiteBreach",
    };

    var other = new DomainUser();

    Assert.False(sb == other);
  }

  [Fact]
  public void NotEqualsOperater_WithNull_IsTrue()
  {
    var sb = new SiteBreach()
    {
      Name = "SiteBreach",
    };

    SiteBreach other = null;

    Assert.True(sb != other);
  }

  [Theory]
  [InlineData("SiteBreach", false)]
  [InlineData("DUMMY", true)]
  public void NotEqualsOperater_WithSameType_IsExpected(string name, bool expected)
  {
    var sb = new SiteBreach()
    {
      Name = "SiteBreach",
    };

    var other = new SiteBreach()
    {
      Name = name
    };

    Assert.Equal(expected, sb != other);
  }

  [Theory]
  [InlineData("SiteBreach", false)]
  [InlineData("DUMMY", true)]
  public void NotEqualsOperater_WithObject_IsExpected(string name, bool expected)
  {
    var sb = new SiteBreach()
    {
      Name = "SiteBreach",
    };

    var other = new SiteBreach()
    {
      Name = name
    };

    Assert.Equal(expected, sb != (object)other);
  }

  [Fact]
  public void NotEqualsOperater_WithObjectOfDifferentType_IsTrue()
  {
    var sb = new SiteBreach()
    {
      Name = "SiteBreach",
    };

    var other = new Paste();

    Assert.True(sb != other);
  }
}
