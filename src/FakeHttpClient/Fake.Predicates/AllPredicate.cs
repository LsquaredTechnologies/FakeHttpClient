using System.Linq;

namespace System.Net.Http.Fake.Predicates
{
    public sealed class AllPredicate : IPredicate
    {
        internal AllPredicate(params IPredicate[] predicates) =>
            _predicates = predicates;

        public bool Match(HttpRequestMessage req) =>
            Enumerable.All(_predicates, o => o.Match(req));

        public static IPredicate operator &(AllPredicate a, AllPredicate b) =>
            new AllPredicate(a, b);

        public static IPredicate operator &(AllPredicate a, IPredicate b) =>
            new AllPredicate(a, b);

        private readonly IPredicate[] _predicates;
    }
}