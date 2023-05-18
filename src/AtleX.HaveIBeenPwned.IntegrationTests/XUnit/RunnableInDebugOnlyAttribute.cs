using System.Diagnostics;
using Xunit;

namespace AtleX.HaveIBeenPwned.IntegrationTests.XUnit;

/// <summary>
/// 
/// </summary>
/// <remarks>
/// Source: https://lostechies.com/jimmybogard/2013/06/20/run-tests-explicitly-in-xunit-net/
/// </remarks>
public class RunnableInDebugOnlyAttribute : FactAttribute
{
  public RunnableInDebugOnlyAttribute()
  {
    if (!Debugger.IsAttached)
    {
      Skip = "Only running in interactive mode.";
    }
  }
}
