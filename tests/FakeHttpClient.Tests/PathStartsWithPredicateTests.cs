using System;
using System.Net.Http;
using System.Net.Http.Fake;
using System.Net.Http.Fake.Predicates;
using Xunit;

namespace FakeHttpClient.Tests
{
    public class PathStartsWithPredicateTests
    {
        [Fact]
        public void GivenRootPathPredicateAndRootRequest_WhenMatch_ReturnsTrue()
        {
            // Arrange
            var predicate = new PathStartsWithPredicate("/");

            // Act
            var actual = predicate.Match(new HttpRequestMessage(HttpMethod.Get, "http://example.com/"));

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void GivenRootPathPredicateAndLongerPathRequest_WhenMatch_ReturnsTrue()
        {
            // Arrange
            var predicate = new PathStartsWithPredicate("/");

            // Act
            var actual = predicate.Match(new HttpRequestMessage(HttpMethod.Get, "http://example.com/foo/bar"));

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void GivenLongPathPredicateAndShorterPathRequest_WhenMatch_ReturnsFalse()
        {
            // Arrange
            var predicate = new PathStartsWithPredicate("/foo/bar");

            // Act
            var actual = predicate.Match(new HttpRequestMessage(HttpMethod.Get, "http://example.com/foo"));

            // Assert
            Assert.False(actual);
        }
    }
}
