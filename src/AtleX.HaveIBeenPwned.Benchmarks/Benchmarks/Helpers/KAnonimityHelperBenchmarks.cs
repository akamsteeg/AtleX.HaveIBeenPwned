using AtleX.HaveIBeenPwned.Helpers;
using BenchmarkDotNet.Attributes;

namespace AtleX.HaveIBeenPwned.Benchmarks.Benchmarks.Helpers;

[BenchmarkCategory("Helpers")]
public class KAnonimityHelperBenchmarks
{
  private const string Password = "-&HxcB_dH+M@BZAXHCt7eJv.)eJ&,#x}*6TRTvFzrz#KH.k+fgXXHJ%?]ioQbuAz";

  [Benchmark]
  [Arguments("-&Hx")]
  [Arguments("-&HxcB_d")]
  [Arguments("-&HxcB_dH+M@BZAX")]
  [Arguments("-&HxcB_dH+M@BZAXHCt7eJv.)eJ&,#x}")]
  [Arguments("-&HxcB_dH+M@BZAXHCt7eJv.)eJ&,#x}*6TRTvFzrz#KH.k+fgXXHJ%?]ioQbuAz")]
  public (string, string) GetKAnonimityPartsForPassword(string password)
  {
    var result = KAnonimityHelper.GetKAnonimityPartsForPassword(password);

    return result;
  }
}
