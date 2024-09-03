// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

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

    Assert.Equal(name, b.ToString());
  }

  [Fact]
  public void Equals_WithNullObject_ReturnsFalse()
  {
    var b = new Breach();

    Assert.False(b.Equals((object)null));
  }

  [Fact]
  public void Equals_WithWrongTypeObject_ReturnsFalse()
  {
    var b = new Breach();

    Assert.False(b.Equals(string.Empty));
  }

  [Fact]
  public void Equals_WithSameTypeObjectButDifferentValues_ReturnsFalse()
  {
    var b = new Breach()
    {
      Name = "FIRST",
    };

    var other = new Breach()
    {
      Name = "SECOND",
    };

    Assert.False(b.Equals((object)other));
  }

  [Fact]
  public void Equals_WithSameTypeObjectAndSameValues_ReturnsTrue()
  {
    var b = new Breach()
    {
      Name = "VALUE",
    };

    var other = new Breach()
    {
      Name = "VALUE",
    };

    Assert.True(b.Equals((object)other));
  }

  [Fact]
  public void Equals_WithSameTypeButDifferentValues_ReturnsFalse()
  {
    var b = new Breach()
    {
      Name = "FIRST",
    };

    var other = new Breach()
    {
      Name = "SECOND",
    };

    Assert.False(b.Equals(other));
  }

  [Fact]
  public void Equals_WithSameTypeAndSameValues_ReturnsTrue()
  {
    var b = new Breach()
    {
      Name = "VALUE",
    };

    var other = new Breach()
    {
      Name = "VALUE",
    };

    Assert.True(b.Equals(other));
  }

  [Fact]
  public void GetHashCode_ReturnsDifferentHashCodesForDifferentValues()
  {
    var b = new Breach()
    {
      Name = "FIRST",
    };

    var other = new Breach()
    {
      Name = "SECOND",
    };

    Assert.NotEqual(b.GetHashCode(), other.GetHashCode());
  }

  [Fact]
  public void GetHashCode_ReturnsSameHashCodesForSameValues()
  {
    var b = new Breach()
    {
      Name = "VALUE",
    };

    var other = new Breach()
    {
      Name = "VALUE",
    };

    Assert.Equal(b.GetHashCode(), other.GetHashCode());
  }

  [Fact]
  public void EqualsOperater_WithNull_IsFalse()
  {
    var b = new Breach()
    {
      Name = "FIRST",
    };

    Breach other = null;

    Assert.False(b == other);
  }

  [Theory]
  [InlineData("breach", true)]
  [InlineData("DUMMY", false)]
  public void EqualsOperater_WithSameType_IsExpected(string name, bool expected)
  {
    var b = new Breach()
    {
      Name = "breach",
    };

    var other = new Breach()
    {
      Name = name
    };

    Assert.Equal(expected, b == other);
  }

  [Theory]
  [InlineData("breach", true)]
  [InlineData("DUMMY", false)]
  public void EqualsOperater_WithObject_IsExpected(string name, bool expected)
  {
    var b = new Breach()
    {
      Name = "breach",
    };

    var other = new Breach()
    {
      Name = name
    };

    Assert.Equal(expected, b == (object)other);
  }

  [Fact]
  public void EqualsOperater_WithObjectOfDifferentType_IsFalse()
  {
    var b = new Breach()
    {
      Name = "breach",
    };

    var other = new Paste();

    Assert.False(b == other);
  }

  [Fact]
  public void NotEqualsOperater_WithNull_IsTrue()
  {
    var b = new Breach()
    {
      Name = "breach",
    };

    Breach other = null;

    Assert.True(b != other);
  }

  [Theory]
  [InlineData("breach", false)]
  [InlineData("DUMMY", true)]
  public void NotEqualsOperater_WithSameType_IsExpected(string name, bool expected)
  {
    var b = new Breach()
    {
      Name = "breach",
    };

    var other = new Breach()
    {
      Name = name
    };

    Assert.Equal(expected, b != other);
  }

  [Theory]
  [InlineData("breach", false)]
  [InlineData("DUMMY", true)]
  public void NotEqualsOperater_WithObject_IsExpected(string name, bool expected)
  {
    var b = new Breach()
    {
      Name = "breach",
    };

    var other = new Breach()
    {
      Name = name
    };

    Assert.Equal(expected, b != (object)other);
  }

  [Fact]
  public void NotEqualsOperater_WithObjectOfDifferentType_IsTrue()
  {
    var b = new Breach()
    {
      Name = "breach",
    };

    var other = new Paste();

    Assert.True(b != other);
  }
}
