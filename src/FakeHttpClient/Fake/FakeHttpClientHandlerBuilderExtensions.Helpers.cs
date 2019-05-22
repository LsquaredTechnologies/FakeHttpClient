// using Microsoft.AspNetCore.Http;
// using System.Net.Http;
// using System.Threading.Tasks;

// namespace System.Net.Http.Fake
// {
//     public static partial class FakeHttpHandlerBuilderExtensions
//     {
//         private static Func<HttpRequestMessage, Task<HttpResponseMessage>> CreateAction(Func<HttpRequestMessage, HttpResponseMessage> action) =>
//             (req) =>
//                 Task.FromResult(action(req));

//         private static Func<HttpRequestMessage, Task<bool>> CreatePredicate(PathString path) =>
//             (req) =>
//                 Task.FromResult(req.RequestUri.LocalPath.StartsWith(path.Value));
//     }
// }
