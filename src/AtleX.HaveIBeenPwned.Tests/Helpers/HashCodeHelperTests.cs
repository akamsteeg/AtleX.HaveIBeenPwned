// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

using System;
using AtleX.HaveIBeenPwned.Helpers;
using Xunit;

namespace AtleX.HaveIBeenPwned.Tests.Helpers;

public class HashCodeHelperTests
{
  [Fact]
  public void GetHashCode_WithSingleValue_ReturnsSameCodeForSameValue()
  {
    const string TestValue = "TESTVALUE";

    var value1 = HashCodeHelper.GetHashCode(TestValue);
    var value2 = HashCodeHelper.GetHashCode(TestValue);

    Assert.Equal(value1, value2);
  }

  [Fact]
  public void GetHashCode_WithSingleNullValue_ThrowsArgumentNullException()
  {
    Assert.Throws<ArgumentNullException>(() => HashCodeHelper.GetHashCode((string)null));
  }

  [Theory]
  [InlineData((string)null, "", "value1")]
  [InlineData("", (string)null, "value2")]
  public void GetHashCode_WithTwoValues_ThrowsArgumentNullException(string value1, string value2, string parameterName)
  {
    Assert.Throws<ArgumentNullException>(parameterName, () => HashCodeHelper.GetHashCode(value1, value2));
  }

  [Fact]
  public void GetHashCode_WithTwoValues_ReturnsSameCodeForSameValues()
  {
    const string TestValue1 = "TESTVALUE1";
    const string TestValue2 = "TESTVALUE1";

    var value1 = HashCodeHelper.GetHashCode(TestValue1, TestValue2);
    var value2 = HashCodeHelper.GetHashCode(TestValue1, TestValue2);

    Assert.Equal(value1, value2);
  }

  [Theory]
  [InlineData(1, 392)]
  [InlineData(2.0, 1073742215)]
  public void GetHashCode_WithSpecifiedInput_ReturnsExpectedOutput<T>(T testValue, int expectedResult)
    where T : notnull
  {
    var hashCode = HashCodeHelper.GetHashCode(testValue);

    Assert.Equal(expectedResult, hashCode);
  }

  [Theory]
  [InlineData(1, 2, 9018)]
  [InlineData(1.0, 2.0, -24108255)]
  public void GetHashCode_WithMultipleSpecifiedInputs_ReturnsExpectedOutput<T>(T testValue1, T testValue2, int expectedResult)
    where T : notnull
  {
    var hashCode = HashCodeHelper.GetHashCode(testValue1, testValue2);

    Assert.Equal(expectedResult, hashCode);
  }
}
