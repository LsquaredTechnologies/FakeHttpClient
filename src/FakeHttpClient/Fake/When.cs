using Microsoft.AspNetCore.Http;
using System.Net.Http.Fake.Predicates;

namespace System.Net.Http.Fake
{
    public static class When
    {
        public static AllPredicate All(params IPredicate[] predicates) =>
            new AllPredicate(predicates);

        public static class Method
        {
            public static MethodPredicate Is(string verb) =>
                new MethodPredicate(verb);
        }

        public static class Path
        {
            public static PathContainsPredicate Contains(PathString path) =>
                new PathContainsPredicate(path);
            public static PathStartsWithPredicate StartsWith(PathString path) =>
                new PathStartsWithPredicate(path);

            public static PathMatchesPredicate Matches(PathString path) =>
                new PathMatchesPredicate(path);
        }

        public static class Query
        {
            public static QueryContainsPredicate Contains(string name) =>
                new QueryContainsPredicate(name);

            public static QueryContainsPredicate Contains(string name, string value) =>
                new QueryContainsPredicate(name, value);

            public static QueryEqualsToPredicate EqualsTo(string name, string value, bool ignoreCase) =>
                new QueryEqualsToPredicate(name, value, ignoreCase);

            public static QueryMatchesPredicate Matches(string name, string value) =>
                new QueryMatchesPredicate(name, value);
        }
    }
}