using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace System.Net.Http.Fake.Predicates
{
    public sealed class QueryMatchesPredicate : IPredicate
    {
        internal QueryMatchesPredicate(string name, string value) =>
            (_name, _value) = (name, new Regex(value));

        public bool Match(HttpRequestMessage req) =>
            CreateQuery(req.RequestUri.Query)
                .Any(kv =>
                    kv.Key == _name &&
                    _value.IsMatch(kv.Value))
            ;

        public static IPredicate operator &(QueryMatchesPredicate a, QueryMatchesPredicate b) =>
            new AllPredicate(a, b);

        public static IPredicate operator &(QueryMatchesPredicate a, IPredicate b) =>
            new AllPredicate(a, b);

        private static IEnumerable<KeyValuePair<string, string>> CreateQuery(string queryString) =>
            (queryString.IndexOf('?') == 0
                ? queryString.Substring(1)
                : queryString)
                .Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(kv => kv.Split('='))
                .Select(kv => kv.Length == 1
                    ? new KeyValuePair<string, string>(kv[0], kv[0])
                    : new KeyValuePair<string, string>(kv[0], kv[1]))
                ;

        private readonly string _name;
        private readonly Regex _value;
        private readonly bool _ignoreCase;
    }
}