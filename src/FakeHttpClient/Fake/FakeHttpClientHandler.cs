using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http.Fake
{
    public sealed class FakeHttpClientHandler : HttpMessageHandler
    {
        internal FakeHttpClientHandler(params IFakeHttpClientHandler[] handlers) =>
            _handlers = handlers;

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = null;

            var handled = false;
            foreach (var handler in _handlers)
            {
                (handled, response) = await handler.SendAsync(request);
                if (handled) break;
            }

            if (!handled)
                response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Empty)
                };

            return response;
        }

        private readonly IFakeHttpClientHandler[] _handlers;
    }
}