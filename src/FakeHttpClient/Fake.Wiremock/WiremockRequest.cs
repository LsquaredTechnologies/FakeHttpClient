using System.Collections.Generic;

namespace System.Net.Http.Fake.Wiremock
{
    // http://wiremock.org/docs/request-matching/
    public sealed class WiremockRequest
    {
        public IDictionary<string, WiremockValue> QueryParameters { get; } = new Dictionary<string, WiremockValue>();

        public IDictionary<string, WiremockValue> Cookies { get; } = new Dictionary<string, WiremockValue>();

        public IDictionary<string, WiremockValue> Headers { get; } = new Dictionary<string, WiremockValue>();

        public string Method { get; set; } = "GET";

        public string Url { get; set; }

        public string UrlPathMatching { get; set; }

        public BasicAuth AasicAuth { get; set; }
    }
    public sealed class BasicAuth
    {
        public string Username { get; set; }
        
        public string Password { get; set; }
    }
}