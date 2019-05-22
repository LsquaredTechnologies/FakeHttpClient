using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace System.Net.Http.Fake
{
    public static partial class FakeHttpClientHandlerBuilderExtensions
    {
        public static IFakeHttpClientBuilder Map(this IFakeHttpClientBuilder builder, IPredicate predicate, FileInfo file) =>
            builder.Map(
                predicate,
                (res) =>
                    Task.FromResult(
                        new HttpResponseMessage
                        {
                            Content = new StreamContent(file.OpenRead())
                        }));
    }
}
