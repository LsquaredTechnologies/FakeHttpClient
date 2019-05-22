namespace System.Net.Http.Fake.Wiremock
{
    public sealed class WiremockValue
    {
        public bool Absent { get; set; }

        public string Contains { get; set; }

        public string EqualTo { get; set; }

        public string EqualToJson { get; set; }

        public string EqualToXml { get; set; }

        public string BinaryEqualTo { get; set; } // base64 equality

        public string Matches { get; set; }

        public string DoesNotMatch { get; set; }

        public bool IgnoreCase { get; set; }
    }
}