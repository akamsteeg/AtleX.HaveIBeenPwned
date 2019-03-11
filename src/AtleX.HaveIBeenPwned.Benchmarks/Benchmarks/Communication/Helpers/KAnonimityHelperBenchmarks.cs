using AtleX.HaveIBeenPwned.Communication.Helpers;
using BenchmarkDotNet.Attributes;

namespace AtleX.HaveIBeenPwned.Benchmarks.Benchmarks.Communication.Helpers
{
  public class KAnonimityHelperBenchmarks
  {
    private const string Password = "-&HxcB_dH+M@BZAXHCt7eJv.)eJ&,#x}*6TRTvFzrz#KH.k+fgXXHJ%?]ioQbuAz";

    [Benchmark]
    public void GetHashForPassword()
    {
      var result = KAnonimityHelper.GetHashForPassword(Password);
    }
  }
}
