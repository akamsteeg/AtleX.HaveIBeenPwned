using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.CsProj;
using System.Reflection;

namespace AtleX.HaveIBeenPwned.Benchmarks
{
  public static class Program
  {
    private static void Main(string[] args)
    {
      var config = GetConfig();
      BenchmarkRunner.Run(Assembly.GetExecutingAssembly(), config);
    }

    private static IConfig GetConfig()
    {
      var config = ManualConfig.Create(DefaultConfig.Instance);

      config.AddDiagnoser(BenchmarkDotNet.Diagnosers.MemoryDiagnoser.Default);

      config.AddJob(
        Job.ShortRun.WithToolchain(CsProjCoreToolchain.NetCoreApp31).AsBaseline(),
        Job.ShortRun.WithToolchain(CsProjCoreToolchain.NetCoreApp21),
        Job.ShortRun.WithToolchain(CsProjClassicNetToolchain.Net472),
        Job.ShortRun.WithToolchain(CsProjClassicNetToolchain.Net461)
        );

      return config;
    }
  }
}
