using System.Collections.Generic;

namespace System.Net.Http.Fake.Wiremock
{
    /// <summary>
    /// 
    /// </summary>
    /// <seeAlso>https://wiremock.readthedocs.io/</seeAlso>
    public sealed class WiremockConfiguration
    {
        public ICollection<WiremockMapping> Mappings { get; } = new List<WiremockMapping>();
    }
}