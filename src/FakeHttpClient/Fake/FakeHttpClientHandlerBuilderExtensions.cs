using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Net.Http.Fake.Wiremock;
using System.Threading.Tasks;

namespace System.Net.Http.Fake
{
    public static partial class FakeHttpClientHandlerBuilderExtensions
    {
        public static IFakeHttpClientBuilder WithBaseAddress(this IFakeHttpClientBuilder builder, string address) =>
            builder.Add((client) => client.BaseAddress = new Uri(address));

        public static IFakeHttpClientBuilder WithBaseAddress(this IFakeHttpClientBuilder builder, Uri address) =>
            builder.Add((client) => client.BaseAddress = address);

        public static IFakeHttpClientBuilder WithDefaultResponse(this IFakeHttpClientBuilder builder, HttpStatusCode statusCode) =>
            builder.Map(
                When.Path.StartsWith("/"),
                (req) =>
                    Task.FromResult(new HttpResponseMessage(statusCode)
                    {
                        Content = new StringContent(string.Empty)
                    }));

        public static IFakeHttpClientBuilder FromFile(this IFakeHttpClientBuilder builder, string path) =>
            builder.FromFile(new FileInfo(path));

        public static IFakeHttpClientBuilder FromFile(this IFakeHttpClientBuilder builder, FileInfo file)
        {
            using (var stream = file.OpenRead())
            using (var reader = new StreamReader(stream))
            {
                var configuration = JsonConvert.DeserializeObject<WiremockConfiguration>(reader.ReadToEnd());
                foreach (var mapping in configuration.Mappings)
                {
                    IPredicate predicate = When.Method.Is(mapping.Request.Method);

                    if (mapping.Request.Url != null)
                        predicate = When.Path.StartsWith(mapping.Request.Url) & predicate;
                    else if (mapping.Request.UrlPathMatching != null)
                        predicate = When.Path.Matches(mapping.Request.UrlPathMatching) & predicate;

                    foreach (var entry in mapping.Request.QueryParameters)
                    {
                        if (entry.Value.Contains != null)
                        {
                            predicate = When.Query.Contains(entry.Key, entry.Value.Contains) & predicate;
                        }
                        else if (entry.Value.EqualTo != null)
                        {
                            predicate = When.Query.EqualsTo(entry.Key, entry.Value.EqualTo, entry.Value.IgnoreCase) & predicate;
                        }
                        else if (entry.Value.Matches != null)
                        {
                            predicate = When.Query.Matches(entry.Key, entry.Value.Matches) & predicate;
                        }
                    }

                    HttpContent content;
                    if (mapping.Response.Body != null)
                    {
                        content = new StringContent(mapping.Response.Body);
                    }
                    else if (mapping.Response.BodyFileName != null)
                    {
                        content = new StreamContent(File.OpenRead(mapping.Response.BodyFileName));
                    }
                    else if (mapping.Response.JsonBody != null)
                    {
                        content = new StringContent(mapping.Response.JsonBody.ToString());
                    }
                    else
                    {
                        content = new StringContent(string.Empty);
                    }

                    var response = new HttpResponseMessage(mapping.Response.Status) { Content = content };

                    foreach (var header in mapping.Response.Headers)
                    {
                        if (!content.Headers.TryAddWithoutValidation(header.Key, header.Value))
                            response.Headers.TryAddWithoutValidation(header.Key, header.Value);
                    }

                    builder.Map(predicate, response);
                }

                return builder;
            }
        }

        public static IFakeHttpClientBuilder Map(this IFakeHttpClientBuilder builder, IPredicate predicate, HttpResponseMessage response) =>
            builder.Map(
                predicate,
                (req) => Task.FromResult(response));

        public static IFakeHttpClientBuilder Map(this IFakeHttpClientBuilder builder, IPredicate predicate, Func<HttpRequestMessage, HttpResponseMessage, Task> action) =>
            builder.Map(
                predicate,
                async (req) =>
                {
                    var res = new HttpResponseMessage();
                    await action(req, res);
                    return res;
                });

        public static IFakeHttpClientBuilder Map(this IFakeHttpClientBuilder builder, IPredicate predicate, Func<HttpRequestMessage, Task<HttpResponseMessage>> action) =>
            builder.Map(
                predicate,
                action);

        public static IFakeHttpClientBuilder Map(this IFakeHttpClientBuilder builder, PathString path, Func<HttpRequestMessage, Task<HttpResponseMessage>> action) =>
            builder.Map(
                When.Path.StartsWith(path),
                action);
    }
}
