using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;

namespace System.Net.Http.Fake.Predicates
{
    public sealed class PathMatchesPredicate : IPredicate
    {
        internal PathMatchesPredicate(PathString path) =>
            _path = new Regex(path.Value);

        public bool Match(HttpRequestMessage req) =>
            _path.IsMatch(req.RequestUri.LocalPath);

        public static IPredicate operator &(PathMatchesPredicate a, PathMatchesPredicate b) =>
            new AllPredicate(a, b);

        public static IPredicate operator &(PathMatchesPredicate a, IPredicate b) =>
            new AllPredicate(a, b);

        private readonly Regex _path;
    }
}