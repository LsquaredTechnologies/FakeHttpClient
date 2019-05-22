using System.Threading.Tasks;

namespace System.Net.Http.Fake
{
    public interface IFakeHttpClientBuilder
    {
        FakeHttpClient Build();

        IFakeHttpClientBuilder Map(IPredicate predicate, Func<HttpRequestMessage, Task<HttpResponseMessage>> action);

        IFakeHttpClientBuilder Add(Action<HttpClient> configure);
    }
}
