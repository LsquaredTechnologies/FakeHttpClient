using System;
using System.Net.Http;
using System.Net.Http.Fake;
using System.Net.Http.Fake.Predicates;
using Xunit;

namespace FakeHttpClient.Tests
{
    public class MethodPredicateTests
    {
        [Fact]
        public void GivenGetMethodPredicateAndGetRequest_WhenMatch_ReturnsTrue()
        {
            // Arrange
            var predicate = new MethodPredicate("GET");

            // Act
            var actual = predicate.Match(new HttpRequestMessage(HttpMethod.Get, "/"));

            // Assert
            Assert.True(actual);
        }
        
        [Fact]
        public void GivenPostMethodPredicateAndGetRequest_WhenMatch_ReturnsFalse()
        {
            // Arrange
            var predicate = new MethodPredicate("POST");

            // Act
            var actual = predicate.Match(new HttpRequestMessage(HttpMethod.Get, "/"));

            // Assert
            Assert.False(actual);
        }
    }
}
