using System.Collections.Generic;

namespace System.Net.Http.Fake.Wiremock
{
    // http://wiremock.org/docs/response-templating/
    public sealed class WiremockResponse
    {
        public string Body { get; set; }

        public string BodyFileName { get; set; }

        public object JsonBody { get; set; }

        public HttpStatusCode Status { get; set; } = HttpStatusCode.OK;

        public IDictionary<string, string> Headers { get; } = new Dictionary<string, string>();
    }
}