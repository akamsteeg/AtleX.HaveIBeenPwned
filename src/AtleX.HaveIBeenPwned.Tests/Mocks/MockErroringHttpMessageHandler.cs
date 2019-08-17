using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AtleX.HaveIBeenPwned.Tests.Mocks
{
  internal sealed class MockErroringHttpMessageHandler
    : HttpMessageHandler
  {
    private readonly HttpStatusCode _desiredResultStatusCode;

    public MockErroringHttpMessageHandler(HttpStatusCode desiredResultStatusCode)
    {
      this._desiredResultStatusCode = desiredResultStatusCode;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
      var result = new HttpResponseMessage(this._desiredResultStatusCode);

      // Add custom headers per statuscode if necessary
      switch (this._desiredResultStatusCode)
      {
        case HttpStatusCode.TooManyRequests:
          {
            result.Headers.Add("retry-after", "1500");
            break;
          }
      }

      return Task.FromResult(result);
    }
  }
}
