using System;
using System.Net.Http;
using System.Net.Http.Fake;
using System.Net.Http.Fake.Predicates;
using Xunit;

namespace FakeHttpClient.Tests
{
    public class PathMatchesPredicateTests
    {
        [Fact]
        public void GivenStarPredicateAndNoPathRequest_WhenMatch_ReturnsTrue()
        {
            // Arrange
            var predicate = new PathMatchesPredicate("/(.*)");

            // Act
            var actual = predicate.Match(new HttpRequestMessage(HttpMethod.Get, "http://example.com/"));

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void GivenStarPredicateAndNonMatchingRequest_WhenMatch_ReturnsTrue()
        {
            // Arrange
            var predicate = new PathMatchesPredicate("/(.*)");

            // Act
            var actual = predicate.Match(new HttpRequestMessage(HttpMethod.Get, "http://example.com/bar"));

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void GivenFooStarPredicateAndNoPathRequest_WhenMatch_ReturnsFalse()
        {
            // Arrange
            var predicate = new PathMatchesPredicate("/foo/(.*)");

            // Act
            var actual = predicate.Match(new HttpRequestMessage(HttpMethod.Get, "http://example.com/"));

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void GivenFooStarPredicateAndMatchingRequest_WhenMatch_ReturnsTrue()
        {
            // Arrange
            var predicate = new PathMatchesPredicate("/foo/(.*)");

            // Act
            var actual = predicate.Match(new HttpRequestMessage(HttpMethod.Get, "http://example.com/foo/bar"));

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void GivenStarFooPredicateAndNoPathRequest_WhenMatch_ReturnsFalse()
        {
            // Arrange
            var predicate = new PathMatchesPredicate("/(.*)/foo");

            // Act
            var actual = predicate.Match(new HttpRequestMessage(HttpMethod.Get, "http://example.com/"));

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void GivenStarFooPredicateAndMatchingRequest_WhenMatch_ReturnsTrue()
        {
            // Arrange
            var predicate = new PathMatchesPredicate("/(.*)/foo");

            // Act
            var actual = predicate.Match(new HttpRequestMessage(HttpMethod.Get, "http://example.com/wiz/foo"));

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void GivenFooStarBarPredicateAndNoPathRequest_WhenMatch_ReturnsFalse()
        {
            // Arrange
            var predicate = new PathMatchesPredicate("/foo/(.*)/bar");

            // Act
            var actual = predicate.Match(new HttpRequestMessage(HttpMethod.Get, "http://example.com/"));

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void GivenFooStarBarPredicateAndMatchingRequest_WhenMatch_ReturnsTrue()
        {
            // Arrange
            var predicate = new PathMatchesPredicate("/foo/(.*)/bar");

            // Act
            var actual = predicate.Match(new HttpRequestMessage(HttpMethod.Get, "http://example.com/foo/wiz/bar"));

            // Assert
            Assert.True(actual);
        }
    }
}
