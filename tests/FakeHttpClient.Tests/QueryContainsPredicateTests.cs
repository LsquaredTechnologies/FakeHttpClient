using System;
using System.Net.Http;
using System.Net.Http.Fake;
using System.Net.Http.Fake.Predicates;
using Xunit;

namespace FakeHttpClient.Tests
{
    public class QueryContainsPredicateTests
    {
        [Fact]
        public void GivenKeyQueryPredicateAndNoQueryRequest_WhenMatch_ReturnsFalse()
        {
            // Arrange
            var predicate = new QueryContainsPredicate("foo");

            // Act
            var actual = predicate.Match(new HttpRequestMessage(HttpMethod.Get, "http://example.com/"));

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void GivenKeyQueryPredicateAndNonMatchingRequest_WhenMatch_ReturnsFalse()
        {
            // Arrange
            var predicate = new QueryContainsPredicate("foo");

            // Act
            var actual = predicate.Match(new HttpRequestMessage(HttpMethod.Get, "http://example.com/?bar"));

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void GivenKeyQueryPredicateAndMatchingRequest_WhenMatch_ReturnsTrue()
        {
            // Arrange
            var predicate = new QueryContainsPredicate("foo");

            // Act
            var actual = predicate.Match(new HttpRequestMessage(HttpMethod.Get, "http://example.com/?foo"));

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void GivenKeyQueryPredicateAndMatchingRequest2_WhenMatch_ReturnsTrue()
        {
            // Arrange
            var predicate = new QueryContainsPredicate("foo");

            // Act
            var actual = predicate.Match(new HttpRequestMessage(HttpMethod.Get, "http://example.com/?foo=bar"));

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void GivenKeyQueryPredicateAndMatchingRequest3_WhenMatch_ReturnsTrue()
        {
            // Arrange
            var predicate = new QueryContainsPredicate("foo");

            // Act
            var actual = predicate.Match(new HttpRequestMessage(HttpMethod.Get, "http://example.com/?foo=bar&foo=wiz"));

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void GivenKeyValueQueryPredicateAndNoQueryRequest_WhenMatch_ReturnsFalse()
        {
            // Arrange
            var predicate = new QueryContainsPredicate("foo");

            // Act
            var actual = predicate.Match(new HttpRequestMessage(HttpMethod.Get, "http://example.com/"));

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void GivenKeyValueQueryPredicateAndNonMatchingRequest_WhenMatch_ReturnsFalse()
        {
            // Arrange
            var predicate = new QueryContainsPredicate("foo");

            // Act
            var actual = predicate.Match(new HttpRequestMessage(HttpMethod.Get, "http://example.com/?bar"));

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void GivenKeyValueQueryPredicateAndMatchingRequest_WhenMatch_ReturnsFalse()
        {
            // Arrange
            var predicate = new QueryContainsPredicate("foo", "bar");

            // Act
            var actual = predicate.Match(new HttpRequestMessage(HttpMethod.Get, "http://example.com/?foo"));

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void GivenKeyValueQueryPredicateAndMatchingRequest2_WhenMatch_ReturnsTrue()
        {
            // Arrange
            var predicate = new QueryContainsPredicate("foo", "bar");

            // Act
            var actual = predicate.Match(new HttpRequestMessage(HttpMethod.Get, "http://example.com/?foo=bar"));

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void GivenKeyValueQueryPredicateAndMatchingRequest3_WhenMatch_ReturnsTrue()
        {
            // Arrange
            var predicate = new QueryContainsPredicate("foo", "bar");

            // Act
            var actual = predicate.Match(new HttpRequestMessage(HttpMethod.Get, "http://example.com/?foo=bar&foo=wiz"));

            // Assert
            Assert.True(actual);
        }
    }
}
