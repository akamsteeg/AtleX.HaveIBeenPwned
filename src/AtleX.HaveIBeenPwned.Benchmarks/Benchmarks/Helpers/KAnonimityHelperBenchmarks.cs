// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

using AtleX.HaveIBeenPwned.Helpers;
using BenchmarkDotNet.Attributes;

namespace AtleX.HaveIBeenPwned.Benchmarks.Benchmarks.Helpers;

[BenchmarkCategory("Helpers")]
public class KAnonimityHelperBenchmarks
{
  [Benchmark]
  [Arguments("-&HxcB_d")] // 8 characters
  [Arguments("-&HxcB_dH+M@BZAXHCt7eJv.)eJ&,#x}*6TRTvFzrz#KH.k+fgXXHJ%?]ioQbuAz")] // 64 characters
  [Arguments("これはパスワードです")] // "thisisapassword" in Japanese
  public (string, string) GetKAnonimityPartsForPassword(string password)
  {
    var result = KAnonimityHelper.GetKAnonimityPartsForPassword(password);

    return result;
  }
}
