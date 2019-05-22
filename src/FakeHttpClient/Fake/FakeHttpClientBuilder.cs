using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.Net.Http.Fake
{
    public sealed class FakeHttpClientBuilder : IFakeHttpClientBuilder
    {
        internal FakeHttpClientBuilder() { }

        public FakeHttpClient Build() =>
            Update(
                new FakeHttpClient(
                    new FakeHttpClientHandler(
                        _handlers.ToArray())));

        public IFakeHttpClientBuilder Map(IPredicate predicate, Func<HttpRequestMessage, Task<HttpResponseMessage>> action)
        {
            _handlers.Add(new FakeInnerHttpHandler(predicate, action));
            return this;
        }

        public IFakeHttpClientBuilder Add(Action<HttpClient> configure)
        {
            _configuration.Add(configure);
            return this;
        }

        private FakeHttpClient Update(FakeHttpClient client)
        {
            foreach (var action in _configuration)
                action(client);
            return client;
        }

        private readonly List<IFakeHttpClientHandler> _handlers = new List<IFakeHttpClientHandler>();
        private readonly List<Action<HttpClient>> _configuration = new List<Action<HttpClient>>();

        #region Nested

        private sealed class FakeInnerHttpHandler : IFakeHttpClientHandler
        {
            public FakeInnerHttpHandler(IPredicate predicate, Func<HttpRequestMessage, Task<HttpResponseMessage>> action) =>
                (_predicate, _action) = (predicate, action);

            public async Task<(bool, HttpResponseMessage)> SendAsync(HttpRequestMessage request)
            {
                HttpResponseMessage response = default;
                var canExecuteHandler = _predicate.Match(request);
                if (canExecuteHandler)
                    response = await _action(request);
                return (canExecuteHandler, response);
            }

            private readonly IPredicate _predicate;
            private readonly Func<HttpRequestMessage, Task<HttpResponseMessage>> _action;
        }

        #endregion
    }
}
