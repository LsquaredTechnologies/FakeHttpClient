using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace System.Net.Http.Fake
{
    public static partial class FakeHttpClientHandlerBuilderExtensions
    {
        public static IFakeHttpClientBuilder Map(this IFakeHttpClientBuilder builder, IPredicate predicate, object instance) =>
            builder.Map(
                predicate,
                instance,
                contentType: "application/json");

        // public static IFakeHttpHandlerBuilder When(this IFakeHttpHandlerBuilder builder, IPredicate predicate, object instance, HeaderDictionary) =>
        //     builder.When(
        //         predicate,
        //         instance,
        //         contentType: "application/json");

        public static IFakeHttpClientBuilder Map(this IFakeHttpClientBuilder builder, IPredicate predicate, object instance, string contentType) =>
            builder.Map(
                predicate,
                (res) =>
                    Task.FromResult(
                        new HttpResponseMessage
                        {
                            Content = new StringContent(JsonConvert.SerializeObject(instance))
                            {
                                Headers =
                                {
                                    ContentType = new MediaTypeHeaderValue(contentType)
                                }
                            }
                        }));
    }
}
