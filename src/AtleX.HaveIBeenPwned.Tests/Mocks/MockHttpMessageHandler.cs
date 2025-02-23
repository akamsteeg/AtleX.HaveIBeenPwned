// Copyright (c) Alex Kamsteeg (https://atlex.nl/) and contributors
// License: MIT (See LICENSE file)

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AtleX.HaveIBeenPwned.Tests.Mocks;

internal sealed class MockHttpMessageHandler
  : HttpMessageHandler
{
  protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
  {
    var response = string.Empty;

    if (request.RequestUri.AbsolutePath.StartsWith("/api/v3/breaches"))
    {
      response = @"[
                    {""Name"":""000webhost"",""Title"":""000webhost"",""Domain"":""000webhost.com"",""BreachDate"":""2015-03-01"",""AddedDate"":""2015-10-26T23:35:45Z"",""ModifiedDate"":""2017-12-10T21:44:27Z"",""PwnCount"":14936670,""Description"":"""",""LogoType"":""png"",""DataClasses"":[""Email addresses"", ""IP addresses"", ""Names"", ""Passwords""], ""IsVerified"":true, ""IsFabricated"":false, ""IsSensitive"":false, ""IsRetired"":false, ""IsSpamList"":false}

            ]";
    }
    else if (request.RequestUri.AbsolutePath.StartsWith("/api/v3/latestbreach"))
    {
      response = @"{""Name"":""000webhost"",""Title"":""000webhost"",""Domain"":""000webhost.com"",""BreachDate"":""2015-03-01"",""AddedDate"":""2015-10-26T23:35:45Z"",""ModifiedDate"":""2017-12-10T21:44:27Z"",""PwnCount"":14936670,""Description"":"""",""LogoType"":""png"",""DataClasses"":[""Email addresses"", ""IP addresses"", ""Names"", ""Passwords""], ""IsVerified"":true, ""IsFabricated"":false, ""IsSensitive"":false, ""IsRetired"":false, ""IsSpamList"":false}";
    }
    else if (request.RequestUri.AbsolutePath.StartsWith("/api/v3/breachedaccount")) // Breaches
    {
      response = @"[
                    {""Name"":""000webhost""}

            ]";
    }
    else if (request.RequestUri.AbsolutePath.StartsWith("/api/v3/breacheddomain")) // Breaches for a domain
    {
      response = @"
        {
          ""user"": [
                    ""000webhost""
                  ]   
         }";
    }
    else if (request.RequestUri.AbsolutePath.StartsWith("/api/v3/subscribeddomains")) // Subscribed domains
    {
      response = @"[
                    {""DomainName"":""example.com""}

            ]";
    }
    if (request.RequestUri.AbsolutePath.StartsWith("/api/v3/pasteaccount")) // Pastes
    {
      response = @"[
{
""Source"":""Pastebin"",
""Id"":""8Q0BvKD8"",
""Title"":""syslog"",
""Date"":""2014-03-04T19:14:54Z"",
""EmailCount"":139
},
{
""Source"":""Pastie"",
""Id"":""7152479"",
""Date"":""2013-03-28T16:51:10Z"",
""EmailCount"":30
}
]";
    }
    else if (request.RequestUri.AbsolutePath.StartsWith("/range")) // Pwned Passwords
    {
      response = @"
2DC183F740EE76F27B78EB39C8AD972A757:1
00D4F6E8FA6EECAD2A3AA415EEC418D38EC:2
011053FD0102E94D6AE2F8B83D76FAF94F6:1
012A7CA357541F0AC487871FEEC1891C49C:2
0136E006E24E7D152139815FB0FC6A50B15:3
01A85766CD276B17DE6DA022AA3CADAC3CE:3
";
    }

    var result = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
    {
      Content = new StringContent(response),
      RequestMessage = request,
    };

    return Task.FromResult(result);
  }
}
