using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Xunit;

namespace Tests.Models
{
    public class StockDBTest
    {
     [Fact]
        public void Properties_SetCorrectly()
        {
            // Arrange
            var stock = new StockDB();

            // Act
            stock.Id = 1;
            stock.Symbol = "AAPL";
            stock.Image = "apple.jpg";
            stock.CompanyName = "Apple Inc.";
            stock.Price = 150.50m;
            stock.Changes = 1.25m;
            stock.LastDiv = 0.75m;
            stock.MarketCap = 20000000000;
            stock.Currency = "USD";
            stock.Description = "Apple Inc. is an American multinational technology company.";
            stock.Industry = "Technology";

            // Assert
            Assert.Equal(1, stock.Id);
            Assert.Equal("AAPL", stock.Symbol);
            Assert.Equal("apple.jpg", stock.Image);
            Assert.Equal("Apple Inc.", stock.CompanyName);
            Assert.Equal(150.50m, stock.Price);
            Assert.Equal(1.25m, stock.Changes);
            Assert.Equal(0.75m, stock.LastDiv);
            Assert.Equal(20000000000, stock.MarketCap);
            Assert.Equal("USD", stock.Currency);
            Assert.Equal("Apple Inc. is an American multinational technology company.", stock.Description);
            Assert.Equal("Technology", stock.Industry);
        }
    }
}