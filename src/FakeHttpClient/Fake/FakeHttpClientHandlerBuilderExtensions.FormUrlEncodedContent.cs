using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace System.Net.Http.Fake
{
    public static partial class FakeHttpClientHandlerBuilderExtensions
    {
        public static IFakeHttpClientBuilder Map(this IFakeHttpClientBuilder builder, IPredicate predicate, IDictionary<string, string> nameValueCollection) =>
            builder.Map(
                predicate,
                (res) =>
                    Task.FromResult(
                        new HttpResponseMessage
                        {
                            Content = new FormUrlEncodedContent(nameValueCollection)
                        }));

        public static IFakeHttpClientBuilder Map(this IFakeHttpClientBuilder builder, IPredicate predicate, IReadOnlyDictionary<string, string> nameValueCollection) =>
            builder.Map(
                predicate,
                (res) =>
                    Task.FromResult(
                        new HttpResponseMessage
                        {
                            Content = new FormUrlEncodedContent(nameValueCollection)
                        }));
    }
}
