using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace System.Net.Http.Fake
{
    public static partial class FakeHttpClientHandlerBuilderExtensions
    {
        public static IFakeHttpClientBuilder Map(this IFakeHttpClientBuilder builder, IPredicate predicate, byte[] content) =>
            builder.Map(
                predicate,
                (res) =>
                    Task.FromResult(
                        new HttpResponseMessage
                        {
                            Content = new ByteArrayContent(content)
                        }));

        public static IFakeHttpClientBuilder Map(this IFakeHttpClientBuilder builder, PathString path, byte[] content) =>
            builder.Map(
                When.Path.StartsWith(path),
                (res) =>
                    Task.FromResult(
                        new HttpResponseMessage
                        {
                            Content = new ByteArrayContent(content)
                        }));
    }
}