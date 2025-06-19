// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

using System;
using System.Reflection;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.CsProj;
using BenchmarkDotNet.Toolchains.NativeAot;
using BenchmarkDotNet.Validators;
using Perfolizer.Horology;

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

  private static ManualConfig GetConfig()
  {
    var job = Job.Default;

    var config = DefaultConfig.Instance
      .AddDiagnoser(MemoryDiagnoser.Default)
      .AddColumn(StatisticColumn.Median, StatisticColumn.Min, StatisticColumn.Max)
      .AddJob(
 job.WithRuntime(CoreRuntime.Core80).AsBaseline(),
        job.WithRuntime(CoreRuntime.Core60),
        job.WithRuntime(NativeAotRuntime.Net80)
      );

    if (Environment.OSVersion.Platform == PlatformID.Win32NT)
    {
      config.AddJob(job.WithRuntime(ClrRuntime.Net481));
    }

    config.SummaryStyle = SummaryStyle.Default
      .WithRatioStyle(RatioStyle.Percentage);

    config.AddValidator(JitOptimizationsValidator.FailOnError); // Fail when any of the referenced assemblies are not optimized

    config.WithOrderer(new DefaultOrderer(SummaryOrderPolicy.FastestToSlowest));

    return config;
  }
}
