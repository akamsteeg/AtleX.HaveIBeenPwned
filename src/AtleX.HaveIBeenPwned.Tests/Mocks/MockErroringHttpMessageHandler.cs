using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AtleX.HaveIBeenPwned.Tests.Mocks
{
  internal sealed class MockErroringHttpMessageHandler
    : HttpMessageHandler
  {
    private readonly int _desiredResultStatusCode;

    public MockErroringHttpMessageHandler(int desiredResultStatusCode)
    {
      this._desiredResultStatusCode = desiredResultStatusCode;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
      var result = new HttpResponseMessage((HttpStatusCode)this._desiredResultStatusCode);

      // Add custom headers per statuscode if necessary
      switch ((int)this._desiredResultStatusCode)
      {
        case 429: // Too many requests
          {
            result.Headers.Add("retry-after", "1500");
            break;
          }
      }

      return Task.FromResult(result);
    }
  }
}
