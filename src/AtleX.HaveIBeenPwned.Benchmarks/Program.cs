using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Diagnostics.Windows;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.CsProj;
using System;
using System.Reflection;

namespace AtleX.HaveIBeenPwned.Benchmarks;

public static class Program
{
  public static void Main(string[] args)
  {
    var config = GetConfig();
    BenchmarkSwitcher
      .FromAssembly(Assembly.GetExecutingAssembly())
      .Run(args, config);
  }

  private static IConfig GetConfig()
  {
    var config = ManualConfig.Create(DefaultConfig.Instance);

    config
      .AddDiagnoser(MemoryDiagnoser.Default);

    if (Environment.OSVersion.Platform == PlatformID.Win32NT)
    {
      config.AddDiagnoser(new JitStatsDiagnoser());
    }

    config.AddJob(
      Job.Default.WithToolchain(CsProjCoreToolchain.NetCoreApp80).AsBaseline(),
      Job.Default.WithToolchain(CsProjCoreToolchain.NetCoreApp60),
      Job.Default.WithToolchain(CsProjClassicNetToolchain.Net481)
      );

    return config;
  }
}
