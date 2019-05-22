namespace System.Net.Http.Fake
{
    public class FakeHttpClient : HttpClient
    {
        public static IFakeHttpClientBuilder Builder { get => new FakeHttpClientBuilder(); }

        protected internal FakeHttpClient(HttpMessageHandler handler) : base(handler) { }
    }
}
