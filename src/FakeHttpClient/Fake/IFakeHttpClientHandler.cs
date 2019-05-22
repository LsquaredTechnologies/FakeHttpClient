using System.Threading.Tasks;

namespace System.Net.Http.Fake
{
    public interface IFakeHttpClientHandler
    {
        Task<(bool, HttpResponseMessage)> SendAsync(HttpRequestMessage request);
    }
}
