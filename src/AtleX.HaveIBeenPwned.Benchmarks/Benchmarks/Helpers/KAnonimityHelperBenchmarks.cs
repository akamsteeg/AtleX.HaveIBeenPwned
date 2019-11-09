using AtleX.HaveIBeenPwned.Helpers;
using BenchmarkDotNet.Attributes;

namespace AtleX.HaveIBeenPwned.Benchmarks.Benchmarks.Helpers
{
  public class KAnonimityHelperBenchmarks
  {
    private const string Password = "-&HxcB_dH+M@BZAXHCt7eJv.)eJ&,#x}*6TRTvFzrz#KH.k+fgXXHJ%?]ioQbuAz";

    [Benchmark]
    public (string, string) GetKAnonimityPartsForPassword()
    {
      var result = KAnonimityHelper.GetKAnonimityPartsForPassword(Password);

      return result;
    }
  }
}
