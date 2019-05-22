using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;

namespace System.Net.Http.Fake.Predicates
{
    public sealed class PathContainsPredicate : IPredicate
    {
        internal PathContainsPredicate(PathString path) =>
            _path = path.Value;

        public bool Match(HttpRequestMessage req) =>
            req.RequestUri.LocalPath.Contains(_path);

        public static IPredicate operator &(PathContainsPredicate a, PathContainsPredicate b) =>
            new AllPredicate(a, b);

        public static IPredicate operator &(PathContainsPredicate a, IPredicate b) =>
            new AllPredicate(a, b);

        private readonly string _path;
    }
}