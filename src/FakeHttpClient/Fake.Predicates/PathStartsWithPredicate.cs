using Microsoft.AspNetCore.Http;

namespace System.Net.Http.Fake.Predicates
{
    public sealed class PathStartsWithPredicate : IPredicate
    {
        internal PathStartsWithPredicate(PathString path) =>
            _path = path;

        public bool Match(HttpRequestMessage req) =>
            req.RequestUri.LocalPath.StartsWith(_path);

        public static IPredicate operator &(PathStartsWithPredicate a, PathStartsWithPredicate b) =>
            new AllPredicate(a, b);

        public static IPredicate operator &(PathStartsWithPredicate a, IPredicate b) =>
            new AllPredicate(a, b);

        private readonly PathString _path;
    }
}