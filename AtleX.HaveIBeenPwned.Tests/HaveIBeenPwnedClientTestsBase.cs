using AtleX.HaveIBeenPwned.Communication;
using AtleX.HaveIBeenPwned.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtleX.HaveIBeenPwned.Tests
{
  public abstract class HaveIBeenPwnedClientTestsBase
  {
    protected static IServiceClient CreateServiceClient()
    {
      var serviceClient = new Mock<IServiceClient>();

      serviceClient
        .Setup(sc => sc.GetBreachesAsync(It.IsAny<string>()))
        .ReturnsAsync((string account) =>
        {
          var result = new List<Breach>()
          {
            new Breach()
            {
              AddedDate = DateTime.Now,
              BreachDate = DateTime.Now,
              DataClasses = new [] { "password" },
              Description = "FAKE",
              IsFabricated = false,
              IsRetired = false,
              IsSensitive = false,
              IsSpamList = false,
              IsVerified = true,
              ModifiedDate = DateTime.Now,
              Name = "FAKE",
              PwnCount = 1337,
              Title = "FAKE"
            },
          };

          return result;
        });

      return serviceClient.Object;
    }
  }
}
