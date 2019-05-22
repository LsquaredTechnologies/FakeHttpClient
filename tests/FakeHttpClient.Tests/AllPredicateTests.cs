using System;
using System.Net.Http;
using System.Net.Http.Fake;
using System.Net.Http.Fake.Predicates;
using Xunit;

namespace FakeHttpClient.Tests
{
    public class AllPredicateTests
    {
        [Fact]
        public void GivenTrueAndTrue_WhenMatch_ReturnsTrue()
        {
            // Arrange
            var true1 = new TruePredicate();
            var true2 = new TruePredicate();
            var all = new AllPredicate(true1, true2);

            // Act
            var actual = all.Match(new HttpRequestMessage());

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void GivenFalseAndTrue_WhenMatch_ReturnsFalse()
        {
            // Arrange
            var false1 = new FalsePredicate();
            var true2 = new TruePredicate();
            var all = new AllPredicate(false1, true2);

            // Act
            var actual = all.Match(new HttpRequestMessage());

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void GivenTrueAndFalse_WhenMatch_ReturnsFalse()
        {
            // Arrange
            var true1 = new TruePredicate();
            var false2 = new FalsePredicate();
            var all = new AllPredicate(true1, false2);

            // Act
            var actual = all.Match(new HttpRequestMessage());

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void GivenFalseAndFalse_WhenMatch_ReturnsFalse()
        {
            // Arrange
            var false1 = new TruePredicate();
            var false2 = new FalsePredicate();
            var all = new AllPredicate(false1, false2);

            // Act
            var actual = all.Match(new HttpRequestMessage());

            // Assert
            Assert.False(actual);
        }

        private sealed class TruePredicate : IPredicate
        {
            public bool Match(HttpRequestMessage req) => true;
        }

        private sealed class FalsePredicate : IPredicate
        {
            public bool Match(HttpRequestMessage req) => false;
        }
    }
}
