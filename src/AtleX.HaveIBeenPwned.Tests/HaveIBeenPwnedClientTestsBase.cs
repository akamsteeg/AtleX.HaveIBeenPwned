using AtleX.HaveIBeenPwned.Communication;
using AtleX.HaveIBeenPwned.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AtleX.HaveIBeenPwned.Tests
{
  public abstract class HaveIBeenPwnedClientTestsBase
  {
    protected static IHaveIBeenPwnedClient CreateServiceClient()
    {
      var serviceClient = new Mock<IHaveIBeenPwnedClient>();

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

      serviceClient.Setup(sc => sc.IsPwnedPasswordAsync(It.IsAny<string>()))
        .ReturnsAsync((string password) =>
        {
          return true;
        });
      serviceClient.Setup(sc => sc.IsPwnedPasswordAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync((string password, CancellationToken cancellationToken) =>
        {
          return true;
        });

      return serviceClient.Object;
    }
  }
}
