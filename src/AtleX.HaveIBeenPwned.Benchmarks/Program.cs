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

      config.Add(BenchmarkDotNet.Diagnosers.MemoryDiagnoser.Default);

      config.Add(
        Job.ShortRun.With(CsProjCoreToolchain.NetCoreApp22).AsBaseline(),
        Job.ShortRun.With(CsProjCoreToolchain.NetCoreApp30)
        );

      return config;
    }
  }
}
