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
  public void GetHashCode_WithTwoValues_ReturnsSameCodeForSameValues()
  {
    const string TestValue1 = "TESTVALUE1";
    const string TestValue2 = "TESTVALUE1";

    var value1 = HashCodeHelper.GetHashCode(TestValue1, TestValue2);
    var value2 = HashCodeHelper.GetHashCode(TestValue1, TestValue2);

    Assert.Equal(value1, value2);
  }
}
