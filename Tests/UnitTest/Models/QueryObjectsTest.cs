using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Helpers;
using Xunit;

namespace Tests.UnitTest.Models
{
    public class QueryObjectsTest
    {
        [Fact]
        public void DefaultValues_ShouldBeSetCorrectly()
        {
            // Arrange
            var query = new QueryObject();

            // Assert
            Assert.False(query.OrderBySymbol);
            Assert.False(query.OrderByName);
            Assert.False(query.OrderByPrice);
            Assert.Equal(1, query.PageNumber);
            Assert.Equal(20, query.PageSize);
        }

        [Fact]
        public void Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var query = new QueryObject();

            // Act
            query.OrderBySymbol = true;
            query.PageNumber = 2;
            query.PageSize = 30;

            // Assert
            Assert.True(query.OrderBySymbol);
            Assert.False(query.OrderByName);
            Assert.False(query.OrderByPrice);
            Assert.Equal(2, query.PageNumber);
            Assert.Equal(30, query.PageSize);
        }
    }
}