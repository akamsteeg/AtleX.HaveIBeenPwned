// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

using Xunit;

namespace AtleX.HaveIBeenPwned.Tests;

public class PasteTests
{
  [Fact]
  public void ToString_ReturnsValueOfTitleProperty()
  {
    const string title = "PASTE_TITLE";

    var p = new Paste()
    {
      Title = title,
    };

    Assert.Equal(title, p.ToString());
  }

  [Fact]
  public void Equals_WithNullObject_ReturnsFalse()
  {
    var p = new Paste();

    Assert.False(p.Equals((object)null));
  }

  [Fact]
  public void Equals_WithWrongTypeObject_ReturnsFalse()
  {
    var p = new Paste();

    Assert.False(p.Equals(string.Empty));
  }

  [Fact]
  public void Equals_WithSameTypeObjectButDifferentValues_ReturnsFalse()
  {
    var p = new Paste()
    {
      Id = "FIRST",
    };

    var other = new Paste()
    {
      Id = "SECOND",
    };

    Assert.False(p.Equals((object)other));
  }

  [Fact]
  public void Equals_WithSameTypeObjectAndSameValues_ReturnsTrue()
  {
    var p = new Paste()
    {
      Id = "VALUE",
    };

    var other = new Paste()
    {
      Id = "VALUE",
    };

    Assert.True(p.Equals((object)other));
  }

  [Fact]
  public void Equals_WithSameTypeButDifferentValues_ReturnsFalse()
  {
    var p = new Paste()
    {
      Id = "FIRST",
    };

    var other = new Paste()
    {
      Id = "SECOND",
    };

    Assert.False(p.Equals(other));
  }

  [Fact]
  public void Equals_WithSameTypeAndSameValues_ReturnsTrue()
  {
    var p = new Paste()
    {
      Id = "VALUE",
    };

    var other = new Paste()
    {
      Id = "VALUE",
    };

    Assert.True(p.Equals(other));
  }

  [Fact]
  public void GetHashCode_ReturnsDifferentHashCodesForDifferentValues()
  {
    var p = new Paste()
    {
      Id = "FIRST",
    };

    var other = new Paste()
    {
      Id = "SECOND",
    };

    Assert.NotEqual(p.GetHashCode(), other.GetHashCode());
  }

  [Fact]
  public void GetHashCode_ReturnsSameHashCodesForSameValues()
  {
    var p = new Paste()
    {
      Id = "VALUE",
    };

    var other = new Paste()
    {
      Id = "VALUE",
    };

    Assert.Equal(p.GetHashCode(), other.GetHashCode());
  }

  [Fact]
  public void EqualsOperater_WithNull_IsFalse()
  {
    var p = new Paste();

    DomainUser other = null;

    Assert.False(p == other);
  }

  [Theory]
  [InlineData("paste", true)]
  [InlineData("DUMMY", false)]
  public void EqualsOperater_WithSameType_IsExpected(string id, bool expected)
  {
    var p = new Paste()
    {
      Id = "paste"
    };

    var other = new Paste()
    {
      Id = id
    };

    Assert.Equal(expected, p == other);
  }

  [Theory]
  [InlineData("paste", true)]
  [InlineData("DUMMY", false)]
  public void EqualsOperater_WithObject_IsExpected(string id, bool expected)
  {
    var p = new Paste()
    {
      Id = "paste"
    };

    var other = new Paste()
    {
      Id = id
    };

    Assert.Equal(expected, p == (object)other);
  }

  [Fact]
  public void EqualsOperater_WithObjectOfDifferentType_IsFalse()
  {
    var p = new Paste();

    var other = new DomainUser();

    Assert.False(p == other);
  }

  [Fact]
  public void NotEqualsOperater_WithNull_IsTrue()
  {
    var p = new Paste()
    {
      Id = "paste",
    };

    Paste other = null;

    Assert.True(p != other);
  }

  [Theory]
  [InlineData("paste", false)]
  [InlineData("DUMMY", true)]
  public void NotEqualsOperater_WithSameType_IsExpected(string id, bool expected)
  {
    var p = new Paste()
    {
      Id = "paste",
    };

    var other = new Paste()
    {
      Id = id
    };

    Assert.Equal(expected, p != other);
  }

  [Theory]
  [InlineData("paste", false)]
  [InlineData("DUMMY", true)]
  public void NotEqualsOperater_WithObject_IsExpected(string id, bool expected)
  {
    var p = new Paste()
    {
      Id = "paste",
    };

    var other = new Paste()
    {
      Id = id
    };

    Assert.Equal(expected, p != (object)other);
  }

  [Fact]
  public void NotEqualsOperater_WithObjectOfDifferentType_IsTrue()
  {
    var p = new Paste()
    {
      Id = "paste",
    };

    var other = new DomainUser();

    Assert.True(p != other);
  }
}
