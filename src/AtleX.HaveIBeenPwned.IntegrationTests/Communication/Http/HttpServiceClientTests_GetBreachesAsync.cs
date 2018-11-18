﻿using AtleX.HaveIBeenPwned.Communication;
using AtleX.HaveIBeenPwned.Communication.Http;
using AtleX.HaveIBeenPwned.IntegrationTests.XUnit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AtleX.HaveIBeenPwned.IntegrationTests.Communication.Http
{
  public class HttpServiceClientTests_GetBreachesAsync
  {
    [RunnableInDebugOnlyAttribute]
    public async Task GetBreachesAsync_WithValidInput_DoesNotThrow()
    {
      using (var httpClient = new HttpClient())
      using (var c = new HttpServiceClient(new ClientSettings(), httpClient))
      {
        var result = await c.GetBreachesAsync("test@example.com");

        Assert.NotNull(result);
      }
    }

    [RunnableInDebugOnlyAttribute]
    public async Task GetBreachesAsync_WithValidInputAndVerifiedBreaches_DoesNotThrow()
    {
      using (var httpClient = new HttpClient())
      using (var c = new HttpServiceClient(new ClientSettings(), httpClient))
      {
        var result = await c.GetBreachesAsync("test@example.com", BreachMode.IncludeUnverified);

        Assert.NotNull(result);
      }
    }
  }
}
