using System.Collections.Generic;
using System.Linq;

namespace System.Net.Http.Fake.Predicates
{
    public sealed class QueryEqualsToPredicate : IPredicate
    {
        internal QueryEqualsToPredicate(string name, string value, bool ignoreCase) =>
            (_name, _value, _ignoreCase) = (name, value, ignoreCase);

        public bool Match(HttpRequestMessage req) =>
            CreateQuery(req.RequestUri.Query)
                .Any(kv =>
                    kv.Key == _name &&
                    string.Equals(
                        kv.Value,
                        _value,
                        _ignoreCase
                            ? StringComparison.OrdinalIgnoreCase
                            : StringComparison.Ordinal))
            ;

        public static IPredicate operator &(QueryEqualsToPredicate a, QueryEqualsToPredicate b) =>
            new AllPredicate(a, b);

        public static IPredicate operator &(QueryEqualsToPredicate a, IPredicate b) =>
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
        private readonly string _value;
        private readonly bool _ignoreCase;
    }
}