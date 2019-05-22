using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Fake;
using System.Threading.Tasks;

namespace FakeHttpClient.Demo
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            var client = System.Net.Http.Fake.FakeHttpClient.Builder
                .WithBaseAddress("http://example.com/")
                .FromFile(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "search.api.json"))
                .Map(
                    When.Path.StartsWith("/") & When.Query.Contains("bar"),
                    new { message = "Hello" })
                .Build();

            var res = await client.GetAsync("/path/foo/match?search=WireMock");
            var text = await res.Content.ReadAsStringAsync();
        }
    }
}
