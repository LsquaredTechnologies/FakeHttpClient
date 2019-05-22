namespace System.Net.Http.Fake.Predicates
{
    public sealed class MethodPredicate : IPredicate
    {
        internal MethodPredicate(string verb) =>
            _verb = verb;

        public bool Match(HttpRequestMessage req) =>
            req.Method.Method == _verb;

        public static IPredicate operator &(MethodPredicate a, MethodPredicate b) =>
            new AllPredicate(a, b);

        public static IPredicate operator &(MethodPredicate a, IPredicate b) =>
            new AllPredicate(a, b);

        private readonly string _verb;
    }
}