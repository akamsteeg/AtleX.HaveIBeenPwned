using AtleX.HaveIBeenPwned.Helpers;
using BenchmarkDotNet.Attributes;

namespace AtleX.HaveIBeenPwned.Benchmarks.Benchmarks.Helpers;

public class HashCodeHelperBenchmarks
{
  [Benchmark]
  [Arguments("-&Hx")]
  [Arguments("-&HxcB_d")]
  [Arguments("-&HxcB_dH+M@BZAX")]
  [Arguments("-&HxcB_dH+M@BZAXHCt7eJv.)eJ&,#x}")]
  public int GetHashCode(string value)
  {
    var result = HashCodeHelper.GetHashCode(value);

    return result;
  }

  [Benchmark]
  [Arguments("-&Hx", "-&HxcB_dH+M@BZAXHCt7eJv.)eJ&,#x}")]
  [Arguments("-&HxcB_d", "-&Hx")]
  [Arguments("-&HxcB_dH+M@BZAX", "-&HxcB_d")]
  [Arguments("-&HxcB_dH+M@BZAXHCt7eJv.)eJ&,#x}", "-&HxcB_dH+M@BZAX")]
  public int GetHashCode(string value1, string value2)
  {
    var result = HashCodeHelper.GetHashCode(value1, value2);

    return result;
  }
}
