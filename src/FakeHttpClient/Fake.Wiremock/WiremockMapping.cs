namespace System.Net.Http.Fake.Wiremock
{
    public sealed class WiremockMapping
    {
        public WiremockRequest Request { get; } = new WiremockRequest();

        public WiremockResponse Response { get; } = new WiremockResponse();
    }
}