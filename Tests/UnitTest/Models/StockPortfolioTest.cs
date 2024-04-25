using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Xunit;

namespace Tests.Models
{
    public class StockPortfolioTest
    {
        [Fact]
        public void HasExpectedProperties()
        {
            // Arrange
            var portifolio = new StockPortfolio();

            // Act
            var properties = typeof(StockPortfolio).GetProperties();

            // Assert
            Assert.Contains(properties, p => p.Name == "AppUserId" && p.PropertyType == typeof(string));
            Assert.Contains(properties, p => p.Name == "StockId" && p.PropertyType == typeof(int));
            Assert.Contains(properties, p => p.Name == "Stock" && p.PropertyType == typeof(StockDB));
            Assert.Contains(properties, p => p.Name == "AppUser" && p.PropertyType == typeof(AppUser));
        }

        [Fact]
        public void Associations_AreSetCorrectly()
        {
            // Arrange
            var stock = new StockDB { Id = 1 };
            var user = new AppUser { Id = "test_user_id" };
            var portifolio = new StockPortfolio
            {
                StockId = 1,
                AppUserId = "test_user_id",
                Stock = stock,
                AppUser = user
            };

            // Assert
            Assert.Equal(1, portifolio.StockId);
            Assert.Equal("test_user_id", portifolio.AppUserId);
            Assert.Same(stock, portifolio.Stock);
            Assert.Same(user, portifolio.AppUser);
        }
    }
}